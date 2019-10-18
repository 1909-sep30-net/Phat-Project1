using System;
using System.Collections.Generic;

namespace StoreApp.Data.Entities
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Orders>();
        }

        public int StoreId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int Ariel { get; set; }
        public int Downie { get; set; }
        public int Suavitel { get; set; }

        public virtual Manager Manager { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
