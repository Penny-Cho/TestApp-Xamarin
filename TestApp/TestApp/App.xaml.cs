using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        //public App()
        //{
        //    InitializeComponent();

        //}

        //database connection
        public App(string databaseLocation)
        {
            InitializeComponent();
            //NavigationPage가 전체 페이지를 감싸고 있는 Parent Page임. NaviationPage(new 루트페이지) 즉 시작화면을 정의
            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = databaseLocation;

        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
