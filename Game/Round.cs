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

        public Theme[] Themes { get; set; }

        public Round()
        {
            Themes = new Theme[DefaultSize];
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
