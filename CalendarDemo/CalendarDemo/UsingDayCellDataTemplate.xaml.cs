using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarDemo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsingDayCellDataTemplate : ContentPage
	{
	    private CalendarEventDataSource _source = new CalendarEventDataSource();

        public UsingDayCellDataTemplate ()
		{
			InitializeComponent ();

            Calendar.OnMonthCellLoaded += CalendarOnOnMonthCellLoaded;
		}

	    private void CalendarOnOnMonthCellLoaded(object sender, MonthCell monthCell)
	    {
	        var dataTemplateModel = _source.GetDayModelFor(monthCell.Date, Calendar);

	        monthCell.CellBindingContext = dataTemplateModel;
	    }
    }
}