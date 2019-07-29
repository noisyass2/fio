using System;

public class GenericEventArgs : EventArgs
{
    public string Message { get; }

    public GenericEventArgs(string message)
    {
        Message = message;
    }
}
