using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CovidTracker.CasesAndDeaths;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CovidTracker.Pages
{
    public class Covid19CasesAndDeathsJsonModel : PageModel
    {
        public JsonResult OnGet()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    string covid19Data = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                    var covid19CasesAndDeaths = Covid19CasesAndDeaths.FromJson(covid19Data);
                    return new JsonResult(covid19CasesAndDeaths);
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception while calling API", ex);
                }
            }
        }
    }
}
            

