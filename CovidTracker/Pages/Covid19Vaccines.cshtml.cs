using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CovidTracker.Vaccines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CovidTracker.Pages
{
    public class Covid19VaccinesModel : PageModel
    {
        [BindProperty]
        public SelectList StateList { get; set; }
        public List<Covid19VaccinesChartModel> Covid19VaccinesList { get; set; }
        public string SearchState { get; set; }
        public Dictionary<string, string> statesDictionary { get; set; }

        public void OnGet(string query)
        {
            Covid19VaccinesList = new List<Covid19VaccinesChartModel>();
            InitStateDropdown();
            using (var webClient = new WebClient())
            {
                string covid19Data = string.Empty;

                try 
                {
                    covid19Data = webClient.DownloadString("https://data.cdc.gov/resource/rh2h-3yt2.json");
                    var covid19Vaccines = Covid19Vaccine.FromJson(covid19Data);

                    if (!string.IsNullOrWhiteSpace(query))
                    {
                        var covid19VaccineList = covid19Vaccines.ToList();
                        var stateWiseVaccines = covid19VaccineList.FindAll(x => string.Equals(x.Location, query, StringComparison.OrdinalIgnoreCase)).Where(x => x.DateType == DateType.Report).ToList();
                        if (stateWiseVaccines != null && stateWiseVaccines.Count > 0)
                        {
                            InitCovid19VaccinesChartList(stateWiseVaccines.OrderBy(x => x.Date).ToList());
                            var orderedStateWiseVaccines = stateWiseVaccines.OrderByDescending(x => x.Date).ToArray();
                            ViewData["Covid19Vaccines"] = orderedStateWiseVaccines[0];
                        }
                        else
                        {
                            ViewData["Covid19Vaccines"] = null;
                        }
                    }
                    else
                    {
                        ViewData["Covid19Vaccines"] = null;
                    }
                    SearchState = query;
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception while calling API", ex);
                }
            }
        }
        public class Covid19VaccinesChartModel
        {
            public long DailyDoses { get; set; }
            public long SeriesComplete { get; set; }
            public DateTime SubmittedDate { get; set; }
        }

        private void InitCovid19VaccinesChartList(List<Covid19Vaccine> stateWiseVaccines)
        {
            Covid19VaccinesList = new List<Covid19VaccinesChartModel>();
            foreach (var covid19Vaccine in stateWiseVaccines)
            {
                var covid19VaccineChartData = new Covid19VaccinesChartModel();
                covid19VaccineChartData.DailyDoses = covid19Vaccine.AdministeredDaily;
                covid19VaccineChartData.SeriesComplete = covid19Vaccine.SeriesCompleteDaily;
                covid19VaccineChartData.SubmittedDate = covid19Vaccine.Date.Date;
                Covid19VaccinesList.Add(covid19VaccineChartData);
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

