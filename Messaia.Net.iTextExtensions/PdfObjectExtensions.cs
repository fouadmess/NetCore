///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 02:44:47
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace iTextSharp.text.pdf
{
    /// <summary>
    /// PdfObjectExtensions class.
    /// </summary>
    public static class PdfObjectExtensions
    {
        /// <summary>
        /// Converts a PdfObject to a PdfDictionary. 
        /// A dictionary is an associative table containing pairs of objects. 
        /// The first element of each pair is called the key and the second element is called the value.
        /// </summary>
        /// <param name="pdfObject"></param>
        /// <returns></returns>
        public static PdfDictionary ToDictionary(this PdfObject pdfObject)
        {
            return pdfObject as PdfDictionary;
        }

        /// <summary>
        /// Gets a PdfObject from another. 
        /// PdfObject is the abstract baseclass of all PDF objects.
        /// </summary>
        /// <param name="pdfReader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PdfObject GetObject(this PdfObject pdfObject, PdfName name)
        {
            return PdfReaderUtil.GetDictionary(pdfObject).Get(name);
        }
    }
}