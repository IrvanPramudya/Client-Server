using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //DbContext merupakan Constructor yang tersedia pada EntityFrameworkCore
    public class BookingDbContext : DbContext
    {

        public BookingDbContext(DbContextOptions<BookingDbContext> options): base(options) { }
        //Dbset digunakan untuk mendaftarkan tabel yang dibuat pada model ke database
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(e => new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();
            //Many Education with One University
            modelBuilder.Entity<Education>()
                        .HasOne<University>         (Education  => Education.University)
                        .WithMany                   (University => University.Educations)
                        .HasForeignKey              (University => University.UniversityGuid)
                        .OnDelete                   (DeleteBehavior.Restrict);
            //Many Booking with One Room
            modelBuilder.Entity<Booking>()
                        .HasOne                     (Booking    => Booking.Room)
                        .WithMany                   (Room       => Room.Bookings)
                        .HasForeignKey              (Booking    => Booking.RoomGuid)
                        .OnDelete                   (DeleteBehavior.Restrict);
            //Employee
            modelBuilder.Entity<Booking>()
                        .HasOne                     (Booking    => Booking.Employee)
                        .WithMany                   (Employee   => Employee.Bookings)
                        .HasForeignKey              (Booking    => Booking.EmployeeGuid)
                        .OnDelete                   (DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>()
                        .HasOne                     (Employee   => Employee.Education)
                        .WithOne                    (Education  => Education.Employee)
                        .HasForeignKey<Education>   (Employee   => Employee.Guid)
                        .OnDelete                   (DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>()
                        .HasOne                     (Employee   => Employee.Account)
                        .WithOne                    (Account    => Account.Employee)
                        .HasForeignKey<Account>     (Employee   => Employee.Guid)
                        .OnDelete                   (DeleteBehavior.Restrict);
            //Account Roles
            modelBuilder.Entity<AccountRole>()
                        .HasOne                     (AccountRole => AccountRole.Account)
                        .WithMany                   (Account     => Account.AccountRoles)
                        .HasForeignKey              (AccountRole => AccountRole.AccountGuid)
                        .OnDelete                   (DeleteBehavior.Restrict);
            modelBuilder.Entity<AccountRole>()
                        .HasOne                     (AccountRole => AccountRole.Role)
                        .WithMany                   (Role        => Role.AccountRoles)
                        .HasForeignKey              (AccountRole => AccountRole.RoleGuid)
                        .OnDelete                   (DeleteBehavior.Restrict);
            

        }
    }
}
