
Thank you for using Select.Pdf for .NET.

Online demo C#: https://selectpdf.com/demo/
Online demo Vb.Net: https://selectpdf.com/demo-vb/
Online documentation: https://selectpdf.com/pdf-library-classic/

Looking for Linux or Docker support? Use SelectPdf.Universal - the cross-platform edition of SelectPdf, which keeps the classes and members of this library (porting needs new package references, the SelectPdf.Universal namespace and SelectPdf's own drawing types instead of System.Drawing): https://www.nuget.org/packages/SelectPdf.Universal/ - migration guide: https://selectpdf.com/pdf-library-classic/html/Migrating-To-SelectPdf-Universal.htm


Select.Pdf is very easy to use. For example, the following code will create a pdf document with a simple text in it:

            SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
            SelectPdf.PdfPage page = doc.AddPage();

            SelectPdf.PdfFont font = doc.AddFont(SelectPdf.PdfStandardFont.Helvetica);
            font.Size = 20;

            SelectPdf.PdfTextElement text = new SelectPdf.PdfTextElement(50, 50, "Hello world!", font);
            page.Add(text);

            doc.Save("test.pdf");
            doc.Close();


With Select.Pdf is also very easy to convert any web page to a pdf document. The code is as simple as this:

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl("http://selectpdf.com");
            doc.Save("test.pdf");
            doc.Close();

Important: This package works for .NET Framework up to version 4.5. To use newer .NET Framework versions, .NET Core, .NET 5 - .NET 11 use the following package:
https://www.nuget.org/packages/Select.Pdf.NetCore/


For complete product information, take a look at https://selectpdf.com.
For support, contact us at support@selectpdf.com.
