using Microsoft.Maui.Controls;
namespace PR6_MAUI;

public partial class TimerPage : ContentPage
{
    private TimeSpan _time;
    private IDispatcherTimer _timer;
    public TimerPage()
	{
		InitializeComponent(); 
        SetupTimer();
    }

    private void SetupTimer()
    {
        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += OnTimerTick;
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        if (_time.TotalSeconds == 0)
        {
            _time = timePicker.Time;
        }

        _timer.Start();
        btnStart.IsEnabled = false;
        btnStop.IsEnabled = true;
        btnReset.IsEnabled = false;
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        _timer.Stop();
        btnStart.IsEnabled = true;
        btnStop.IsEnabled = false;
        btnReset.IsEnabled = true;
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        _timer.Stop();
        _time = new TimeSpan(0, 0, 0);
        timeLabel.Text = "00:00:00";
        timePicker.Time = new TimeSpan(0, 0, 0);
        btnStart.IsEnabled = true;
        btnStop.IsEnabled = false;
        btnReset.IsEnabled = false;
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        if (_time.CompareTo(new TimeSpan(0, 0, 0)) > 0)
        {
            _time = _time.Add(new TimeSpan(0, 0, -1));
            timeLabel.Text = _time.ToString(@"hh\:mm\:ss");
        }
        else
        {
            _timer.Stop();
            timeLabel.Text = "00:00:00";
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnReset.IsEnabled = true;

            ShowNotification("Таймер", "Время вышло!");
        }
    }

    private async void ShowNotification(string title, string message)
    {
        await DisplayAlert(title, message, "OK");
    }
}