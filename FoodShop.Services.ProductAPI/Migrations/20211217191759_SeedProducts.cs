using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodShop.Services.ProductAPI.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://sun9-65.userapi.com/impg/uMwaHfgtu6vBHkn_r0ZxPXOU5mJWXhtQZw9wZQ/y7LDQfAsiAY.jpg?size=875x500&quality=96&sign=dfa84ac8d692a38152f75e469f9e864e&type=album", "Samosa", 15.0 },
                    { 2, "Appetizer", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://sun9-84.userapi.com/impg/m9CuWi3UuMQ-wMFbg8roDSMz5jJVpaiprDDJyA/yr6m-XUHdkI.jpg?size=875x500&quality=96&sign=4637354546a08303b538a9cc4840d1d5&type=album", "Paneer Tikka", 13.99 },
                    { 3, "Dessert", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://sun9-7.userapi.com/impg/aErLL5-pGUWn6a3Il8nGJxa4Kf82fQgOpQ1rpg/XPPf8GhCraA.jpg?size=875x500&quality=96&sign=f174764e6b7013815d5129e42090d307&type=album", "Sweet Pie", 10.99 },
                    { 4, "Entree", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://sun9-78.userapi.com/impg/qSo4-6YIg02GyvfDQ8_1Dv2oY-vOy8Dd3h4F0w/Hg-0G_eiXrU.jpg?size=875x500&quality=96&sign=907b6558594be92c6a956a83e56c1348&type=album", "Pav Bhaji", 15.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
