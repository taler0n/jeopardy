using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class FinalRound
    {
        public const int DefaultSize = 7;

        private FinalQuestion[] _questions { get; set; }

        public FinalRound()
        {
            _questions = new FinalQuestion[DefaultSize];
        }

        public FinalQuestion this[int index]
        {
            get { return _questions[index]; }
            set { _questions[index] = value; }
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
