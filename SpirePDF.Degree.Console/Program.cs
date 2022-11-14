using Serilog;
using SpirePdf.PdfGenerator;

internal class Program
{
    private static void Main(string[] args)
    {
        ConfigureLog();

        var replaceStrings = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("dic.json"));

        if (replaceStrings == null)
        {
            return;
        }

        PrintPdfDegree(replaceStrings);
        PrintPdfDegree(replaceStrings);
        PrintPdfDegree(replaceStrings);
        Thread.Sleep(5000);
        PrintPdfDegree(replaceStrings);
        PrintPdfDegree(replaceStrings);
        PrintPdfDegree(replaceStrings);

        //while (true)
        //{
        //    ImprimePdfDiploma(replaceStrings);
        //    Console.ReadKey();
        //}
    }

    private static void ConfigureLog()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }

    private static void PrintPdfDegree(Dictionary<string, string> replaceStrings)
    {
        var printPdf = new SpirePdfReplacer();
        var memoryStream = printPdf.ReplaceAndSaveDegree(
            @"template.pdf",
            replaceStrings,
            "https://www.google.com");

        if (memoryStream != null)
        {
            File.WriteAllBytes("degree.pdf", memoryStream.ToArray());
        }
    }
}