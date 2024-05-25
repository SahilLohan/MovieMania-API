using System;

namespace MovieMania.CustomExceptions
{
    public class InvalidRequestObjectException:Exception
    {
        public InvalidRequestObjectException(string message):base(message)
        {
            
        }
    }
}
