using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ImageWebResizer.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=store.db");
        }
    }

}


