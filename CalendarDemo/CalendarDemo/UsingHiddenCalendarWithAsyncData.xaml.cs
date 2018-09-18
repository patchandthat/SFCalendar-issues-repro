using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarDemo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsingHiddenCalendarWithAsyncData : ContentPage
	{
	    private CalendarEventDataSource _source;

	    public UsingHiddenCalendarWithAsyncData ()
		{
			InitializeComponent ();

		    Calendar.OnMonthCellLoaded += CalendarOnOnMonthCellLoaded;

		    Task.Run(async () =>
		    {
                // Some api call to load events etc before calendar is ready to open
		        await Task.Delay(TimeSpan.FromSeconds(2.5));
                _source = new CalendarEventDataSource();

		    }).ContinueWith(
		        (t, o) =>
		        {
                    // Calendar can be opened by the user
		            ToggleButton.IsEnabled = true;
		        }, 
		        TaskContinuationOptions.AttachedToParent, 
		        TaskScheduler.FromCurrentSynchronizationContext());
		}
        
	    private void CalendarOnOnMonthCellLoaded(object sender, MonthCell monthCell)
	    {
            // On android this happens way before the calendar presents
            // So data is not ready to load into the data template

            // On iOS, this happens when the calendar opens, so data is available

	        var dataTemplateModel = _source?.GetDayModelFor(monthCell.Date, Calendar);

	        monthCell.CellBindingContext = dataTemplateModel;
	    }

	    private void ToggleCalendar(object sender, EventArgs e)
	    {
	        CalendarFrame.IsVisible = !CalendarFrame.IsVisible;
	    }
	}
}