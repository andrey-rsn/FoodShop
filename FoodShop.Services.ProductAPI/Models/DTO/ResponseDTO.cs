namespace FoodShop.Services.ProductAPI.Models.DTO
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; } = true;

        public object Result { get; set; }

        public string DisplayMessgae { get; set; } = "";

        public List<string> ErrorMessages { get; set; }
    }
}
