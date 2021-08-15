using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using TestApp.Model;

namespace TestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }


        //override시 유저가 새로운 값을 입력한 후 다시 조회할 때 신규 추가된 내용이 반영됨(cache는 안하는가?)
        protected override void OnAppearing()
        {
            base.OnAppearing();


            //conn.Close() 호출을 까먹었을 때를 대비해서, Idisposable이 내장된 SQLiteConnection을 Using하는 식으로 코드를 짜면 자동으로 할일을 끝낸 후 디비를 닫아줌.
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                postListView.ItemsSource = posts;
            }

        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;
            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}