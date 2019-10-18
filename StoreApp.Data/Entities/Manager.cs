using System;
using System.Collections.Generic;

namespace StoreApp.Data.Entities
{
    public partial class Manager
    {
        public int ManagerId { get; set; }
        public int? StoreId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Store Store { get; set; }
    }
}
