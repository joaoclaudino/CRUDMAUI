using MAUICRUD.MVVM.View;
using MAUICRUD.Service;

namespace MAUICRUD
{
    public partial class App : Application
    {
        public App(IDBService DBService, IErrorService errorService)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new MenuPage(DBService, errorService);
        }
    }
}
