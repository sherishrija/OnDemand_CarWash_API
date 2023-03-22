using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Models
{
    public class CarWashDbContext : DbContext
    {
        public CarWashDbContext(DbContextOptions<CarWashDbContext> options) : base(options)
        {


        }
        public DbSet<UserModel> Usertable { get; set; }
        public DbSet<CarModel> CarModeltbl { get; set; }
        public DbSet<CarPackageModel> PackageTable { get; set; }
        public DbSet<OrderModel>OrderTable { get; set; }
    }
}
