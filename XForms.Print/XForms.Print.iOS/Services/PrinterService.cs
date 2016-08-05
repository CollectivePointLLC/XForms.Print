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
        public void Print(string text)
        {
            var printInfo = UIPrintInfo.PrintInfo;
            printInfo.OutputType = UIPrintInfoOutputType.General;
            printInfo.JobName = "Print Job " + DateTime.Now.ToString();

            var textFormatter = new UISimpleTextPrintFormatter(text)
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
