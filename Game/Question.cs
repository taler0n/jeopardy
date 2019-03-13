using System;

namespace Game
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }

        public Question()
        {
            QuestionText = String.Empty;
            AnswerText = String.Empty;
        }

        public bool IsReady()
        {
            return (QuestionText != String.Empty && AnswerText != String.Empty);
        }
    }
}
