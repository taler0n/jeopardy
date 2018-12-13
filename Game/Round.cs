using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Round
    {
        public const int DefaultSize = 6;

        private Theme[] _themes { get; set; }

        public Round()
        {
            _themes = new Theme[DefaultSize];
        }

        public Theme this[int index]
        {
            get { return _themes[index]; }
            set { _themes[index] = value; }
        }

        public bool IsReady()
        {
            return CollectionManager.IsReady(_themes);
        }

        public void Swap(int index1, int index2)
        {
            CollectionManager.Swap(_themes, index1, index2);
        }
    }
}
