using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XForms.Print.Droid.Services;
using XForms.Print.Services;
using Android.Print;
using Android.Support.V4.Print;
using Android.Graphics;

[assembly: Dependency(typeof(PrinterService))]
namespace XForms.Print.Droid.Services
{
    public class PrinterService : IPrinterService
    {
        static PrintManager printMgr;

        public static void Init(PrintManager mgr)
        {
            printMgr = mgr;
        }

        public void Print(string text)
        {
            if (printMgr == null)
                throw new NullReferenceException("PrinterService.Print() was called, but PrinterService.printMgr is null");
            
            printMgr.Print("Print Job " + DateTime.Now.ToString(), new TestPrintDocumentAdapter(Android.App.Application.Context, text), null);
        }
    }
}