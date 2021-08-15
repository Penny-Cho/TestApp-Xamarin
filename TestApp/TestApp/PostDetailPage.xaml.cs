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
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;

        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            this.selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;

        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Update(selectedPost);

                if (rows > 0) DisplayAlert("Success", "Post has been updated", "OK");
                else DisplayAlert("Failure", "failed to update", "OK");
            }

        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {

            bool answer = await DisplayAlert("Confirm", "Are you sure to delete the item?", "Yes", "No");
            if (answer)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    int rows = conn.Delete(selectedPost);

                }

                await Navigation.PushAsync(new HistoryPage());
            }
        }
    }
}