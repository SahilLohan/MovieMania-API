using System;

namespace MovieMania.CustomExceptions
{
    public class IdNotExistException:Exception
    {
        public IdNotExistException(string message) : base(message)
        {
            
        }
    }
}
