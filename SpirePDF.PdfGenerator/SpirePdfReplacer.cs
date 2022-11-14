using Spire.Pdf;
using Spire.Pdf.Fields;
using Spire.Pdf.Graphics;
using Spire.Pdf.Widget;

namespace SpirePdf.PdfGenerator
{
    public class SpirePdfReplacer
    {
        public void PrintDegreePdf()
        {

        }

        public MemoryStream? ReplaceAndSaveDegree(
            string template,
            Dictionary<string, string> replaceStrings,
            string url)
        {
            var doc = new PdfDocument();
            doc.LoadFromFile(template);

            if (doc.Form == null)
            {
                return null;
            }

            var formWidget = doc.Form as PdfFormWidget;

            if (formWidget is null)
            {
                return null;
            }

            for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
            {
                if (formWidget.FieldsWidget.List[i] == null)
                {
                    continue;
                }

                var field = formWidget.FieldsWidget.List[i] as PdfField;

                if (field is PdfTextBoxFieldWidget)
                {
                    var textBoxField = field as PdfTextBoxFieldWidget;

                    if (textBoxField == null)
                    {
                        continue;
                    }

                    foreach (var replaceString in replaceStrings)
                    {
                        ReplaceValues(textBoxField, replaceString.Key, replaceString.Value);
                    }

                    textBoxField.Flatten = true;
                }
            }

            formWidget.IsFlatten = true;

            if (doc.Pages.Count >= 2)
            {
                var qrCodePdfImage = PdfImage.FromStream(new MemoryStream(QRCodeFactory.From(url)));

                var page = doc.Pages[doc.Pages.Count - 1];

                float width = qrCodePdfImage.Width;
                float height = qrCodePdfImage.Height;
                float x = (page.Canvas.ClientSize.Width - width - 40);
                float y = page.Canvas.ClientSize.Height - (page.Canvas.ClientSize.Width - x);
                page.Canvas.DrawImage(qrCodePdfImage, x, y, width, height);
            }

            doc.Pages.RemoveAt(1);

            var memoryStream = new MemoryStream();
            doc.SaveToStream(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        private static void ReplaceValues(PdfTextBoxFieldWidget textBoxField, string chave, string valor)
        {
            if (textBoxField.Text.Contains(chave))
            {
                textBoxField.Text = textBoxField.Text.Replace(chave, valor);
            }
        }
    }
}
