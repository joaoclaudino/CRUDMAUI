using MauiCrud.MVVM.View;
using MauiCrud.Service;

namespace MauiCrud
{
    public partial class App : Application
    {
        public App(IDbService dbService, IErrorService errorService)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new MenuPage(dbService, errorService);
        }
    }
}
