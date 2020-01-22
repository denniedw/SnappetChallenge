using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snappet_Challenge.Models
{
    public class SubjectAnswerObject
    {
        public int SubmittedAnswerId { get; set; }
        public DateTime SubmitDateTime { get; set; }
        public int Progress { get; set; }
        public int ExerciseId { get; set; }
        public string Difficulty { get; set; }
        public string Domain { get; set; }
        public string LeaningObjective { get; set; }
    }
}
