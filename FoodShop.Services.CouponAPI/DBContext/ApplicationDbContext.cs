using FoodShop.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.CouponAPI.DBContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Coupon> Coupons { get; set; }

    }
}
