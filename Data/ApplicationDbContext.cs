using Microsoft.EntityFrameworkCore;
using Student_Portal__CRUD_Ops_.Models.Entities;




namespace Student_Portal__CRUD_Ops_.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
