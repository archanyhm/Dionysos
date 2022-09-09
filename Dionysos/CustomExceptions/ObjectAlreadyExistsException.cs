namespace Dionysos.CustomExceptions;

public class ObjectAlreadyExistsException : Exception
{
    public ObjectAlreadyExistsException()
    {
    }
    
    public ObjectAlreadyExistsException(string message)
    : base (message)
    {
    }
    
    public ObjectAlreadyExistsException(string message, Exception inner)
    : base(message, inner)
    {
        
    }
}
