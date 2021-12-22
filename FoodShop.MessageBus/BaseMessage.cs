using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.MessageBus
{
    public class BaseMessage
    {
        public int Id { get; set; }

        public DateTime MessageCreated { get; set; }
    }
}
