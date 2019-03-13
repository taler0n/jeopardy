
namespace Game
{
    public class Setting
    {
        public int Value { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public Setting()
        { }

        public Setting(int value, int min, int max)
        {
            Value = value;
            Minimum = min;
            Maximum = max;
        }
    }

    public class Settings
    {
        public Setting NumberOfPlayers { get; set; }
        public Setting RoundSize { get; set; }
        public Setting ThemeSize { get; set; }
        public Setting FinalSize { get; set; }
        public Setting FirstQuestionValue { get; set; }
        public Setting QuestionStepValue { get; set; }
        public Setting MaxThemeNameLength { get; set; }
        public Setting MaxQuestionTextLength { get; set; }

        public Settings()
        {
            NumberOfPlayers = new Setting(3, 2, 6);
            RoundSize = new Setting(6, 1, 12);
            ThemeSize = new Setting(5, 1, 10);
            FinalSize = new Setting(7, 2, 14);

            FirstQuestionValue = new Setting(100, 1, 100000000);
            QuestionStepValue = new Setting(100, 1, 100000000);
            MaxThemeNameLength = new Setting(16, 1, 32);
            MaxQuestionTextLength = new Setting(200, 1, 400);
        }

    }
}
