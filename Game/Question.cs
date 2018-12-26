using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }

        public Question()
        {

        }

        public Question(string question, string answer)
        {
            QuestionText = question;
            AnswerText = answer;
        }
    }
}
