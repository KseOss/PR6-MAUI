using Microsoft.Maui.Controls;
namespace PR6_MAUI
{
    public partial class MainPage : ContentPage
    {
        IDispatcherTimer _timer;
        public MainPage()
        {
            InitializeComponent(); 
            SetupClock();
        }

        private void SetupClock()
        {
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) => UpdateClock();
            _timer.Start();

            UpdateClock();
        }

        private void UpdateClock()
        {
            DateTime now = DateTime.Now;
            timeLabel.Text = now.ToString("HH:mm:ss");
            dateLabel.Text = now.ToString("dddd, dd MMMM yyyy");
        }
    }
}
