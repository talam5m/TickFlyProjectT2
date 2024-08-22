using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace DataAccessLayer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserRole> Roles { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<TicketComment> TicketComments { get; set; } = null!;
        public DbSet<Attachment> Attachments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId);
            modelBuilder.Entity<TicketComment>()
                .HasOne(tc => tc.Ticket)
                .WithMany(t => t.TicketComments)
                .HasForeignKey(tc => tc.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = Guid.Parse("1D460004-DFDD-4C24-8F60-CB52299A4096"),
                    Name = "Client"
                },
                new UserRole
                {
                    Id = Guid.Parse("077FF91C-7598-416E-9AF9-1B484B85D410"),
                    Name = "Support"
                },
                new UserRole
                {
                    Id = Guid.Parse("C450E249-BAB0-471C-AA59-6A851C769C5C"),
                    Name = "SupportManager"
                }
            );

            var password = new PasswordHasher<User>();
            var hashPassword = password.HashPassword(null, "Supportanagerjamil2003@");

            // Add Support Manger 
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("6FEB7AE5-BD2F-48EC-B004-FCE99CC30F8E"),
                FirstName = "Jamil",
                LastName = "Dora",
                Email = "supportmanager@gmail.com",
                Password = hashPassword,
                MobileNumber = "0502673827",
                DateOfBirth = new DateTime(1988, 7, 2),
                Address = "Riyadh",
                IsActive = true,
                RoleId = Guid.Parse("C450E249-BAB0-471C-AA59-6A851C769C5C"),
            });
        }
    }
}