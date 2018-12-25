using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameObject
    {
        public const int DefaultNumberOfPlayers = 3;

        public List<Round> Rounds { get; set; }
        public FinalRound Final { get; set; }

        public GameObject()
        {
            Rounds = new List<Round>();
            Final = new FinalRound();
        }

        public bool IsReady()
        {
            if (Final != null && Final.IsReady())
            {
                foreach (var item in Rounds)
                {
                    if (item == null || !item.IsReady())
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
