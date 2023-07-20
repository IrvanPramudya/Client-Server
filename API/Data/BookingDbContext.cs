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
            modelBuilder.Entity<Education>().HasOne<University>(e => e.University)
                        .WithMany(u => u.Educations).HasForeignKey(u=>u.UniversityGuid);
            //Many Booking with One Room
            modelBuilder.Entity<Booking>().HasOne(r => r.Room).WithMany(b => b.Bookings).HasForeignKey(b => b.RoomGuid);
            //Employee
            modelBuilder.Entity<Booking>().HasOne(e => e.Employee).WithMany(b => b.Bookings).HasForeignKey(b => b.EmployeeGuid);
            modelBuilder.Entity<Employee>().HasOne(e => e.Education).WithOne(em => em.Employee).HasForeignKey<Employee>(em => em.Guid);
            modelBuilder.Entity<Employee>().HasOne(a => a.Account).WithOne(em => em.Employee).HasForeignKey<Employee>(em => em.Guid);
            //Account Roles
            modelBuilder.Entity<AccountRole>().HasOne(ar => ar.Account).WithMany(ac => ac.AccountRoles).HasForeignKey(ac => ac.AccountGuid);
            modelBuilder.Entity<AccountRole>().HasOne(ar => ar.Role).WithMany(r => r.AccountRoles).HasForeignKey(ac => ac.RoleGuid);
            

        }
    }
}
