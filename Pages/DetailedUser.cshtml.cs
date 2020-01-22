using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snappet_Challenge.Helpers;
using Snappet_Challenge.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Snappet_Challenge.Pages
{
    public class DetailedView : PageModel
    {
        private readonly ILogger<TodayModel> _logger;

        [BindProperty] public DetailedStatsObject DetailsOfUser { get; set; }
        [BindProperty] public UserDataObject UserData { get; set; }

        public DetailedView(ILogger<TodayModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int? id)
        {
            List<UserDataObject> userData = DataExtractHelper.GetStudentData();
            UserData = userData.Find(x => x.UserId == id);

            SubjectObject rekenen = UserData.Subjects.Find(x => x.Subject.Equals("Rekenen"));
            SubjectObject begrijpend = UserData.Subjects.Find(x => x.Subject.Equals("Begrijpend Lezen"));
            SubjectObject spelling = UserData.Subjects.Find(x => x.Subject.Equals("Spelling"));

            int totalRek = rekenen.CorrectAnswered.Count + rekenen.IncorrectAnswered.Count;
            int totalBegrijpend = begrijpend.CorrectAnswered.Count + begrijpend.IncorrectAnswered.Count;
            int totalSpelling = spelling.CorrectAnswered.Count + spelling.IncorrectAnswered.Count;

            DetailsOfUser = new DetailedStatsObject
            {
                UserId = UserData.UserId,
                TotalRek = rekenen.CorrectAnswered.Count + rekenen.IncorrectAnswered.Count,
                TotalBegrijpend = begrijpend.CorrectAnswered.Count + begrijpend.IncorrectAnswered.Count,
                TotalSpelling = spelling.CorrectAnswered.Count + spelling.IncorrectAnswered.Count,
                PercCorrectRek = CalculatePercentage(rekenen.CorrectAnswered.Count, totalRek),
                PercIncorrectRek = CalculatePercentage(rekenen.IncorrectAnswered.Count, totalRek),
                PercCorrectBegrijpend = CalculatePercentage(begrijpend.CorrectAnswered.Count, totalBegrijpend),
                PercIncorrectBegrijpend = CalculatePercentage(begrijpend.IncorrectAnswered.Count, totalBegrijpend),
                PercCorrectSpelling = CalculatePercentage(spelling.CorrectAnswered.Count, totalSpelling),
                PercIncorrectSpelling = CalculatePercentage(spelling.IncorrectAnswered.Count, totalSpelling),
                EventuallyCorrectRek = FirstIncorrectThenCorrentAnsweredString(rekenen.IncorrectAnswered, rekenen.CorrectAnswered),
                EventuallyCorrectBegijpend = FirstIncorrectThenCorrentAnsweredString(begrijpend.IncorrectAnswered, begrijpend.CorrectAnswered),
                EventuallyCorrectSpelling = FirstIncorrectThenCorrentAnsweredString(spelling.IncorrectAnswered, spelling.CorrectAnswered),
                MultipleIncorrectRek = MultipleTimesIncorrectNeverCorrectString(rekenen.IncorrectAnswered, rekenen.CorrectAnswered),
                MultipleIncorrectBegrijpend = MultipleTimesIncorrectNeverCorrectString(begrijpend.IncorrectAnswered, begrijpend.CorrectAnswered),
                MultipleIncorrectSpelling = MultipleTimesIncorrectNeverCorrectString(spelling.IncorrectAnswered, spelling.CorrectAnswered)
            };
        }

        private int CalculatePercentage(int partial, int total)
        {
            if(total == 0)
            {
                return 0;
            }

            return Convert.ToInt32((double)partial / total * 100);
        }

        private string FirstIncorrectThenCorrentAnsweredString(List<SubjectAnswerObject> incorrectAnswers, List<SubjectAnswerObject> correctAnswers)
        {
            string exercisesString = "";
            List<int> exercises = new List<int>();

            correctAnswers = correctAnswers.OrderBy(x => x.ExerciseId).ToList();

            for(int i = 0; i < correctAnswers.Count; i++)
            {
                int exerciseId = correctAnswers[i].ExerciseId;
                if(incorrectAnswers.FindIndex(x=> x.ExerciseId == exerciseId) != -1)
                {
                    if (!exercises.Contains(exerciseId))
                    {
                        exercises.Add(exerciseId);
                    }
                }
            }

            return CreateExerciseString(exercises);
        }

        private string MultipleTimesIncorrectNeverCorrectString(List<SubjectAnswerObject> incorrectAnswers, List<SubjectAnswerObject> correctAnswers)
        {
            List<int> exercises = new List<int>();

            incorrectAnswers = incorrectAnswers.OrderBy(x => x.ExerciseId).ToList();

            for (int i = 0; i < incorrectAnswers.Count; i++)
            {
                int exerciseId = incorrectAnswers[i].ExerciseId;

                if (incorrectAnswers.Count(x=> x.ExerciseId == exerciseId) > 1)
                {
                    if (correctAnswers.FindIndex(x => x.ExerciseId == exerciseId) == -1)
                    {
                        if (!exercises.Contains(exerciseId))
                        {
                            exercises.Add(exerciseId);
                        }
                    }
                }
            }

            return CreateExerciseString(exercises);
        }

        private string CreateExerciseString(List<int> exercises)
        {
            string exercisesString = "";

            foreach (int exercise in exercises)
            {
                exercisesString += exercise + ", ";
            }

            if (!String.IsNullOrEmpty(exercisesString))
            {
                exercisesString.Remove(exercisesString.Length - 2, 2);
            }

            return exercisesString;
        }
    }
}
