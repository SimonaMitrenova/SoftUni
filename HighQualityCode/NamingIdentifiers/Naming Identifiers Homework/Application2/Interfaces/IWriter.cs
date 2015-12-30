namespace MineGame.Interfaces
{
    public interface IWriter
    {
        void Write(object message);

        void Write(string format, params object[] args);

        void WriteLine(object message);

        void WriteLine(string format, params object[] args);
    }
}
