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

        public string Name { get; }
        private Question[] _questions { get; set; }
        private int[] _questionValues { get; }

        public Theme(string name, int pointMultiplier)
        {
            Name = name;
            _questions = new Question[DefaultSize];
            _questionValues = new int[DefaultSize];
            int currentValue = _defaultFirstQuestionValue * pointMultiplier;

            for (int i = 0; i < DefaultSize; i++)
            {
                _questionValues[i] = currentValue;
                currentValue += _defaultQuestionValueStep * pointMultiplier;
            }

           
        }

        public Question this[int index]
        {
            get { return _questions[index]; }
            set { _questions[index] = value; }
        }

        public int GetValue(int index)
        {
            return _questionValues[index];
        }

        public bool IsReady()
        {
            return CollectionManager.IsReady(_questions);
        }

        public void Swap(int index1, int index2)
        {
            CollectionManager.Swap(_questions, index1, index2);
        }
    }
}
