using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CovidTracker.CasesAndDeaths;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CovidTracker.Pages
{
    public class Covid19CasesAndDeathsModel : PageModel
    {
        [BindProperty]
        public SelectList StateList { get; set; }
        public List<Covid19CasesAndDeathsChartModel> Covid19CasesAndDeathsList { get; set; }
        public string SearchState { get; set; }
        public Dictionary<string, string> statesDictionary { get; set; }

        public void OnGet(string query)
        {
            Covid19CasesAndDeathsList = new List<Covid19CasesAndDeathsChartModel>();
            InitStateDropdown();
            using (var webClient = new WebClient()) 
            {
                string covid19Data = string.Empty;

                try
                {
                    covid19Data = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");
                    var covid19CasesAndDeaths = Covid19CasesAndDeaths.FromJson(covid19Data);

                    if (!string.IsNullOrWhiteSpace(query))
                    {
                        var covidCasesAndDeathList = covid19CasesAndDeaths.ToList();
                        var stateWiseCasesAndDeaths = covidCasesAndDeathList.FindAll(x => string.Equals(x.State.ToString(), query, StringComparison.OrdinalIgnoreCase));
                        if (stateWiseCasesAndDeaths != null && stateWiseCasesAndDeaths.Count > 0)
                        {
                            InitCovid19CasesAndDeathsChartList(stateWiseCasesAndDeaths.OrderBy(x => x.SubmissionDate).ToList());
                            var orderedStateWiseCasesAndDeaths = stateWiseCasesAndDeaths.OrderByDescending(x => x.SubmissionDate).ToArray();
                            ViewData["Covid19CasesAndDeaths"] = orderedStateWiseCasesAndDeaths[0];
                        }
                        else
                        {
                            ViewData["Covid19CasesAndDeaths"] = null;
                        }
                    }
                    else
                    {
                        ViewData["Covid19CasesAndDeaths"] = null;
                    }
                    SearchState = query;
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception while calling API", ex);
                }
            }
        }

        public class Covid19CasesAndDeathsChartModel
        { 
            public double NewCases { get; set; }
            public double NewDeaths { get; set; }
            public DateTime SubmittedDate { get; set; }
        }

        private void InitCovid19CasesAndDeathsChartList(List<Covid19CasesAndDeaths> stateWiseCasesAndDeaths)
        {
            Covid19CasesAndDeathsList = new List<Covid19CasesAndDeathsChartModel>();
            foreach (var covid19CaseAndDeath in stateWiseCasesAndDeaths)
            {
                if (!string.IsNullOrWhiteSpace(covid19CaseAndDeath.NewCase))
                {
                    var covid19CasesAndDeathsChartData = new Covid19CasesAndDeathsChartModel();
                    covid19CasesAndDeathsChartData.NewCases = Convert.ToDouble(covid19CaseAndDeath.NewCase);
                    covid19CasesAndDeathsChartData.NewDeaths = Convert.ToDouble(covid19CaseAndDeath.NewDeath);
                    covid19CasesAndDeathsChartData.SubmittedDate = covid19CaseAndDeath.SubmissionDate.DateTime;
                    Covid19CasesAndDeathsList.Add(covid19CasesAndDeathsChartData);
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
                { "PR", "Puerto Rico" },
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

            ViewData["SearchState"] = new SelectList(statesDictionary, "Key", "Value");
        }
    }
}
