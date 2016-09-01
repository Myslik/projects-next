namespace Architecture.Core
{
    public abstract class MessageHandler
    {
        public IBus Bus { get; internal set; }
    }
}
