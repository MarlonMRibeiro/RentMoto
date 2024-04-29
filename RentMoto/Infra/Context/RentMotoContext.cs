using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Context
{
    public class RentMotoContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public RentMotoContext(DbContextOptions<RentMotoContext> options) : base(options) { }

        public DbSet<User> User { get; set; }    
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Motorcycle> Motorcycle { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderNotification> OrderNotification { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Rental> Rental { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plan>().HasData(
                Domain.Entities.Plan.Create().SetDays(7).SetDailyPrice(30).SetPenaltyPercentage((decimal)0.2),
                Domain.Entities.Plan.Create().SetDays(15).SetDailyPrice(28).SetPenaltyPercentage((decimal)0.4),
                Domain.Entities.Plan.Create().SetDays(30).SetDailyPrice(22).SetPenaltyPercentage((decimal)0.6)
            );

        }

    }
}
