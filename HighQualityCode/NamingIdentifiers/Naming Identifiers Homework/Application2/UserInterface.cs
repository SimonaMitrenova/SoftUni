using System;
using MineGame.Interfaces;

namespace MineGame
{
    class UserInterface : IUserInterface
    {
        public void Write(object message)
        {
            Console.Write(message);
        }

        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void WriteLine(object message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public string ReadLine()
        {
            string input = Console.ReadLine();
            return input;
        }
    }
}
