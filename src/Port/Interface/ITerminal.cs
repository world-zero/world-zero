namespace WorldZero.Port.Interface
{
    /// <summary>
    /// This interface defines a simple IO interface.
    /// </summary>
    public interface ITerminal
    {
        /// <summary>
        /// This method will prompt the user for the supplied generic type.
        /// This should allow nulls to be returned, if appropriate.
        /// </summary>
        T Prompt<T>(string message=null);

        /// <summary>
        /// This method will prompt the user for the supplied generic type.
        /// This should not allow nulls to be returned, if appropriate.
        /// </summary>
        T PromptNotNull<T>(string message=null);

        /// <summary>
        /// This method will display the message in a way that the
        /// implementation deems most appropriate.
        /// </summary>
        void Print(string message=null);

        /// <summary>
        /// This method will display the supplied type and the message in a way
        /// that the implementation deems most appropriate.
        /// </summary>
        void Print<T>(T item, string message=null);
    }
}