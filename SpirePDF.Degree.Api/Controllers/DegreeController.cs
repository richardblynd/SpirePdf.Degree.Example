using Microsoft.AspNetCore.Mvc;
using SpirePdf.PdfGenerator;
using x = System.IO;

namespace SpirePdf.Degree.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreeController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var replaceStrings = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(x.File.ReadAllText("dic.json"));

            if (replaceStrings == null)
            {
                return NotFound();
            }

            PrintPdfDegree(replaceStrings);
            return Ok();
        }

        private void PrintPdfDegree(Dictionary<string, string> replaceStrings)
        {
            var printPdf = new SpirePdfReplacer();
            var memoryStream = printPdf.ReplaceAndSaveDegree(
                @"template.pdf",
                replaceStrings,
                "https://www.google.com");

            if (memoryStream != null)
            {
                x.File.WriteAllBytes("degree.pdf", memoryStream.ToArray());
            }
        }
    }    
}
