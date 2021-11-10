using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CovidTracker.Pages
{
    public class Covid19CumulativeModel : PageModel
    {
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                string jsonStringVaccineData = webClient.DownloadString("https://data.cdc.gov/resource/rh2h-3yt2.json");
                var covid19CasesAndDeaths = CasesAndDeaths.Covid19CasesAndDeaths.FromJson(jsonString);
                var covid19Vaccine = Vaccines.Covid19Vaccine.FromJson(jsonStringVaccineData);
                ViewData["Covid19CasesAndDeaths"] = covid19CasesAndDeaths;
                ViewData["Covid19Vaccines"] = covid19Vaccine;

            }
        }
    }
}
