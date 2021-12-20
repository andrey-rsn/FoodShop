using FoodShop.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.ProductAPI.DBContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base (options)
        {

        }

        public DbSet<Product> Products { get; set; }

       //protected override void OnModelCreating (ModelBuilder modelBuilder)
       //{
       //    base.OnModelCreating(modelBuilder);
       //
       //
       //    modelBuilder.Entity<Product>().HasData(new Product
       //    {
       //        ProductId = 1,
       //        Name = "Samosa",
       //        Price = 15,
       //        Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
       //        ImageUrl = "https://sun9-65.userapi.com/impg/uMwaHfgtu6vBHkn_r0ZxPXOU5mJWXhtQZw9wZQ/y7LDQfAsiAY.jpg?size=875x500&quality=96&sign=dfa84ac8d692a38152f75e469f9e864e&type=album",
       //        CategoryName = "Appetizer"
       //    });
       //    modelBuilder.Entity<Product>().HasData(new Product
       //    {
       //        ProductId = 2,
       //        Name = "Paneer Tikka",
       //        Price = 13.99,
       //        Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
       //        ImageUrl = "https://sun9-84.userapi.com/impg/m9CuWi3UuMQ-wMFbg8roDSMz5jJVpaiprDDJyA/yr6m-XUHdkI.jpg?size=875x500&quality=96&sign=4637354546a08303b538a9cc4840d1d5&type=album",
       //        CategoryName = "Appetizer"
       //    });
       //    modelBuilder.Entity<Product>().HasData(new Product
       //    {
       //        ProductId = 3,
       //        Name = "Sweet Pie",
       //        Price = 10.99,
       //        Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
       //        ImageUrl = "https://sun9-7.userapi.com/impg/aErLL5-pGUWn6a3Il8nGJxa4Kf82fQgOpQ1rpg/XPPf8GhCraA.jpg?size=875x500&quality=96&sign=f174764e6b7013815d5129e42090d307&type=album",
       //        CategoryName = "Dessert"
       //    });
       //    modelBuilder.Entity<Product>().HasData(new Product
       //    {
       //        ProductId = 4,
       //        Name = "Pav Bhaji",
       //        Price = 15,
       //        Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
       //        ImageUrl = "https://sun9-78.userapi.com/impg/qSo4-6YIg02GyvfDQ8_1Dv2oY-vOy8Dd3h4F0w/Hg-0G_eiXrU.jpg?size=875x500&quality=96&sign=907b6558594be92c6a956a83e56c1348&type=album",
       //        CategoryName = "Entree"
       //    });
       //}
    }
}
