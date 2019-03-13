using System.Collections.Generic;

namespace Game
{
    public class GameObject
    {
        public Settings Settings { get; set; }
        public List<Round> Rounds { get; set; }
        public FinalRound Final { get; set; }

        public GameObject()
        { }

        public GameObject(Settings settings)
        {
            Settings = settings;
            Rounds = new List<Round>();
            Final = new FinalRound(Settings);
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
