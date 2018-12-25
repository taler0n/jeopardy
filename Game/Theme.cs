using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Theme
    {
        private const int _defaultFirstQuestionValue = 100;
        private const int _defaultQuestionValueStep = 100;
        public const int DefaultSize = 5;

        public string Name { get; set; }
        public Question[] Questions { get; set; }
        public int[] QuestionValues { get; set; }

        public Theme()
        {
            Name = String.Empty;
            Questions = new Question[DefaultSize];
            QuestionValues = new int[DefaultSize];

            int currentValue = _defaultFirstQuestionValue;

            for (int i = 0; i < DefaultSize; i++)
            {
                QuestionValues[i] = currentValue;
                currentValue += _defaultQuestionValueStep;
            }
        }

        public void SetMultiplier(int pointMultiplier)
        {
            int currentValue = _defaultFirstQuestionValue * pointMultiplier;

            for (int i = 0; i < DefaultSize; i++)
            {
                QuestionValues[i] = currentValue;
                currentValue += _defaultQuestionValueStep * pointMultiplier;
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
            if (Name != String.Empty)
            {
                foreach (var item in Questions)
                {
                    if (item == null)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
