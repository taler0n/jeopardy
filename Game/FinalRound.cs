
namespace Game
{
    public class FinalRound
    {
        public FinalQuestion[] Questions { get; set; }
        
        public FinalRound()
        { }

        public FinalRound(Settings settings)
        {
            Questions = new FinalQuestion[settings.FinalSize.Value];

            for (int i = 0; i < settings.FinalSize.Value; i++)
            {
                Questions[i] = new FinalQuestion();
            }
        }

        public FinalQuestion this[int index]
        {
            get { return Questions[index]; }
            set { Questions[index] = value; }
        }

        public bool IsReady()
        {
            foreach (var item in Questions)
            {
                if (item == null || !item.IsReady())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
