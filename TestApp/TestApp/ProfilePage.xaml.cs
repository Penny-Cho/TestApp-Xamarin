using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var postTable = conn.Table<Post>().ToList();
                //var categories = (from p in postTable orderby p.CategoryId select p.CategoryName).Distinct().ToList();

                var categories = postTable.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

                //fitering data into dictionary
                Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

                foreach (var category in categories)
                {
                    //var count = (from post in postTable where post.CategoryName == category select post).ToList().Count;

                    var count = postTable.Where(p => p.CategoryName == category).ToList().Count;

                    if (category != null)
                    {
                        categoriesCount.Add(category, count);
                    }
                    else
                    {
                        categoriesCount.Add("Uncategorized", count);
                    }


                }

                categoriesListView.ItemsSource = categoriesCount;

                postCountLabel.Text = postTable.Count.ToString();
            }

        }

    }
}