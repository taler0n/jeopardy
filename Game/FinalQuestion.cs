
namespace Game
{
    public class FinalQuestion : Question
    {
        private const string _defaultName = "имя_вопроса";
        public string Name { get; set; }

        public FinalQuestion()
        {
            Name = _defaultName;
        }
    }
}
