using System;
using System.Collections.Generic;

namespace StoreApp.Data
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int Ariel { get; set; }
        public int Downie { get; set; }
        public int Suavitel { get; set; }

        public ICollection<Orders> Orders { get; set; }

    }
}
