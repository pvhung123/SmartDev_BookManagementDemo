using BookService.Application.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookService.Application.Data
{
    public class BookManagementContext : DbContext
    {
        public BookManagementContext(DbContextOptions<BookManagementContext> options) : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<UserBook>().ToTable("UserBook");            

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookId);

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.User)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.UserId);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Admin", Fullname = "Administrator", Address = "HCM", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new User { Id = 2, Username = "Hungpv", Fullname = "Pham Van Hung", Address = "DN", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "C# Book", Author = "Author 1", Description = "C# book description", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Book { Id = 2, Title = "Java", Author = "Author 2", Description = "Java book description", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Book { Id = 3, Title = "PHP", Author = "Author 3", Description = "PHP book description", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
                );

            modelBuilder.Entity<UserBook>().HasData(
                new UserBook { Id = 1, UserId = 1, BookId = 1, ReadingStatus = 2, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new UserBook { Id = 2, UserId = 1, BookId = 2, ReadingStatus = 2, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new UserBook { Id = 3, UserId = 1, BookId = 3, ReadingStatus = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
                );
        }
    }
}
