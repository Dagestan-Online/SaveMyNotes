using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Save_My_Notes.Classes;

namespace Save_My_Notes
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=NotesDataBase.db");
        }
    }
}
