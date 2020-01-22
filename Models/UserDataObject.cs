using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snappet_Challenge.Models
{
    public class UserDataObject
    {
        public int UserId { get; set; }
        public List<SubjectObject> Subjects { get; set; }
    }
}
