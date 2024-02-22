using Microsoft.EntityFrameworkCore;

namespace EmployeeManagements.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
              new Employee() { Id = 1, Name = "Dela", Email = "della@gmail.com", Department = Dept.HR, PhotoPath = "~/image/female1.jpg" },
             new Employee() { Id = 2, Name = "John", Email = "john02@gmail.com", Department = Dept.IT, PhotoPath = "~/image/employee4.jpg" },
             new Employee() { Id = 3, Name = "Bella", Email = "bella@yahoo.com", Department = Dept.IT, PhotoPath = "~/image/employeeteam.png" },
             new Employee() { Id = 4, Name = "Vivian", Email = "vivi@gmail.com", Department = Dept.HR, PhotoPath = "~/image/female2.jpg" }
              );
        }
    }
}
