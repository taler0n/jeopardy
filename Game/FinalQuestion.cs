using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class FinalQuestion : Question
    {
        public string Name { get; }

        public FinalQuestion(string name, string question, string answer, MediaContent media) : base(question, answer, media)
        {
            Name = name;
        }
    }
}
