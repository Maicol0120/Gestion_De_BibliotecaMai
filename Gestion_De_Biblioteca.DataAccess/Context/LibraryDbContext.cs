using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(author => author.FirstName).HasMaxLength(80).IsRequired();
            entity.Property(author => author.LastName).HasMaxLength(80).IsRequired();
            entity.Property(author => author.Nationality).HasMaxLength(80);
            entity.Property(author => author.Biography).HasMaxLength(1000);
            entity.Property(author => author.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(category => category.Name).IsUnique();
            entity.Property(category => category.Name).HasMaxLength(80).IsRequired();
            entity.Property(category => category.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(book => book.Isbn).IsUnique();
            entity.Property(book => book.Title).HasMaxLength(160).IsRequired();
            entity.Property(book => book.Isbn).HasMaxLength(20).IsRequired();
            entity.Property(book => book.TotalCopies).HasDefaultValue(1);
            entity.Property(book => book.AvailableCopies).HasDefaultValue(1);
            entity.Property(book => book.Status).HasConversion<int>().HasDefaultValue(BookStatus.Available);

            entity.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(book => book.Category)
                .WithMany(category => category.Books)
                .HasForeignKey(book => book.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasIndex(member => member.Email).IsUnique();
            entity.Property(member => member.FirstName).HasMaxLength(80).IsRequired();
            entity.Property(member => member.LastName).HasMaxLength(80).IsRequired();
            entity.Property(member => member.Email).HasMaxLength(120).IsRequired();
            entity.Property(member => member.Phone).HasMaxLength(30);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.Property(loan => loan.LateFee).HasPrecision(10, 2);

            entity.HasOne(loan => loan.Book)
                .WithMany(book => book.Loans)
                .HasForeignKey(loan => loan.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(loan => loan.Member)
                .WithMany(member => member.Loans)
                .HasForeignKey(loan => loan.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
