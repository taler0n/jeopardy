
namespace Game
{
    public class Player
    {
        public string Name { get; private set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
