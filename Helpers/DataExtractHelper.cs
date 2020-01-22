using Newtonsoft.Json;
using Snappet_Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snappet_Challenge.Helpers
{
    public class DataExtractHelper
    {
        private static List<UserDataObject> UserData;
        public static List<UserDataObject> GetStudentData()
        {
            UserData = new List<UserDataObject>();

            List<SubmittedAnwserObject> submittedAnswers = new List<SubmittedAnwserObject>();
            submittedAnswers = JsonConvert.DeserializeObject<List<SubmittedAnwserObject>>(System.IO.File.ReadAllText(@"work.json"));

            foreach (var answer in submittedAnswers)
            {
                if (UserData.FindIndex(x => x.UserId == answer.UserId) == -1)
                {
                    UserData.Add(new UserDataObject
                    {
                        UserId = answer.UserId,
                        Subjects = SetSubjects()
                    });
                }

                int userIndex = UserData.FindIndex(x => x.UserId == answer.UserId);

                AddAnswerToCorrectSubject(answer, userIndex);
            }

            UserData = UserData.OrderBy(x => x.UserId).ToList();

            return UserData;
        }

        private static List<SubjectObject> SetSubjects()
        {
            List<SubjectObject> subjectObjects = new List<SubjectObject>();
            subjectObjects.Add(new SubjectObject("Begrijpend Lezen"));
            subjectObjects.Add(new SubjectObject("Rekenen"));
            subjectObjects.Add(new SubjectObject("Spelling"));

            return subjectObjects;
        }

        private static void AddAnswerToCorrectSubject(SubmittedAnwserObject answer, int userIndex)
        {
            int subjectIndex = UserData[userIndex].Subjects.FindIndex(x => x.Subject.Equals(answer.Subject));
            DateTime today = new DateTime(2015, 3, 14, 11, 30, 00);

            if (answer.SubmitDateTime < today)
            {
                if (answer.Correct)
                {
                    UserData[userIndex].Subjects[subjectIndex].CorrectAnswered.Add(CreateNewSubjectAnwserObject(answer));
                }
                else
                {
                    UserData[userIndex].Subjects[subjectIndex].IncorrectAnswered.Add(CreateNewSubjectAnwserObject(answer));
                }
            }
        }

        private static SubjectAnswerObject CreateNewSubjectAnwserObject(SubmittedAnwserObject answer)
        {
            SubjectAnswerObject subjectAnswerObject = new SubjectAnswerObject
            {
                Difficulty = answer.Difficulty,
                Domain = answer.Domain,
                ExerciseId = answer.ExerciseId,
                LeaningObjective = answer.LeaningObjective,
                Progress = answer.Progress,
                SubmitDateTime = answer.SubmitDateTime,
                SubmittedAnswerId = answer.SubmittedAnswerId
            };

            return subjectAnswerObject;
        }
    }
}
