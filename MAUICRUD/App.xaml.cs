using MauiCrud.MVVM.View;
using MauiCrud.Service;
using System.Globalization;
using System.Globalization;

namespace MauiCrud
{
    public partial class App : Application
    {
        public App(IDbService dbService, IErrorService errorService)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWXZceXVVRWlfVEZzW0s=");
            InitializeComponent();
            var culture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            //MainPage = new AppShell();
            MainPage = new MenuPage(dbService, errorService);
        }
    }
}
