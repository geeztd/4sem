using lab11.MVVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab11 {
    public class DB : DbContext {
        public DbSet<Consultation> consultations { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}
