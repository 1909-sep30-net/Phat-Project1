using System;
using System.Collections.Generic;

namespace StoreApp.Data.Entities
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public int Ariel { get; set; }
        public int Downie { get; set; }
        public int Suavitel { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Store { get; set; }
    }
}
