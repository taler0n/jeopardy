using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPConnection
{
    public static class MessageSigns
    {
        public const int SignLength = 3;
        public const string BuzzMessage = @"[!]";
        public const string AnswerMessage = @"[*]";
        public const string QuestionSign = @"[?]";
        public const string RightAnswerSign = @"[+]";
        public const string WrongAnswerSign = @"[-]";
        public const string FinalRoundMessage = @"[$]";
        public const string TimesUpMessage = @"[^]";
    }
}
