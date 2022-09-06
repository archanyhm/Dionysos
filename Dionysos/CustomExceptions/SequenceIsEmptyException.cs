namespace Dionysos.CustomExceptions;

public class SequenceIsEmptyException : Exception
{
    public SequenceIsEmptyException()
    {
        
    }

    public SequenceIsEmptyException(string message)
    : base(message)
    {
        
    }

    public SequenceIsEmptyException(string message, Exception inner)
    : base(message, inner)
    {
        
    }
}
