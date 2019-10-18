using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    public class Inventory
    {
        public Product items = new Product();

        public bool CheckInventoryNotNull()
        {
            if (items.IsNumberofProductValid() == true)
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
