using TopoLocoApp.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace TopoLocoApp
{
    public partial class App : Application
    {
        public static TopoLocoViewModel ViewModel = new();
        
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}