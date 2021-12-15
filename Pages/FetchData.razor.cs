using BlazorTest.Shared;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace BlazorTest.Pages
{
    public partial class FetchData
    {
        private List<VirtueRecord> virtues = new List<VirtueRecord>();
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

        private CsvConfiguration CsvConfig = new CsvConfiguration(new CultureInfo("lv-lv"))
        {
            Delimiter = ",", // Enforce ',' as delimiter
            PrepareHeaderForMatch = header => header.Header.ToLower() // Ignore casing
        };
    }
}