using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostDetailPage : ContentPage
	{
        Post _selectedPost;
       
		public PostDetailPage (Post selectedPost)
		{
			InitializeComponent ();
            _selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;

		}

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App._db))
            {
                conn.CreateTable<Post>();
                int rows = conn.Update(_selectedPost);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience successfully updated", "Ok");
                }
                else
                {
                    DisplayAlert("Failed", "It FAILED", "Too bad");
                }
            };
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App._db))
            {
                conn.CreateTable<Post>();
                int rows = conn.Delete(_selectedPost);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience successfully deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Failed", "It FAILED", "Too bad");
                }
            };
        }
    }
}