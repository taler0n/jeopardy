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

        public FinalQuestion[] Questions { get; set; }

        public FinalRound()
        {
            Questions = new FinalQuestion[DefaultSize];
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
                if (item == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
