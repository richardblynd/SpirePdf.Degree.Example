using QRCoder;

namespace SpirePdf.PdfGenerator;

public class QRCodeFactory
{
    public static byte[] From(string text)
    {
        return GeneratedQRCode(text);
    }

    private static byte[] GeneratedQRCode(string text)
    {
        var qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new BitmapByteQRCode(qrCodeData);
        return qrCode.GetGraphic(2);
    }
}