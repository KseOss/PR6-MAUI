using Microsoft.Maui.Controls;
namespace PR6_MAUI;

public partial class AlarmPage : ContentPage
{
    private TimeSpan _alarmTime;
    private IDispatcherTimer _timer;
    public AlarmPage()
	{
		InitializeComponent(); 
        SetupAlarmTimer();
    }

    private void SetupAlarmTimer()
    {
        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += OnAlarmTimerTick;
    }

    private void OnAlarmToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            _alarmTime = timePicker.Time;
            timePicker.IsEnabled = false;
            _timer.Start();
            statusLabel.Text = "Будильник включен";
            statusLabel.TextColor = Colors.Green;
        }
        else
        {
            timePicker.IsEnabled = true;
            _timer.Stop();
            statusLabel.Text = "Будильник выключен";
            statusLabel.TextColor = Colors.Gray;
        }
    }

    private void OnAlarmTimerTick(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        TimeSpan currentTime = new TimeSpan(now.Hour, now.Minute, 0);

        if (_alarmTime.CompareTo(currentTime) == 0)
        {
            if (CheckDayOfWeek(now.DayOfWeek))
            {
                _timer.Stop();
                ShowAlarmNotification();
            }
        }
    }

    private bool CheckDayOfWeek(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => mondaySwitch.IsToggled,
            DayOfWeek.Tuesday => tuesdaySwitch.IsToggled,
            DayOfWeek.Wednesday => wednesdaySwitch.IsToggled,
            DayOfWeek.Thursday => thursdaySwitch.IsToggled,
            DayOfWeek.Friday => fridaySwitch.IsToggled,
            DayOfWeek.Saturday => saturdaySwitch.IsToggled,
            DayOfWeek.Sunday => sundaySwitch.IsToggled,
            _ => false
        };
    }

    private async void ShowAlarmNotification()
    {
        await DisplayAlert("Будильник", "Просыпайтесь! Время вставать!", "OK");
        alarmSwitch.IsToggled = false;
    }
}