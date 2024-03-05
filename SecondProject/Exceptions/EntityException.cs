namespace SecondProject.Exceptions
{
    public abstract class EntityException : System.Exception
    {
        public EntityException(string message) : base(message) { }

    }
    public class EntityNotFoundException : EntityException { 
        public EntityNotFoundException(string message) : base(message) { }
    }
    public class EntityValidationException : EntityException
    {
        public EntityValidationException(string message) : base(message) { }

    }

    public class IncorrectCredentialsException :EntityException
    {
        public IncorrectCredentialsException(string message) : base(message) { }    

    }
}
