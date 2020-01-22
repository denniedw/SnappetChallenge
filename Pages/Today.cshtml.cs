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
    public class TodayModel : PageModel
    {
        private readonly ILogger<TodayModel> _logger;
        [BindProperty] public List<UserDataObject> UserData { get; set; }

        public TodayModel(ILogger<TodayModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            UserData = DataExtractHelper.GetStudentData();
        }
    }
}
