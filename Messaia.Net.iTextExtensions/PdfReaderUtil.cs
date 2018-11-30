///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 02:41:13
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace iTextSharp.text.pdf
{
    /// <summary>
    /// PdfReaderUtil class.
    /// </summary>
    public static class PdfReaderUtil
    {
        /// <summary>
        /// Gets a PdfDictionary. 
        /// A dictionary is an associative table containing pairs of objects. 
        /// The first element of each pair is called the key and the second element is called the value.
        /// </summary>
        /// <param name="pdfReader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PdfDictionary GetDictionary(PdfObject pdfObject)
        {
            return PdfReader.GetPdfObject(pdfObject)?.ToDictionary();
        }

        /// <summary>
        /// Gets the type pf the PdfObject.
        /// </summary>
        /// <param name="pdfObject"></param>
        /// <returns></returns>
        public static PdfName GetType(PdfObject pdfObject)
        {
            return (PdfName)PdfReader.GetPdfObject(GetDictionary(pdfObject).Get(PdfName.Subtype));
        }
    }
}