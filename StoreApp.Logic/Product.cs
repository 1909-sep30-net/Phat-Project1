using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    public class Product
    {
     
            public string ariel = "Ariel";
            public int NumberofAriel { get; set; }
            public string downie = "Downie";
            public int NumberofDownie { get; set; }
            public string suavitel = "Suavitel";
            public int NumberofSuavitel { get; set; }

            public bool IsNumberofProductValid()
            {
                if (NumberofAriel < 0 && NumberofDownie < 0 && NumberofSuavitel < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
    }
}
