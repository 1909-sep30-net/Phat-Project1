using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    public class Store
    {

            public Address address = new Address();
            public Inventory storeInventory = new Inventory();
            public int storeId { get; set; }
            public bool CheckLocationNotNull()
            {
                if (this.storeInventory.CheckInventoryNotNull() == true && this.storeId > 0 && this.address.IsAddressNotNull() == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
    }

}

