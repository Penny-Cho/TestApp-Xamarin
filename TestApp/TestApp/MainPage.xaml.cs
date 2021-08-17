using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("TestApp.Assets.Images.plane.png", assembly);
        }

        private void loginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(email.Text);
            bool isPassordEmpty = string.IsNullOrEmpty(password.Text);

            if (isEmailEmpty || isPassordEmpty)
            {
                DisplayAlert("Alert", "password needed", "ok");
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }

        }
    }
}
