using Android.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Android.Print.Pdf;
using Android.Content;
using Android.Runtime;
using System.IO;
using Android.Graphics.Pdf;

namespace XForms.Print
{
    public class TestPrintDocumentAdapter : PrintDocumentAdapter
    {
        Context context;
        string text;
        PrintedPdfDocument document;
        float scale;

        public TestPrintDocumentAdapter(Context context, string text)
        {
            this.context = context;
            this.text = text;
        }

        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes,
                                       CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            document = new PrintedPdfDocument(context, newAttributes);

            CalculateScale(newAttributes);

            var printInfo = new PrintDocumentInfo
                .Builder("pdf-test.pdf")
                .SetContentType(PrintContentType.Document)
                .SetPageCount(1)
                .Build();

            callback.OnLayoutFinished(printInfo, true);
        }

        void CalculateScale(PrintAttributes newAttributes)
        {
            int dpi = Math.Max(newAttributes.GetResolution().HorizontalDpi, newAttributes.GetResolution().VerticalDpi);

            int leftMargin = (int)(dpi * (float)newAttributes.MinMargins.LeftMils / 1000);
            int rightMargin = (int)(dpi * (float)newAttributes.MinMargins.RightMils / 1000);
            int topMargin = (int)(dpi * (float)newAttributes.MinMargins.TopMils / 1000);
            int bottomMargin = (int)(dpi * (float)newAttributes.MinMargins.BottomMils / 1000);

            int w = (int)(dpi * (float)newAttributes.GetMediaSize().WidthMils / 1000) - leftMargin - rightMargin;
            int h = (int)(dpi * (float)newAttributes.GetMediaSize().HeightMils / 1000) - topMargin - bottomMargin;

            scale = Math.Min((float)document.PageContentRect.Width() / w, (float)document.PageContentRect.Height() / h);
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination,
                                      CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            PrintedPdfDocument.Page page = document.StartPage(0);

            page.Canvas.Scale(scale, scale);

            page.Canvas.DrawText(text, page.Canvas.ClipBounds.Right / 2, page.Canvas.ClipBounds.Bottom / 2, new Android.Graphics.Paint());
            
            document.FinishPage(page);

            WritePrintedPdfDoc(destination);

            document.Close();

            document.Dispose();

            callback.OnWriteFinished(pages);
        }

        void WritePrintedPdfDoc(ParcelFileDescriptor destination)
        {
            var javaStream = new Java.IO.FileOutputStream(destination.FileDescriptor);
            var osi = new OutputStreamInvoker(javaStream);
            using (var mem = new MemoryStream())
            {
                document.WriteTo(mem);
                var bytes = mem.ToArray();
                osi.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
