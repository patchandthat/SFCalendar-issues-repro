using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalendarDemo
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private void HiddenCalendar_OnClicked(object sender, EventArgs e)
	    {
	        this.Navigation.PushAsync(new UsingDataTemplateStartingHidden());
	    }

	    private void DataTemplateButton_OnClicked(object sender, EventArgs e)
	    {
	        this.Navigation.PushAsync(new UsingDayCellDataTemplate());
	    }

	    private void AsyncDataHiddenCalendar_OnClicked(object sender, EventArgs e)
	    {
	        this.Navigation.PushAsync(new UsingHiddenCalendarWithAsyncData());
        }
	}
}
