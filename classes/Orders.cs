using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        public int Ariel { get; set; }
        public int Downie { get; set; }
        public int Suavitel { get; set; }

        public  Customer Customer { get; set; }
        public Store Store { get; set; }
    }
}
