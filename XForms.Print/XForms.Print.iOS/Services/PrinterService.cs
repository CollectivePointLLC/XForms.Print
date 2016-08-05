using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using XForms.Print.iOS.Services;
using XForms.Print.Services;

[assembly: Dependency(typeof(PrinterService))]
namespace XForms.Print.iOS.Services
{
    public class PrinterService : IPrinterService
    {
        public void Print()
        {
            var printInfo = UIPrintInfo.PrintInfo;
            printInfo.OutputType = UIPrintInfoOutputType.General;
            printInfo.JobName = "My first Print Job";

            var textFormatter = new UISimpleTextPrintFormatter("Once upon a time...")
            {
                StartPage = 0,
                ContentInsets = new UIEdgeInsets(72, 72, 72, 72),
                MaximumContentWidth = 6 * 72,
            };

            var printer = UIPrintInteractionController.SharedPrintController;
            printer.PrintInfo = printInfo;
            printer.PrintFormatter = textFormatter;
            printer.ShowsPageRange = true;
            printer.Present(true, (handler, completed, err) => {
                if (!completed && err != null)
                {
                    Console.WriteLine("error");
                }
            });
        }
    }
}
