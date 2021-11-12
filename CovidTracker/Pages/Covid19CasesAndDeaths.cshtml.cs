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
        public string Query;
        public void OnGet(string query)
        {
            Query = query;
            using (var webClient = new WebClient()) 
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                var covid19CasesAndDeaths = CasesAndDeaths.Covid19CasesAndDeaths.FromJson(jsonString);


                if(!string.IsNullOrWhiteSpace(query))
                {
                    var covidCasesAndDeathList = covid19CasesAndDeaths.ToList();
                    var stateWiseCasesAndDeaths = covidCasesAndDeathList.FindAll(x => string.Equals(x.State.ToString(), query, StringComparison.OrdinalIgnoreCase));
                    if (stateWiseCasesAndDeaths != null)
                    {
                        var orderedStateWiseCasesAndDeaths = stateWiseCasesAndDeaths.OrderByDescending(x => x.SubmissionDate).ToArray();
                        ViewData["Covid19CasesAndDeaths"] = orderedStateWiseCasesAndDeaths[0];
                    }

                }
            }
        }
    }
}
