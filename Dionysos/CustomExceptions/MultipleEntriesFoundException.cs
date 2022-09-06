namespace Dionysos.CustomExceptions;

public class MultipleEntriesFoundException : Exception
{
    public MultipleEntriesFoundException()
    {
        
    }

    public MultipleEntriesFoundException(string message)
    : base(message)
    {
        
    }

    public MultipleEntriesFoundException(string message, Exception inner)
    : base(message, inner)
    {
        
    }
}
