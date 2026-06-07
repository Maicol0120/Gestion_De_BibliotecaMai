using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Enums;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Gestion_De_Biblioteca.Domain.Interfaces.Services;

namespace Gestion_De_Biblioteca.Domain.Services;

public class LibraryService(
    IAuthorRepository authors,
    ICategoryRepository categories,
    IBookRepository books,
    IMemberRepository members,
    ILoanRepository loans) : ILibraryService
{
    private const decimal DailyLateFee = 1000m;

    public async Task<IReadOnlyList<Author>> GetAuthorsAsync() =>
        (await authors.GetAllAsync()).OrderBy(author => author.LastName).ToList();

    public Task<Author?> GetAuthorAsync(int id) => authors.GetByIdAsync(id);

    public async Task<Author> CreateAuthorAsync(Author author)
    {
        TrimAuthor(author);
        await authors.AddAsync(author);
        await authors.SaveChangesAsync();
        return author;
    }

    public async Task<Author?> UpdateAuthorAsync(int id, Author author)
    {
        var existingAuthor = await authors.GetByIdAsync(id);
        if (existingAuthor is null)
        {
            return null;
        }

        existingAuthor.FirstName = author.FirstName.Trim();
        existingAuthor.LastName = author.LastName.Trim();
        existingAuthor.Nationality = author.Nationality?.Trim();
        existingAuthor.Biography = author.Biography?.Trim();
        existingAuthor.BirthDate = author.BirthDate;
        existingAuthor.UpdatedAt = DateTime.UtcNow;

        authors.Update(existingAuthor);
        await authors.SaveChangesAsync();
        return existingAuthor;
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

    public async Task<IReadOnlyList<Category>> GetCategoriesAsync() =>
        (await categories.GetAllAsync()).OrderBy(category => category.Name).ToList();

    public Task<Category?> GetCategoryAsync(int id) => categories.GetByIdAsync(id);

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        TrimCategory(category);
        await categories.AddAsync(category);
        await categories.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateCategoryAsync(int id, Category category)
    {
        var existingCategory = await categories.GetByIdAsync(id);
        if (existingCategory is null)
        {
            return null;
        }

        existingCategory.Name = category.Name.Trim();
        existingCategory.Description = category.Description?.Trim();

        categories.Update(existingCategory);
        await categories.SaveChangesAsync();
        return existingCategory;
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

    public async Task<IReadOnlyList<Book>> GetBooksAsync() =>
        (await books.GetAllWithDetailsAsync()).ToList();

    public Task<Book?> GetBookAsync(int id) => books.GetByIdWithDetailsAsync(id);

    public Task<IReadOnlyList<Book>> SearchBooksByCategoryAsync(string? category) =>
        books.SearchByCategoryAsync(category);

    public async Task<ServiceResult<Book>> CreateBookAsync(Book book)
    {
        var validationError = await ValidateBookAsync(book);
        if (validationError is not null)
        {
            return ServiceResult<Book>.Fail(validationError);
        }

        TrimBook(book);
        await books.AddAsync(book);
        await books.SaveChangesAsync();

        var createdBook = await books.GetByIdWithDetailsAsync(book.Id);
        return ServiceResult<Book>.Ok(createdBook ?? book);
    }

    public async Task<ServiceResult<Book>> UpdateBookAsync(int id, Book book)
    {
        var existingBook = await books.GetByIdAsync(id);
        if (existingBook is null)
        {
            return ServiceResult<Book>.Fail("El libro indicado no existe.");
        }

        var validationError = await ValidateBookAsync(book);
        if (validationError is not null)
        {
            return ServiceResult<Book>.Fail(validationError);
        }

        existingBook.Title = book.Title.Trim();
        existingBook.Isbn = book.Isbn.Trim();
        existingBook.PublicationYear = book.PublicationYear;
        existingBook.TotalCopies = book.TotalCopies;
        existingBook.AvailableCopies = book.AvailableCopies;
        existingBook.Status = book.Status;
        existingBook.AuthorId = book.AuthorId;
        existingBook.CategoryId = book.CategoryId;

        books.Update(existingBook);
        await books.SaveChangesAsync();

        var updatedBook = await books.GetByIdWithDetailsAsync(id);
        return ServiceResult<Book>.Ok(updatedBook ?? existingBook);
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

    public async Task<IReadOnlyList<Member>> GetMembersAsync() =>
        (await members.GetAllAsync()).OrderBy(member => member.LastName).ToList();

    public Task<Member?> GetMemberAsync(int id) => members.GetByIdAsync(id);

    public async Task<Member> CreateMemberAsync(Member member)
    {
        TrimMember(member);
        await members.AddAsync(member);
        await members.SaveChangesAsync();
        return member;
    }

    public async Task<Member?> UpdateMemberAsync(int id, Member member)
    {
        var existingMember = await members.GetByIdAsync(id);
        if (existingMember is null)
        {
            return null;
        }

        existingMember.FirstName = member.FirstName.Trim();
        existingMember.LastName = member.LastName.Trim();
        existingMember.Email = member.Email.Trim();
        existingMember.Phone = member.Phone?.Trim();
        existingMember.IsActive = member.IsActive;

        members.Update(existingMember);
        await members.SaveChangesAsync();
        return existingMember;
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

    public Task<IReadOnlyList<Loan>> GetLoanHistoryAsync() => loans.GetHistoryAsync();

    public Task<IReadOnlyList<Loan>> GetLoanHistoryByMemberAsync(int memberId) =>
        loans.GetHistoryByMemberAsync(memberId);

    public async Task<ServiceResult<Loan>> CreateLoanAsync(int bookId, int memberId, DateOnly dueDate)
    {
        var book = await books.GetByIdAsync(bookId);
        if (book is null)
        {
            return ServiceResult<Loan>.Fail("El libro indicado no existe.");
        }

        if (book.AvailableCopies <= 0)
        {
            return ServiceResult<Loan>.Fail("No hay copias disponibles para prestar.");
        }

        var member = await members.GetByIdAsync(memberId);
        if (member is null || !member.IsActive)
        {
            return ServiceResult<Loan>.Fail("El miembro no existe o esta inactivo.");
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (dueDate < today)
        {
            return ServiceResult<Loan>.Fail("La fecha de entrega no puede ser anterior a hoy.");
        }

        var loan = new Loan
        {
            BookId = bookId,
            MemberId = memberId,
            LoanDate = today,
            DueDate = dueDate,
            Status = LoanStatus.Active
        };

        book.AvailableCopies--;
        await loans.AddAsync(loan);
        await loans.SaveChangesAsync();

        var createdLoan = await loans.GetByIdWithDetailsAsync(loan.Id);
        return ServiceResult<Loan>.Ok(createdLoan ?? loan);
    }

    public async Task<ServiceResult<Loan>> ReturnLoanAsync(int id, DateOnly? returnDate)
    {
        var loan = await loans.GetByIdWithDetailsAsync(id);
        if (loan is null)
        {
            return ServiceResult<Loan>.Fail("El prestamo indicado no existe.");
        }

        if (loan.ReturnDate.HasValue)
        {
            return ServiceResult<Loan>.Fail("Este prestamo ya fue devuelto.");
        }

        var finalReturnDate = returnDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
        if (finalReturnDate < loan.LoanDate)
        {
            return ServiceResult<Loan>.Fail("La fecha de devolucion no puede ser anterior al prestamo.");
        }

        var lateDays = Math.Max(0, finalReturnDate.DayNumber - loan.DueDate.DayNumber);
        loan.ReturnDate = finalReturnDate;
        loan.LateFee = lateDays * DailyLateFee;
        loan.Status = LoanStatus.Returned;

        if (loan.Book is not null)
        {
            loan.Book.AvailableCopies++;
        }

        loans.Update(loan);
        await loans.SaveChangesAsync();

        return ServiceResult<Loan>.Ok(loan);
    }

    private async Task<string?> ValidateBookAsync(Book book)
    {
        if (book.TotalCopies < 1 || book.AvailableCopies < 0 || book.AvailableCopies > book.TotalCopies)
        {
            return "Las copias disponibles deben estar entre 0 y el total de copias.";
        }

   
        if (!await authors.ExistsAsync(book.AuthorId) || !await categories.ExistsAsync(book.CategoryId))
        {
            return "El autor o la categoria indicada no existe.";
        }

        return null;
    }

    private static void TrimAuthor(Author author)
    {
        author.FirstName = author.FirstName.Trim();
        author.LastName = author.LastName.Trim();
        author.Nationality = author.Nationality?.Trim();
        author.Biography = author.Biography?.Trim();
    }

    private static void TrimCategory(Category category)
    {
        category.Name = category.Name.Trim();
        category.Description = category.Description?.Trim();
    }

    private static void TrimBook(Book book)
    {
        book.Title = book.Title.Trim();
        book.Isbn = book.Isbn.Trim();
    }

    private static void TrimMember(Member member)
    {
        member.FirstName = member.FirstName.Trim();
        member.LastName = member.LastName.Trim();
        member.Email = member.Email.Trim();
        member.Phone = member.Phone?.Trim();
    }
}