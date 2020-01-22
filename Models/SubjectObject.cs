using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snappet_Challenge.Models
{
    public class SubjectObject
    {
        public string Subject { get; set; }
        public List<SubjectAnswerObject> CorrectAnswered { get; set; }
        public List<SubjectAnswerObject> IncorrectAnswered { get; set; }

        public SubjectObject(string subject)
        {
            this.Subject = subject;
            this.CorrectAnswered = new List<SubjectAnswerObject>();
            this.IncorrectAnswered = new List<SubjectAnswerObject>();
        }
    }
}
