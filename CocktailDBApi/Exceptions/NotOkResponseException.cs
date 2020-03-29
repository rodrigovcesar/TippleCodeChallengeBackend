using System;

namespace CocktailDBApi.Exceptions
{
    public class NotOkResponseException : Exception
    {
        public NotOkResponseException(string message) : base(message)
        {

        }
    }
}
