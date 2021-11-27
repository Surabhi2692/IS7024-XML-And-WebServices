using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CovidTracker.Vaccines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CovidTracker.Pages
{
    public class Covid19VaccineJsonModel : PageModel
    {
        public JsonResult OnGet()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    string covid19Data = webClient.DownloadString("https://data.cdc.gov/resource/rh2h-3yt2.json");
                    var covid19Vaccine = Covid19Vaccine.FromJson(covid19Data);
                    return new JsonResult(covid19Vaccine);
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception while calling API", ex);
                }
            }
        }
    }
}
