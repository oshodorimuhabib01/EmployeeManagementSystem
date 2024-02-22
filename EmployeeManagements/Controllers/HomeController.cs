using EmployeeManagements.Models;
using EmployeeManagements.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeManagements.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository, 
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            
                if (employee==null)
                {
                    Response.StatusCode = 404;
                    return View("EmployeeNotFound", id.Value);
                }
            

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            //Employee model = _employeeRepository.GetEmployee(1);
            //ViewBag.PageTitle = "Employee Details";
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Create()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email, 
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "image", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    string uniqueFileName = ProcessUploadedFile(model);
                    employee.PhotoPath = uniqueFileName; // Assuming there's a property like PhotoPath in the Employee entity to store the file name
                }

                _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    DateTime now = DateTime.Now;
                    string formattedDateTime = now.ToString("yyyyMMddHHmmss");
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "image");
                    Directory.CreateDirectory(uploadsFolder);
                    uniqueFileName = $"{formattedDateTime}_{photo.FileName}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { Id = newEmployee.Id });
            }
            return View();
        }

        
    }
}
