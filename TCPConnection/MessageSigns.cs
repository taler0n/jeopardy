﻿
namespace TCPConnection
{
    public static class MessageSigns
    {
        public const int SignLength = 3;
        public const string BuzzMessage = @"[!]";
        public const string AnswerMessage = @"[*]";
        public const string DisconnectMessage = @"[@]";
        public const string QuestionSign = @"[?]";
        public const string RightAnswerSign = @"[+]";
        public const string WrongAnswerSign = @"[-]";
        public const string FinalRoundMessage = @"[$]";
        public const string TimesUpMessage = @"[^]";
    }
}
