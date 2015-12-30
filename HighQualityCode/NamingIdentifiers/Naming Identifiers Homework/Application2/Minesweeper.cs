using MineGame.Interfaces;

namespace MineGame
{
    public class Minesweeper
    {
        private static void Main()
        {   
            IUserInterface userInterface = new UserInterface();
            IDatabase data = new Database();
            IEngine engine = new Engine(data, userInterface);
            engine.Run();
        }
    }
}