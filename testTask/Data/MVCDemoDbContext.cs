using Microsoft.EntityFrameworkCore;
using testTask.Models;

namespace testTask.Data
{
    public class MVCDemoDbContext : DbContext
    {

        // Объявляем конструктор класса 
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        // Подключаем таблицу, которая описана в классе Employee
        public DbSet<Employee> Employee { get; set; }

        // Подключаем таблицу, которая описана в классе Department
        public DbSet<Department> Department { get; set; }

        // override переписываем метод у класса родителя и описываем связь между таблицами
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Department)
                .WithMany()
                .HasForeignKey(t => t.DepartmentId);
        }

    }
}
