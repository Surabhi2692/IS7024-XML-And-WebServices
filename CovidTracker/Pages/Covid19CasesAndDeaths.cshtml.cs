using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CovidTracker.Pages
{
    public class Covid19CasesAndDeathsModel : PageModel
    {
        public void OnGet()
        {
            using (var webClient = new WebClient()) 
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                var covid19CasesAndDeaths = CasesAndDeaths.Covid19CasesAndDeaths.FromJson(jsonString);
                ViewData["Covid19CasesAndDeaths"] = covid19CasesAndDeaths;;
            }
        }
    }
}
