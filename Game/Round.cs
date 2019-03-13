
namespace Game
{
    public class Round
    {
        public Theme[] Themes { get; set; }

        public Round()
        { }

        public Round(Settings settings)
        {
            Themes = new Theme[settings.RoundSize.Value];

            for (int i = 0; i < settings.RoundSize.Value; i++)
            {
                Themes[i] = new Theme(settings);

                for (int j = 0; j < settings.ThemeSize.Value; j++)
                {
                    Themes[i][j] = new Question();
                }
            }
        }

        public Theme this[int index]
        {
            get { return Themes[index]; }
            set { Themes[index] = value; }
        }

        public bool IsReady()
        {
            foreach (var item in Themes)
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
