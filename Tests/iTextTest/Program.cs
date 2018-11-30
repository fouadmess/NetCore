using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.IO;
using Messaia.Net.iTextExtensions;

namespace iTextTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //CreateFromImages();
                CreateFromHtml();


                //var streams = PdfUtil.ExtractImages(@"D:\Temp\Static\Barilla\dc505853-8349-4431-84c9-851e81270f1f.pdf");

                //foreach (var stream in streams)
                //{
                //    using (var image = Image.FromStream(stream))
                //    {
                //        image.Save(@"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\5.jpg");
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("PDF created");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CreateFromImages()
        {
            if (true)
            {
                var images = new string[]
                {
                    @"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\3.jpg",
                    @"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\2.jpg"
                };

                PdfUtil.CreateFromImages(images, @"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\NewPdf_O.pdf", false);
            }

            if (false)
            {
                var images = new string[]
                {
                    @"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\3.jpg",
                    @"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\2.jpg"
                };

                var pdfStream = PdfUtil.FromImages(images, false);

                using (pdfStream)
                {
                    pdfStream.Seek(0, SeekOrigin.Begin);
                    using (FileStream fs = new FileStream(@"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\NewPdf.pdf", FileMode.OpenOrCreate))
                    {
                        pdfStream.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CreateFromHtml()
        {
            PdfUtil.CreateFromContent("Hallo welt!", @"C:\Users\Messaia\Desktop\DELETE\ImagesFromPdf\From_Content.pdf");
        }
    }
}
