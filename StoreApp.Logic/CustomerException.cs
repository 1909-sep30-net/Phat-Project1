using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    /// <summary>
    /// Exception related to the BusinessCustomer class.
    /// </summary>
    public class CustomerException : Exception
    {
        /// <summary>
        /// Base exception constructor
        /// </summary>
        /// <param name="message">Message of the exception</param>
        public CustomerException(string message) : base(message)
        {
        }
    }
}
