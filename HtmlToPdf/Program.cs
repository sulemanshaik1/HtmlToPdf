﻿using iText.Html2pdf;
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
            string htmlFileToString = File.ReadAllText(@"C:/Users/sulem/source/repos/HtmlToPdf/HtmlToPdf/HtmlFiles/Demo2.html");

            //byte[] response = new System.Net.WebClient().DownloadData("https://static.xx.fbcdn.net/rsrc.php/y8/r/dF5SId3UHWd.svg");
            //string image = "image/svg;base64," + Convert.ToBase64String(response);
            byte[] imageBytes = System.IO.File.ReadAllBytes(@"C:/Users/sulem/source/repos/HtmlToPdf/HtmlToPdf/ImageFiles/sample1.png");
            string base64String = Convert.ToBase64String(imageBytes);
           
         
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
                HtmlConverter.ConvertToPdf(htmlFileToString, memoryStream);
                byte[] bytes = memoryStream.ToArray();
                File.WriteAllBytes(@"C:/Users/sulem/source/repos/HtmlToPdf/HtmlToPdf/PdfFiles/DemoWithHtmlToPdfDll.pdf", bytes);
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

        }
    }
}