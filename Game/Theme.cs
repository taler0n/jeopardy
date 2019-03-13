
namespace Game
{
    public class Theme
    {
        private const string _defaultName = "имя_темы";

        public string Name { get; set; }
        public Question[] Questions { get; set; }
        public int[] QuestionValues { get; set; }

        public Theme()
        { }

        public Theme(Settings settings)
        {
            Name = _defaultName;
            Questions = new Question[settings.ThemeSize.Value];
            QuestionValues = new int[settings.ThemeSize.Value];

            int currentValue = settings.FirstQuestionValue.Value;

            for (int i = 0; i < settings.ThemeSize.Value; i++)
            {
                QuestionValues[i] = currentValue;
                currentValue += settings.QuestionStepValue.Value;
            }
        }

        public void SetMultiplier(int pointMultiplier, Settings settings)
        {
            int currentValue = settings.FirstQuestionValue.Value * pointMultiplier;

            for (int i = 0; i < settings.ThemeSize.Value; i++)
            {
                QuestionValues[i] = currentValue;
                currentValue += settings.FirstQuestionValue.Value * pointMultiplier;
            }
        }

        public Question this[int index]
        {
            get { return Questions[index]; }
            set { Questions[index] = value; }
        }

        public int GetQuestionValue(int index)
        {
            return QuestionValues[index];
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
