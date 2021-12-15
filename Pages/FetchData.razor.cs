using BlazorTest.Shared;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace BlazorTest.Pages
{
    public partial class FetchData
    {
        private List<VirtueRecord> virtues = new List<VirtueRecord>();

        private List<VirtueRecord> filteredVirtues = new List<VirtueRecord>();

        protected SearchModel searchModel { get; set; } = new SearchModel();


        protected override async Task OnInitializedAsync()
        {

            var response = await Http.GetAsync("sample-data/virtues.csv");
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync();
            using (var reader = new StreamReader(stream, true))
            using (var csv = new CsvReader(reader, CsvConfig))
            {
                var records = csv.GetRecords<VirtueRecord>();
                virtues = records.ToList();
            }
        }

        protected void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchModel?.Query))
            {
                var q = searchModel.Query.ToLowerInvariant();

                filteredVirtues = virtues.Where(x =>
                    x.EtapValues.Any(v => v.Contains(q)) ||
                    x.EtapVirtues.Any(v => v.Contains(q)) ||
                    x.School2030Values.Any(v => v.Contains(q)) ||
                    x.School2030Virtues.Any(v => v.Contains(q)))
                .ToList();

                if (filteredVirtues.Count > 0)
                {
                    var allMatched = new List<string>();
                    foreach (var resultItem in filteredVirtues)
                    {
                        allMatched.AddRange(resultItem.EtapValues.Where(x => x.Contains(q)));
                        allMatched.AddRange(resultItem.EtapVirtues.Where(x => x.Contains(q)));
                        allMatched.AddRange(resultItem.School2030Values.Where(x => x.Contains(q)));
                        allMatched.AddRange(resultItem.School2030Virtues.Where(x => x.Contains(q)));
                    }
                   
                    searchModel.MatchedQ = string.Join(", ", allMatched.Distinct());
                }
            }
        }

        protected void ShowAll()
        {
            filteredVirtues = virtues.ToList();
            searchModel.Query = "";
        }

        protected void ClearResults()
        {
            filteredVirtues.Clear();
            searchModel.Query = "";
        }

        private CsvConfiguration CsvConfig = new CsvConfiguration(new CultureInfo("lv-lv"))
        {
            Delimiter = ",", // Enforce ',' as delimiter
            PrepareHeaderForMatch = header => header.Header.ToLower() // Ignore casing
        };

        
    }

    public class SearchModel
    {
        public string Query { get; set; } = "";
        public string MatchedQ { get; set; } = "";
    }
}