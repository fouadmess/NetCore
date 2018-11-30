///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 02:16:40
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace iTextSharp.text.pdf
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// PdfReaderExtensions class.
    /// </summary>
    public static class PdfReaderExtensions
    {
        /// <summary>
        /// Extracts images from a pdf file
        /// To save the image to disk use:
        /// var stream = ExtractImages(fileName).FirstOrDefault()
        /// using(var image = Image.FromStream(stream))
        /// {
        ///     image.Save("<PATH>");
        /// }
        /// </summary>
        /// <param name="sourcePdf"></param>
        /// <returns></returns>
        public static List<Stream> ExtractImages(this PdfReader pdfReader)
        {
            /* Images container */
            var images = new List<Stream>();

            try
            {
                /* Iterate all pdf pages */
                for (var pageNumber = 1; pageNumber <= pdfReader.NumberOfPages; pageNumber++)
                {
                    /* Get pdf object */
                    var pdfDictionary = pdfReader.GetPageN(pageNumber)
                        .Get(PdfName.Resources)
                        .GetObject(PdfName.Xobject)
                        .ToDictionary();

                    /* Ieterate the objects names */
                    foreach (var name in pdfDictionary?.Keys)
                    {
                        /* Get the PdfObject and check if it is an indirect object */
                        var obj = pdfDictionary.Get(name as PdfName);
                        if (!obj.IsIndirect())
                        {
                            continue;
                        }

                        /* Check if the pdf object is of type Image */
                        if (!PdfName.Image.Equals(PdfReaderUtil.GetType(obj)))
                        {
                            continue;
                        }

                        /* Get bytes from the stream */
                        var bytes = pdfReader.GetBytes(((PrIndirectReference)obj).Number);
                        if ((bytes != null))
                        {
                            images.Add(new MemoryStream(bytes));
                        }
                    }
                }
            }
            finally
            {
                pdfReader.Close();
            }

            return images;

        }

        /// <summary>
        /// Gets a stream object
        /// </summary>
        /// <param name="pdfReader"></param>
        /// <param name="xrefIndex"></param>
        /// <returns></returns>
        public static PdfStream GetStream(this PdfReader pdfReader, int xrefIndex)
        {
            return pdfReader.GetPdfObject(xrefIndex) as PdfStream;
        }

        /// <summary>
        /// Gets a stream object
        /// </summary>
        /// <param name="pdfReader"></param>
        /// <param name="xrefIndex"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this PdfReader pdfReader, int xrefIndex)
        {
            return PdfReader.GetStreamBytesRaw((PrStream)pdfReader.GetStream(xrefIndex));
        }
    }
}