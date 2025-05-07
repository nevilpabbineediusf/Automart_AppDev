namespace Automart.Models
{
   using Microsoft.EntityFrameworkCore;

public class CarContext : DbContext
{
    public CarContext(DbContextOptions<CarContext> options) : base(options) { }

    public DbSet<Auth> Auth { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Dealers> Dealers { get; set; }

    public DbSet<NewCar> NewCars { get; set; }
    public DbSet<UsedCar> UsedCars { get; set; }


        // Add later: public DbSet<UsedCars> UsedCars { get; set; }
        // Add later: public DbSet<NewCars> NewCars { get; set; }
    }


}
