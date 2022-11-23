using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M.DataAccessLayer
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base("DBConnection") { }

        public DbSet<Student> students { get; set; }
    }
}
