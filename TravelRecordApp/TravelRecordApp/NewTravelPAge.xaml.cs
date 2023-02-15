﻿using SQLite;
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
	public partial class NewTravelPAge : ContentPage
	{
		public NewTravelPAge ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			Post post = new Post()
			{
				Experience = experienceEntry.Text
			};

			using (SQLiteConnection conn = new SQLiteConnection(App._db))
			{
                conn.CreateTable<Post>();
                int rows = conn.Insert(post);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience successfully inserted", "Ok");
                }
                else
                {
                    DisplayAlert("Failed", "It FAILED", "Too bad");
                }
            };
        }
    }
}