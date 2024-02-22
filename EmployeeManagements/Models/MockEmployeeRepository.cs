
namespace EmployeeManagements.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository() 
        {
            _employeeList = new List<Employee>()
         {
             new Employee() {Id=1, Name= "Dela", Email ="della@gmail.com", Department= Dept.HR, PhotoPath="~/image/female1.jpg"},
             new Employee() {Id=2, Name= "John", Email ="john02@gmail.com", Department= Dept.IT , PhotoPath = "~/image/employee4.jpg"},
             new Employee() {Id=3, Name= "Bella", Email = "bella@yahoo.com", Department= Dept.IT, PhotoPath = "~/image/employeeteam.png"},
             new Employee() {Id=4, Name= "Vivian", Email ="vivi@gmail.com", Department= Dept.HR, PhotoPath= "~/image/employee3.png"}
         };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e=>e.Id) + 1;
           _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e=>e.Id == id);    
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
