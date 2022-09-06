namespace Dionysos.CustomExceptions;

public class ObjectDoesNotExistException : Exception
{
    public ObjectDoesNotExistException()
    {
    }
    
    public ObjectDoesNotExistException(string message)
        : base(message)
    {
    }
    
    public ObjectDoesNotExistException(string message, Exception inner)
        : base(message, inner)
    {
        
    }
}
