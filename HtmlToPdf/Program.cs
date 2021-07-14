using iText.Html2pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HtmlToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            string htmlFileToString = File.ReadAllText(@"C:/Users/sulem/source/repos/HtmlToPdf/HtmlToPdf/HtmlFiles/HTMLPage2.html");

            /*

            byte[] imageBytes = null;
            var crawfordlogo = Convert.FromBase64String("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");

            string imageUrl = "https://miro.medium.com/max/1050/1*NeKYs9ypQ7jkalNxEX3t9Q.png";//https://static.xx.fbcdn.net/rsrc.php/y8/r/dF5SId3UHWd.svg;https://miro.medium.com/max/1050/1*NeKYs9ypQ7jkalNxEX3t9Q.png;https://www.jquery-az.com/html/images/banana.jpg

            //byte[] response = new System.Net.WebClient().DownloadData(imageUrl);

            using (var wc = new System.Net.WebClient())
            {
                imageBytes = wc.DownloadData(imageUrl);
                string imageBase64 = Convert.ToBase64String(imageBytes);
                var mimeType = wc.ResponseHeaders["content-type"];
                if (mimeType.Contains(';'))
                {
                    mimeType = mimeType.Split(';')[0];
                }
                string svgData = $"data:{mimeType};base64,{imageBase64}";
            }

            */

            //string image = "image/svg;base64," + Convert.ToBase64String(response);
            byte[] imageBytes1 = System.IO.File.ReadAllBytes(@"C:/Users/sulem/source/repos/HtmlToPdf/HtmlToPdf/ImageFiles/openly.png");
            string base64String = Convert.ToBase64String(imageBytes1);


            /*
             
            // Converting image code 

            MemoryStream streamImage = new MemoryStream(imageBytes);
            
            System.Drawing.Image image = System.Drawing.Image.FromStream(streamImage);
            Bitmap bitmap = new Bitmap(image);

            using (MemoryStream memoryStreamImage = new MemoryStream())
            {
                bitmap.Save(memoryStreamImage,System.Drawing.Imaging.ImageFormat.Png);
                string base64imageString = Convert.ToBase64String(memoryStreamImage.ToArray());
            }

            */



            using (MemoryStream memoryStream = new MemoryStream())
            {
                ConverterProperties converterProperties = new ConverterProperties();
                HtmlConverter.ConvertToPdf(htmlFileToString, memoryStream);
                byte[] bytes = memoryStream.ToArray();
                File.WriteAllBytes(@"C:/Users/sulem/source/repos/HtmlToPdf/HtmlToPdf/PdfFiles/table.pdf", bytes);
                memoryStream.Close();
            }


            /*

            //Converting HTML to pdf by using old itext sharp
            // it will not support all tags with inlin support

            StringReader sr = new StringReader(htmlFileToString);
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HtmlWorker htmlparser = new HtmlWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();
                File.WriteAllBytes(@"C:/Users/sulem/source/repos/HtmlToPdf/HtmlToPdf/HtmlFiles/Demo.pdf", bytes);
                memoryStream.Close();
            }

            */
            Console.WriteLine("operation completed");
        }
    }
}
