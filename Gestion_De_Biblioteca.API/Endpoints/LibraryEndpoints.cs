using Gestion_De_Biblioteca.API.Dtos;
using Gestion_De_Biblioteca.API.Services;

namespace Gestion_De_Biblioteca.API.Endpoints;

public static class LibraryEndpoints
{
    public static IEndpointRouteBuilder MapLibraryEndpoints(this IEndpointRouteBuilder app)
    {
        MapAuthorEndpoints(app);
        MapCategoryEndpoints(app);
        MapBookEndpoints(app);
        MapMemberEndpoints(app);
        MapLoanEndpoints(app);

        return app;
    }

    private static void MapAuthorEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/authors").WithTags("Authors");

        group.MapGet("/", async (ILibraryService service) => Results.Ok(await service.GetAuthorsAsync()));

        group.MapGet("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.GetAuthorAsync(id) is { } author ? Results.Ok(author) : Results.NotFound());

        group.MapPost("/", async (AuthorRequest request, ILibraryService service) =>
        {
            var author = await service.CreateAuthorAsync(request);
            return Results.Created($"/api/authors/{author.Id}", author);
        });

        group.MapPut("/{id:int}", async Task<IResult> (int id, AuthorRequest request, ILibraryService service) =>
            await service.UpdateAuthorAsync(id, request) is { } author ? Results.Ok(author) : Results.NotFound());

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.DeleteAuthorAsync(id) ? Results.NoContent() : Results.NotFound());
    }

    private static void MapCategoryEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories").WithTags("Categories");

        group.MapGet("/", async (ILibraryService service) => Results.Ok(await service.GetCategoriesAsync()));

        group.MapGet("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.GetCategoryAsync(id) is { } category ? Results.Ok(category) : Results.NotFound());

        group.MapPost("/", async (CategoryRequest request, ILibraryService service) =>
        {
            var category = await service.CreateCategoryAsync(request);
            return Results.Created($"/api/categories/{category.Id}", category);
        });

        group.MapPut("/{id:int}", async Task<IResult> (int id, CategoryRequest request, ILibraryService service) =>
            await service.UpdateCategoryAsync(id, request) is { } category ? Results.Ok(category) : Results.NotFound());

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.DeleteCategoryAsync(id) ? Results.NoContent() : Results.NotFound());
    }

    private static void MapBookEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/books").WithTags("Books");

        group.MapGet("/", async (ILibraryService service) => Results.Ok(await service.GetBooksAsync()));

        group.MapGet("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.GetBookAsync(id) is { } book ? Results.Ok(book) : Results.NotFound());

        group.MapGet("/search", async (string? category, ILibraryService service) =>
            Results.Ok(await service.SearchBooksByCategoryAsync(category)));

        group.MapPost("/", async Task<IResult> (BookRequest request, ILibraryService service) =>
        {
            var result = await service.CreateBookAsync(request);
            return result.Success
                ? Results.Created($"/api/books/{result.Data!.Id}", result.Data)
                : Results.BadRequest(result.Error);
        });

        group.MapPut("/{id:int}", async Task<IResult> (int id, BookRequest request, ILibraryService service) =>
        {
            var result = await service.UpdateBookAsync(id, request);
            return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
        });

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.DeleteBookAsync(id) ? Results.NoContent() : Results.NotFound());
    }

    private static void MapMemberEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/members").WithTags("Members");

        group.MapGet("/", async (ILibraryService service) => Results.Ok(await service.GetMembersAsync()));

        group.MapGet("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.GetMemberAsync(id) is { } member ? Results.Ok(member) : Results.NotFound());

        group.MapPost("/", async (MemberRequest request, ILibraryService service) =>
        {
            var member = await service.CreateMemberAsync(request);
            return Results.Created($"/api/members/{member.Id}", member);
        });

        group.MapPut("/{id:int}", async Task<IResult> (int id, MemberRequest request, ILibraryService service) =>
            await service.UpdateMemberAsync(id, request) is { } member ? Results.Ok(member) : Results.NotFound());

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ILibraryService service) =>
            await service.DeleteMemberAsync(id) ? Results.NoContent() : Results.NotFound());
    }

    private static void MapLoanEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/loans").WithTags("Loans");

        group.MapGet("/", async (ILibraryService service) => Results.Ok(await service.GetLoanHistoryAsync()));

        group.MapGet("/member/{memberId:int}", async (int memberId, ILibraryService service) =>
            Results.Ok(await service.GetLoanHistoryByMemberAsync(memberId)));

        group.MapPost("/", async Task<IResult> (LoanRequest request, ILibraryService service) =>
        {
            var result = await service.CreateLoanAsync(request);
            return result.Success
                ? Results.Created($"/api/loans/{result.Data!.Id}", result.Data)
                : Results.BadRequest(result.Error);
        });

        group.MapPut("/{id:int}/return", async Task<IResult> (int id, ReturnLoanRequest request, ILibraryService service) =>
        {
            var result = await service.ReturnLoanAsync(id, request);
            return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
        });
    }
}
