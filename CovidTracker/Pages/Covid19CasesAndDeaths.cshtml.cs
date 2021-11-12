using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CovidTracker.Pages
{
    public class Covid19CasesAndDeathsModel : PageModel
    {
        public string Query;
        public void OnGet(string query)
        [BindProperty]
        public SelectList StateList { get; set; }
        public string SearchState { get; set; }
        public Dictionary<string, string> statesDictionary { get; set; }

        public void OnGet(string q)
        {
            InitStateDropdown();
            Query = query;
            using (var webClient = new WebClient()) 
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                var covid19CasesAndDeaths = CasesAndDeaths.Covid19CasesAndDeaths.FromJson(jsonString);
                ViewData["Covid19CasesAndDeaths"] = covid19CasesAndDeaths[0];


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

        private void InitStateDropdown() 
        {
            statesDictionary = new Dictionary<string, string>
            {
                { "AL", "Alabama" },
                { "AK", "Alaska" },
                { "AZ", "Arizona" },
                { "AR", "Arkansas" },
                { "CA", "California" },
                { "CO", "Colorado" },
                { "CT", "Connecticut" },
                { "DE", "Delaware" },
                { "DC", "District Of Columbia" },
                { "FL", "Florida" },
                { "GA", "Georgia" },
                { "HI", "Hawaii" },
                { "ID", "Idaho" },
                { "IL", "Illinois" },
                { "IN", "Indiana" },
                { "IA", "Iowa" },
                { "KS", "Kansas" },
                { "KY", "Kentucky" },
                { "LA", "Louisiana" },
                { "ME", "Maine" },
                { "MD", "Maryland" },
                { "MA", "Massachusetts" },
                { "MI", "Michigan" },
                { "MN", "Minnesota" },
                { "MS", "Mississippi" },
                { "MO", "Missouri" },
                { "MT", "Montana" },
                { "NE", "Nebraska" },
                { "NV", "Nevada" },
                { "NH", "New Hampshire" },
                { "NJ", "New Jersey" },
                { "NM", "New Mexico" },
                { "NY", "New York" },
                { "NC", "North Carolina" },
                { "ND", "North Dakota" },
                { "OH", "Ohio" },
                { "OK", "Oklahoma" },
                { "OR", "Oregon" },
                { "PA", "Pennsylvania" },
                { "RI", "Rhode Island" },
                { "SC", "South Carolina" },
                { "SD", "South Dakota" },
                { "TN", "Tennessee" },
                { "TX", "Texas" },
                { "UT", "Utah" },
                { "VT", "Vermont" },
                { "VA", "Virginia" },
                { "WA", "Washington" },
                { "WV", "West Virginia" },
                { "WI", "Wisconsin" },
                { "WY", "Wyoming" }
            };

            //ViewData["SearchState"] = new SelectList(statesDictionary, "State", "Name");
        }
    }
}
