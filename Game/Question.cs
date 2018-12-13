using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Question
    {
        public string QuestionText { get; }
        public string AnswerText { get; }
        public bool IsAvailable { get; set; }
        public MediaContent Media { get; }

        public Question(string question, string answer, MediaContent media)
        {
            QuestionText = question;
            AnswerText = answer;
            IsAvailable = true;
            Media = media;
        }
    }
}
