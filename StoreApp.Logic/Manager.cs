using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    public class Manager
    {
   
            public string firstName { get; set; }
            public string lastName { get; set; }
            public int managerID { get; set; }

            
            public void SetManagerFirstName(string name)
            {
                this.firstName = name;
            }
            public void SetManagerLastName(string name)
            {
                this.lastName = name;
            }
      
    }
}
