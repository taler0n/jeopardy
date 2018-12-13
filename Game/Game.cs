using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Game
    {
        private List<Round> _rounds { get; set; }
        public int Count { get { return _rounds.Count; } }
        public FinalRound Final { get; private set; }

        public Game()
        {
            _rounds = new List<Round>();
            Final = new FinalRound();
        }

        public Round this[int index]
        {
            get { return _rounds[index]; }
            set { _rounds[index] = value; }
        }

        public void AddRound(Round round)
        {
            _rounds.Add(round);
        }

        public bool IsReady()
        {
            return (CollectionManager.IsReady(_rounds) && Final != null);
        }

        public void Swap(int index1, int index2)
        {
            CollectionManager.Swap(_rounds, index1, index2);
        }
    }
}
