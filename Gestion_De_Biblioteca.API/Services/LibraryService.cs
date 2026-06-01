using Gestion_De_Biblioteca.API.Dtos;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Enums;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

namespace Gestion_De_Biblioteca.API.Services;

public class LibraryService(
    IRepository<Author> authors,
    IRepository<Category> categories,
    IBookRepository books,
    IRepository<Member> members,
    ILoanRepository loans) : ILibraryService
{
    private const decimal DailyLateFee = 1000m;

    public async Task<IReadOnlyList<AuthorResponse>> GetAuthorsAsync() =>
        (await authors.GetAllAsync()).OrderBy(author => author.LastName).Select(ToAuthorResponse).ToList();

    public async Task<AuthorResponse?> GetAuthorAsync(int id)
    {
        var author = await authors.GetByIdAsync(id);
        return author is null ? null : ToAuthorResponse(author);
    }

    public async Task<AuthorResponse> CreateAuthorAsync(AuthorRequest request)
    {
        var author = new Author
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Nationality = request.Nationality?.Trim(),
            BirthDate = request.BirthDate
        };

        await authors.AddAsync(author);
        await authors.SaveChangesAsync();
        return ToAuthorResponse(author);
    }

    public async Task<AuthorResponse?> UpdateAuthorAsync(int id, AuthorRequest request)
    {
        var author = await authors.GetByIdAsync(id);
        if (author is null)
        {
            return null;
        }

        author.FirstName = request.FirstName.Trim();
        author.LastName = request.LastName.Trim();
        author.Nationality = request.Nationality?.Trim();
        author.BirthDate = request.BirthDate;

        authors.Update(author);
        await authors.SaveChangesAsync();
        return ToAuthorResponse(author);
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await authors.GetByIdAsync(id);
        if (author is null)
        {
            return false;
        }

        authors.Delete(author);
        await authors.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<CategoryResponse>> GetCategoriesAsync() =>
        (await categories.GetAllAsync()).OrderBy(category => category.Name).Select(ToCategoryResponse).ToList();

    public async Task<CategoryResponse?> GetCategoryAsync(int id)
    {
        var category = await categories.GetByIdAsync(id);
        return category is null ? null : ToCategoryResponse(category);
    }

    public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim()
        };

        await categories.AddAsync(category);
        await categories.SaveChangesAsync();
        return ToCategoryResponse(category);
    }

    public async Task<CategoryResponse?> UpdateCategoryAsync(int id, CategoryRequest request)
    {
        var category = await categories.GetByIdAsync(id);
        if (category is null)
        {
            return null;
        }

        category.Name = request.Name.Trim();
        category.Description = request.Description?.Trim();

        categories.Update(category);
        await categories.SaveChangesAsync();
        return ToCategoryResponse(category);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await categories.GetByIdAsync(id);
        if (category is null)
        {
            return false;
        }

        categories.Delete(category);
        await categories.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<BookResponse>> GetBooksAsync() =>
        (await books.GetAllWithDetailsAsync()).Select(ToBookResponse).ToList();

    public async Task<BookResponse?> GetBookAsync(int id)
    {
        var book = await books.GetByIdWithDetailsAsync(id);
        return book is null ? null : ToBookResponse(book);
    }

    public async Task<IReadOnlyList<BookResponse>> SearchBooksByCategoryAsync(string? category) =>
        (await books.SearchByCategoryAsync(category)).Select(ToBookResponse).ToList();

    public async Task<ServiceResult<BookResponse>> CreateBookAsync(BookRequest request)
    {
        var validationError = await ValidateBookRequestAsync(request);
        if (validationError is not null)
        {
            return ServiceResult<BookResponse>.Fail(validationError);
        }

        var book = new Book
        {
            Title = request.Title.Trim(),
            Isbn = request.Isbn.Trim(),
            PublicationYear = request.PublicationYear,
            TotalCopies = request.TotalCopies,
            AvailableCopies = request.AvailableCopies,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId
        };

        await books.AddAsync(book);
        await books.SaveChangesAsync();

        var created = await books.GetByIdWithDetailsAsync(book.Id);
        return ServiceResult<BookResponse>.Ok(ToBookResponse(created ?? book));
    }

    public async Task<ServiceResult<BookResponse>> UpdateBookAsync(int id, BookRequest request)
    {
        var book = await books.GetByIdAsync(id);
        if (book is null)
        {
            return ServiceResult<BookResponse>.Fail("El libro indicado no existe.");
        }

        var validationError = await ValidateBookRequestAsync(request);
        if (validationError is not null)
        {
            return ServiceResult<BookResponse>.Fail(validationError);
        }

        book.Title = request.Title.Trim();
        book.Isbn = request.Isbn.Trim();
        book.PublicationYear = request.PublicationYear;
        book.TotalCopies = request.TotalCopies;
        book.AvailableCopies = request.AvailableCopies;
        book.AuthorId = request.AuthorId;
        book.CategoryId = request.CategoryId;

        books.Update(book);
        await books.SaveChangesAsync();

        var updated = await books.GetByIdWithDetailsAsync(id);
        return ServiceResult<BookResponse>.Ok(ToBookResponse(updated ?? book));
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await books.GetByIdAsync(id);
        if (book is null)
        {
            return false;
        }

        books.Delete(book);
        await books.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<MemberResponse>> GetMembersAsync() =>
        (await members.GetAllAsync()).OrderBy(member => member.LastName).Select(ToMemberResponse).ToList();

    public async Task<MemberResponse?> GetMemberAsync(int id)
    {
        var member = await members.GetByIdAsync(id);
        return member is null ? null : ToMemberResponse(member);
    }

    public async Task<MemberResponse> CreateMemberAsync(MemberRequest request)
    {
        var member = new Member
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            Phone = request.Phone?.Trim(),
            IsActive = request.IsActive
        };

        await members.AddAsync(member);
        await members.SaveChangesAsync();
        return ToMemberResponse(member);
    }

    public async Task<MemberResponse?> UpdateMemberAsync(int id, MemberRequest request)
    {
        var member = await members.GetByIdAsync(id);
        if (member is null)
        {
            return null;
        }

        member.FirstName = request.FirstName.Trim();
        member.LastName = request.LastName.Trim();
        member.Email = request.Email.Trim();
        member.Phone = request.Phone?.Trim();
        member.IsActive = request.IsActive;

        members.Update(member);
        await members.SaveChangesAsync();
        return ToMemberResponse(member);
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        var member = await members.GetByIdAsync(id);
        if (member is null)
        {
            return false;
        }

        members.Delete(member);
        await members.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<LoanResponse>> GetLoanHistoryAsync() =>
        (await loans.GetHistoryAsync()).Select(ToLoanResponse).ToList();

    public async Task<IReadOnlyList<LoanResponse>> GetLoanHistoryByMemberAsync(int memberId) =>
        (await loans.GetHistoryByMemberAsync(memberId)).Select(ToLoanResponse).ToList();

    public async Task<ServiceResult<LoanResponse>> CreateLoanAsync(LoanRequest request)
    {
        var book = await books.GetByIdAsync(request.BookId);
        if (book is null)
        {
            return ServiceResult<LoanResponse>.Fail("El libro indicado no existe.");
        }

        if (book.AvailableCopies <= 0)
        {
            return ServiceResult<LoanResponse>.Fail("No hay copias disponibles para prestar.");
        }

        var member = await members.GetByIdAsync(request.MemberId);
        if (member is null || !member.IsActive)
        {
            return ServiceResult<LoanResponse>.Fail("El miembro no existe o esta inactivo.");
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (request.DueDate < today)
        {
            return ServiceResult<LoanResponse>.Fail("La fecha de entrega no puede ser anterior a hoy.");
        }

        var loan = new Loan
        {
            BookId = request.BookId,
            MemberId = request.MemberId,
            LoanDate = today,
            DueDate = request.DueDate,
            Status = LoanStatus.Active
        };

        book.AvailableCopies--;
        await loans.AddAsync(loan);
        await loans.SaveChangesAsync();

        var created = await loans.GetByIdWithDetailsAsync(loan.Id);
        return ServiceResult<LoanResponse>.Ok(ToLoanResponse(created ?? loan));
    }

    public async Task<ServiceResult<LoanResponse>> ReturnLoanAsync(int id, ReturnLoanRequest request)
    {
        var loan = await loans.GetByIdWithDetailsAsync(id);
        if (loan is null)
        {
            return ServiceResult<LoanResponse>.Fail("El prestamo indicado no existe.");
        }

        if (loan.ReturnDate.HasValue)
        {
            return ServiceResult<LoanResponse>.Fail("Este prestamo ya fue devuelto.");
        }

        var returnDate = request.ReturnDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
        if (returnDate < loan.LoanDate)
        {
            return ServiceResult<LoanResponse>.Fail("La fecha de devolucion no puede ser anterior al prestamo.");
        }

        var lateDays = Math.Max(0, returnDate.DayNumber - loan.DueDate.DayNumber);
        loan.ReturnDate = returnDate;
        loan.LateFee = lateDays * DailyLateFee;
        loan.Status = LoanStatus.Returned;

        if (loan.Book is not null)
        {
            loan.Book.AvailableCopies++;
        }

        loans.Update(loan);
        await loans.SaveChangesAsync();

        return ServiceResult<LoanResponse>.Ok(ToLoanResponse(loan));
    }

    private async Task<string?> ValidateBookRequestAsync(BookRequest request)
    {
        if (request.TotalCopies < 1 || request.AvailableCopies < 0 || request.AvailableCopies > request.TotalCopies)
        {
            return "Las copias disponibles deben estar entre 0 y el total de copias.";
        }

        if (!await authors.ExistsAsync(request.AuthorId) || !await categories.ExistsAsync(request.CategoryId))
        {
            return "El autor o la categoria indicada no existe.";
        }

        return null;
    }

    private static AuthorResponse ToAuthorResponse(Author author) =>
        new(author.Id, author.FirstName, author.LastName, author.Nationality, author.BirthDate);

    private static CategoryResponse ToCategoryResponse(Category category) =>
        new(category.Id, category.Name, category.Description);

    private static BookResponse ToBookResponse(Book book) =>
        new(
            book.Id,
            book.Title,
            book.Isbn,
            book.PublicationYear,
            book.TotalCopies,
            book.AvailableCopies,
            book.AuthorId,
            book.Author is null ? null : $"{book.Author.FirstName} {book.Author.LastName}",
            book.CategoryId,
            book.Category?.Name);

    private static MemberResponse ToMemberResponse(Member member) =>
        new(member.Id, member.FirstName, member.LastName, member.Email, member.Phone, member.RegistrationDate, member.IsActive);

    private static LoanResponse ToLoanResponse(Loan loan) =>
        new(
            loan.Id,
            loan.BookId,
            loan.Book?.Title,
            loan.MemberId,
            loan.Member is null ? null : $"{loan.Member.FirstName} {loan.Member.LastName}",
            loan.LoanDate,
            loan.DueDate,
            loan.ReturnDate,
            loan.LateFee,
            GetLoanStatus(loan).ToString());

    private static LoanStatus GetLoanStatus(Loan loan)
    {
        if (loan.Status == LoanStatus.Active && loan.DueDate < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return LoanStatus.Overdue;
        }

        return loan.Status;
    }
}
