using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailDBApi.Exceptions
{
    public class NotOkResponseException : Exception
    {
        public NotOkResponseException(string message) : base(message)
        {

        }
    }
}
