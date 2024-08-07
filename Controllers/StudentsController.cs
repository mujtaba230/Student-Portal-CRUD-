using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using Student_Portal__CRUD_Ops_.Data;
using Student_Portal__CRUD_Ops_.Models;
using Student_Portal__CRUD_Ops_.Models.Entities;

namespace Student_Portal__CRUD_Ops_.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            { 
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            
            
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            //var viewModel = new AddStudentViewModel
            //{
            //    Name = student.Name,
            //    Email = student.Email,
            //    Phone = student.Phone,
            //    Subscribed = student.Subscribed
            //};
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
           var STUDENT = await dbContext.Students.FindAsync(viewModel.Id);

            if(STUDENT is not null)
            {
                STUDENT.Name = viewModel.Name;
                STUDENT.Email = viewModel.Email;
                STUDENT.Phone = viewModel.Phone;
                STUDENT.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }




            return RedirectToAction("List", "Students");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student Obj)
        {
            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id  == Obj.Id);
            if(student is not null)
            {
                dbContext.Students.Remove(Obj);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
        [HttpPost]
        public async Task<IActionResult> Save(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };


            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Students");
        }


    }
}
