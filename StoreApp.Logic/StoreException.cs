using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    /// <summary>
    /// Exception related to the BusinessLocation class.
    /// </summary>
    public class StoreException : Exception
    {
        /// <summary>
        /// Base exception constructor
        /// </summary>
        /// <param name="message">Message of the exception</param>
        public StoreException(string message) : base(message)
        {
        }
    }
}
