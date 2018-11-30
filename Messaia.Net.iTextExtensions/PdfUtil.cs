///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 16:28:18
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.iTextExtensions
{
    using iTextSharp.text;
    using iTextSharp.text.html;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// PdfUtil class.
    /// </summary>
    public class PdfUtil
    {
        #region Fields

        /// <summary>
        /// Allowed image extensions
        /// </summary>
        public static readonly string[] ImageExtensions = { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

        #endregion

        #region Properties

        /// <summary>
        /// Gets default style sheet.
        /// </summary>
        /// <returns></returns>
        public static StyleSheet DefaultStyle
        {
            get
            {
                var style = new StyleSheet();

                style.LoadTagStyle("body", "face", "Garamond");
                style.LoadTagStyle("body", "encoding", "Identity-H");
                style.LoadTagStyle("body", "size", "10pt");
                style.LoadTagStyle("h1", "size", "30pt");
                style.LoadTagStyle("h1", "style", "line-height:30pt;font-weight:bold;");
                style.LoadTagStyle("h2", "size", "22pt");
                style.LoadTagStyle("h2", "style", "line-height:30pt;font-weight:bold;margin-top:5pt;margin-bottom:12pt;");
                style.LoadTagStyle("h3", "size", "15pt");
                style.LoadTagStyle("h3", "style", "line-height:25pt;font-weight:bold;margin-top:1pt;margin-bottom:15pt;");
                style.LoadTagStyle("h4", "size", "13pt");
                style.LoadTagStyle("h4", HtmlTags.STYLE, "line-height:23pt;margin-top:1pt;margin-bottom:15pt;");
                style.LoadTagStyle("hr", HtmlTags.WIDTH, "100%");
                style.LoadTagStyle("hr", HtmlTags.COLOR, "#EEEEEE");
                style.LoadTagStyle("hr", HtmlTags.PLAINHEIGHT, "1px");
                style.LoadTagStyle("a", HtmlTags.STYLE, "text-decoration:underline;");
                style.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.VERTICALALIGN, HtmlTags.ALIGN_BOTTOM);
                style.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.BOTTOMMARGIN, "12pt");
                style.LoadTagStyle(HtmlTags.HEADERCELL, "borderbottom", "1");

                return style;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Extracts images from a pdf file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<Stream> ExtractImages(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File not found");
            }

            /* Create an instance of pdfReaderand extract the streams */
            return new PdfReader(fileName).ExtractImages();
        }

        /// <summary>
        /// Creates a pdf file from image files
        /// </summary>    
        /// <param name="fileInfos"></param>
        /// <param name="outputFile"></param>
        /// <param name="fitToPage"></param>
        /// <returns>Type: MemoryStream</returns>
        public static void CreateFromImages(string[] fileInfos, string outputFile, bool fitToPage = true)
        {
            CreateFromImages(fileInfos.Select(x => new FileInfo(x)).ToArray(), outputFile, fitToPage);
        }

        /// <summary>
        /// Creates a pdf file from image files
        /// </summary>    
        /// <param name="fileInfos"></param>
        /// <param name="outputFile"></param>
        /// <param name="fitToPage"></param>
        /// <returns>Type: MemoryStream</returns>
        public static void CreateFromImages(FileInfo[] fileInfos, string outputFile, bool fitToPage = true)
        {
            if (outputFile == null)
            {
                throw new ArgumentNullException(nameof(outputFile));
            }

            /* Get the memory stream */
            using (var pdfStream = FromImages(fileInfos, fitToPage))
            {
                /* Seek to begin */
                pdfStream.Seek(0, SeekOrigin.Begin);

                /* Copy the memory stream into the file stream and flush it */
                using (var fs = new FileStream(outputFile, FileMode.OpenOrCreate))
                {
                    pdfStream.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// Creates a pdf file from image files
        /// </summary>    
        /// <param name="fileInfos"></param>
        /// <returns>Type: MemoryStream</returns>
        /// <example>
        /// <code>
        ///     var stream = PdfUtil.FromImages("PATH-TO-IMAGE");
        ///     using (stream)
        ///     {
        ///         stream.Seek(0, SeekOrigin.Begin);
        ///         using (FileStream fileStream = new FileStream("PATH-TO-OUTPUT", FileMode.OpenOrCreate))
        ///         {
        ///             stream.CopyTo(fileStream);
        ///             fileStream.Flush();
        ///         }
        ///     }
        /// </code>      
        /// </example>
        public static Stream FromImages(params string[] fileInfos)
        {
            return FromImages(fileInfos, true);
        }

        /// <summary>
        /// Creates a pdf file from image files
        /// </summary>    
        /// <param name="fileInfos"></param>
        /// <param name="fitToPage"></param>
        /// <returns>Type: MemoryStream</returns>
        /// <example>
        /// <code>
        ///     var stream = PdfUtil.FromImages("PATH-TO-IMAGE");
        ///     using (stream)
        ///     {
        ///         stream.Seek(0, SeekOrigin.Begin);
        ///         using (FileStream fileStream = new FileStream("PATH-TO-OUTPUT", FileMode.OpenOrCreate))
        ///         {
        ///             stream.CopyTo(fileStream);
        ///             fileStream.Flush();
        ///         }
        ///     }
        /// </code>      
        /// </example>
        public static Stream FromImages(string[] fileInfos, bool fitToPage = true)
        {
            if (fileInfos == null)
            {
                throw new ArgumentNullException(nameof(fileInfos));
            }

            return FromImages(fileInfos.Select(x => new FileInfo(x)).ToArray(), fitToPage);
        }

        /// <summary>
        /// Creates a pdf file from image files
        /// </summary>    
        /// <param name="fileInfos"></param>
        /// <returns>Type: MemoryStream</returns>
        /// <example>
        /// <code>
        ///     var stream = PdfUtil.FromImages("PATH-TO-IMAGE");
        ///     using (stream)
        ///     {
        ///         stream.Seek(0, SeekOrigin.Begin);
        ///         using (FileStream fileStream = new FileStream("PATH-TO-OUTPUT", FileMode.OpenOrCreate))
        ///         {
        ///             stream.CopyTo(fileStream);
        ///             fileStream.Flush();
        ///         }
        ///     }
        /// </code>      
        /// </example>
        public static Stream FromImages(params FileInfo[] fileInfos)
        {
            return FromImages(fileInfos, true);
        }

        /// <summary>
        /// Creates a pdf file from image files
        /// </summary>    
        /// <param name="fileInfos"></param>
        /// <param name="fitToPage"></param>
        /// <returns>Type: MemoryStream</returns>
        /// <example>
        /// <code>
        ///     var stream = PdfUtil.FromImages("PATH-TO-IMAGE");
        ///     using (stream)
        ///     {
        ///         stream.Seek(0, SeekOrigin.Begin);
        ///         using (FileStream fileStream = new FileStream("PATH-TO-OUTPUT", FileMode.OpenOrCreate))
        ///         {
        ///             stream.CopyTo(fileStream);
        ///             fileStream.Flush();
        ///         }
        ///     }
        /// </code>      
        /// </example>
        public static Stream FromImages(FileInfo[] fileInfos, bool fitToPage = true)
        {
            if (fileInfos == null)
            {
                throw new ArgumentNullException(nameof(fileInfos));
            }

            /* Check if the file exists */
            if (fileInfos.Any(x => !x.Exists))
            {
                throw new ArgumentException("The specified file does not exist");
            }

            /* Check if the file is an image */

            if (fileInfos.Select(x => x.Extension.ToUpper()).Any(x => !ImageExtensions.Contains(x)))
            {
                throw new ArgumentException("The specified file is not an image");
            }

            /* Create a new document */
            var document = new Document(PageSize.Letter);

            try
            {
                /* Use a memory string so we don't need to write to disk */
                var outputStream = new MemoryStream();

                /* Associate the PDF with the stream */
                PdfWriter.GetInstance(document, outputStream);

                /* Open the PDF for writing */
                document.Open();

                /* Get document dimensions */
                var pageWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);
                var pageHeight = document.PageSize.Height - (document.TopMargin + document.BottomMargin);

                foreach (var fileInfo in fileInfos)
                {
                    /* Load image and scal it */
                    var image = Image.GetInstance(fileInfo.FullName);

                    if (fitToPage)
                    {
                        image.ScaleToFit(pageWidth, pageHeight);
                    }

                    /* Add the image to the document */
                    document.Add(image);
                }

                /* Return the bytes */
                return outputStream;
            }
            finally
            {
                /* Close the document */
                document.Close();
            }
        }

        /// <summary>
        /// Creates a pdf file from text or html.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="outputFile"></param>
        /// <param name="style"></param>
        /// <param name="pageEvent"></param>
        public static void CreateFromContent(string content, string outputFile, StyleSheet style = null, IPdfPageEvent pageEvent = null)
        {
            if (outputFile == null)
            {
                throw new ArgumentNullException(nameof(outputFile));
            }

            /* Get the memory stream */
            using (var pdfStream = FromContent(content, style, pageEvent))
            {
                /* Seek to begin */
                pdfStream.Seek(0, SeekOrigin.Begin);

                /* Copy the memory stream into the file stream and flush it */
                using (var fs = new FileStream(outputFile, FileMode.OpenOrCreate))
                {
                    pdfStream.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// Creates a pdf file from text or html.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="style"></param>
        /// <param name="pageEvent"></param>
        /// <returns></returns>
        /// <returns>Type: MemoryStream</returns>
        /// <example>
        /// <code>
        ///     var stream = PdfUtil.FromContent("Content");
        ///     using (stream)
        ///     {
        ///         stream.Seek(0, SeekOrigin.Begin);
        ///         using (FileStream fileStream = new FileStream("PATH-TO-OUTPUT", FileMode.OpenOrCreate))
        ///         {
        ///             stream.CopyTo(fileStream);
        ///             fileStream.Flush();
        ///         }
        ///     }
        /// </code>      
        /// </example>
        public static Stream FromContent(string content, StyleSheet style = null, IPdfPageEvent pageEvent = null)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            /* Create a memory stream */
            var memoryStream = new MemoryStream();

            /* Create a A4 document */
            var document = new Document(PageSize.A4, 45, 45, 30, 18);

            /* Create a worker to parse the html content */
            var worker = new HtmlWorker(document);

            try
            {
                using (var reader = new StringReader(content))
                {
                    /* Create a writer that listens to the document and directs a XML-stream to a file */
                    var writer = PdfWriter.GetInstance(document, memoryStream);

                    /* Set stylesheet */
                    if (style != null)
                    {
                        worker.Style = style;
                    }

                    /* Add PageEvent, if any */
                    if (pageEvent != null)
                    {
                        writer.PageEvent = pageEvent;
                    }

                    /* Open the document and start the worker on the document */
                    document.Open();
                    worker.StartDocument();

                    /* Parse the html into the document */
                    worker.Parse(reader);

                    /* Close the document */
                    worker.EndDocument();

                    return memoryStream;
                }
            }
            finally
            {
                worker.Close();
                document.Close();
            }
        }

        #endregion
    }
}