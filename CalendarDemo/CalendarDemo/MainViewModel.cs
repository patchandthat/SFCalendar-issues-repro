using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CalendarDemo
{
    class MainViewModel : PropertyChangedBase
    {
        private bool _isCalendarVisible;

        public bool IsCalendarVisible
        {
            get => _isCalendarVisible;
            private set => SetPropertyAndRaise( ref _isCalendarVisible, value);
        }

        public ICommand ToggleCalendarCommand { get; }

        public DateTime MinScrollDate { get; }
        public DateTime MaxScrollDate { get; }

        public IList<string> DayHeaders { get; }

        public int FirstDayOfWeek => (int) CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek;

        public MainViewModel()
        {
            IsCalendarVisible = false;
            ToggleCalendarCommand = new Command(() => IsCalendarVisible = !IsCalendarVisible);

            MinScrollDate = DateTime.Today.AddMonths(-3);
            MaxScrollDate = DateTime.Today.AddMonths(3);

            Debug.WriteLine(FirstDayOfWeek);
            Debug.WriteLine($"{CultureInfo.CurrentUICulture}: {CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek.ToString()}");

            DayHeaders = new List<string>()
            {
                "S",
                "M",
                "T",
                "W",
                "T",
                "F",
                "S"
            };
        }
    }

    class Command : ICommand
    {
        private readonly Action _execute;
        private readonly Func<object, bool> _canExecute;

        public Command(Action execute) : this(execute, _ => true)
        {
        }

        public Command(Action execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _execute();
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged<TProp>(Expression<Func<TProp>> property)
        {
            Expression body;
            var expression = property.Body as UnaryExpression;
            body = expression != null ? expression.Operand : property.Body;

            var member = body as MemberExpression;
            if (member == null)
                throw new ArgumentException("Property must be a MemberExpression");

            string propertyName = member.Member.Name;

            OnPropertyChanged(propertyName);
        }

        protected void RaiseAllPropertiesChanged()
        {
            OnPropertyChanged(string.Empty);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetPropertyAndRaise<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;

            OnPropertyChanged(propertyName);

            return true;
        }
    }
}

