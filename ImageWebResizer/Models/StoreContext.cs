using Microsoft.EntityFrameworkCore;


namespace ImageWebResizer.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {
            //Database.Migrate();
        }

        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source=store.db");
        }
    }

}


