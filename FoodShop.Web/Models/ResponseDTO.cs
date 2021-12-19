﻿namespace FoodShop.Web.Models
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; } = true;

        public object Result { get; set; }

        public string DisplayMessage{ get; set; } = "";

        public List<string> ErrorMessages { get; set; }
    }
}
