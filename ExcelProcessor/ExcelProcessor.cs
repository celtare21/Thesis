using Aspose.Cells;

namespace ExcelProcessor;

public static class ExcelProcessor
{
    public static void ProcessData(Dictionary<string, Dictionary<string, double>> benchmarkData, string outputName)
    {
        using var workbook = new Workbook();
        using var worksheet = workbook.Worksheets[0];
        var lastColumn = string.Empty;

        Console.WriteLine("Processing data in excel...");

        for (var i = 0; i < benchmarkData.Count; i++)
        {
            var item = benchmarkData.ElementAt(i);
            var categoryName = item.Key;
            var results = item.Value;

            worksheet.Cells[$"A{i + 2}"].PutValue(categoryName);

            for (var j = 0; j < results.Count; j++)
            {
                var resultItem = results.ElementAt(j);
                var resultName = resultItem.Key;
                var resultValue = resultItem.Value;
                var data = $"{(char)(char.Parse("B") + j)}1";
                var column = $"{(char)(char.Parse("B") + j)}{2 + i}";

                worksheet.Cells[column].PutValue(resultValue);
                worksheet.Cells[data].PutValue(resultName);

                lastColumn = column;
            }
        }

        var chartIndex = worksheet.Charts.Add(Aspose.Cells.Charts.ChartType.Bar100PercentStacked, 20, 0, 45, 25);
        var chart = worksheet.Charts[chartIndex];

        chart.SetChartDataRange($"A1:{lastColumn}", false);

        workbook.Save($"{outputName}.xls");

        Console.WriteLine("Done!");
    }
}