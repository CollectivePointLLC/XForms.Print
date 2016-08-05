using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XForms.Print.Services;

namespace XForms.Print
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void PrintBtnClicked(object sender, EventArgs e)
        {
            var service = DependencyService.Get<IPrinterService>();
            service.Print("hello XForms.Print");
        }
    }
}
