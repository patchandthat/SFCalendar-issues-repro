using System;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarDemo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsingDataTemplateStartingHidden : ContentPage
	{
	    private CalendarEventDataSource _source = new CalendarEventDataSource();

		public UsingDataTemplateStartingHidden ()
		{
			InitializeComponent ();

            Calendar.OnMonthCellLoaded += CalendarOnOnMonthCellLoaded;
		}

	    private void CalendarOnOnMonthCellLoaded(object sender, MonthCell monthCell)
	    {
            // On android, if the calendar starts out hidden on a page, the data templates do not render!
            // Works fine on iOS, and on the previous Syncfusion version

	        var dataTemplateModel = _source.GetDayModelFor(monthCell.Date, Calendar);

	        monthCell.CellBindingContext = dataTemplateModel;
	    }

	    private void ToggleCalendar(object sender, EventArgs e)
	    {
	        CalendarFrame.IsVisible = !CalendarFrame.IsVisible;
	    }
	}
}