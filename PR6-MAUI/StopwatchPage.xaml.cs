using Microsoft.Maui.Controls;
namespace PR6_MAUI;

public partial class StopwatchPage : ContentPage
{
    private int _seconds = 0;
    private bool _timerIsRun = false;
    public StopwatchPage()
	{
		InitializeComponent();
	}
    private async void OnStartClicked(object sender, EventArgs e)
    {
        _timerIsRun = true;
        btnStart.IsEnabled = false;
        btnStop.IsEnabled = true;
        btnPitStop.IsEnabled = true;
        btnClear.IsEnabled = false;

        while (_timerIsRun)
        {
            _seconds += 1;
            TimeSpan time = TimeSpan.FromSeconds(_seconds);
            timeLabel.Text = time.ToString(@"hh\:mm\:ss");
            await Task.Delay(1000);
        }
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        _timerIsRun = false;
        btnStart.IsEnabled = true;
        btnStop.IsEnabled = false;
        btnPitStop.IsEnabled = false;
        btnClear.IsEnabled = true;
    }

    private void OnPitStopClicked(object sender, EventArgs e)
    {
        Label lapLabel = new Label();
        TimeSpan time = TimeSpan.FromSeconds(_seconds);
        lapLabel.Text = time.ToString(@"hh\:mm\:ss");
        lapLabel.FontSize = 16;
        pitStopContainer.Children.Add(lapLabel);
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        _seconds = 0;
        timeLabel.Text = "00:00:00";
        pitStopContainer.Children.Clear();
        btnClear.IsEnabled = false;
    }
}