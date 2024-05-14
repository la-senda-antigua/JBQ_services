using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JBQ.Models
{
    public class Question
    {
        public Int64 PK_Question_ID { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string Reference { get; set; }
        public Int64 Points { get; set; }
        public Int64 UniqueCharacterIndex { get; set; }
    }
}