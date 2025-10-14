using Microsoft.Maui.Controls;
namespace PR6_MAUI;

public partial class ReminderPage : ContentPage
{
    private IDispatcherTimer _timer;
    private List<Reminder> _reminders = new List<Reminder>();
    public ReminderPage()
	{
		InitializeComponent(); 
        SetupReminderTimer();
    }

    private void SetupReminderTimer()
    {
        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += OnReminderTimerTick;
        _timer.Start();
    }

    private void OnSetReminderClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(messageEntry.Text))
        {
            DisplayAlert("Ошибка", "Введите сообщение напоминания", "OK");
            return;
        }

        DateTime reminderDateTime = datePicker.Date + timePicker.Time;

        if (reminderDateTime <= DateTime.Now)
        {
            DisplayAlert("Ошибка", "Выберите дату и время в будущем", "OK");
            return;
        }

        Reminder newReminder = new Reminder
        {
            DateTime = reminderDateTime,
            Message = messageEntry.Text
        };

        _reminders.Add(newReminder);
        UpdateRemindersList();

        statusLabel.Text = $"Напоминание установлено на {reminderDateTime:dd.MM.yyyy HH:mm}";
        statusLabel.TextColor = Colors.Green;

        messageEntry.Text = string.Empty;
    }

    private void OnReminderTimerTick(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;

        for (int i = _reminders.Count - 1; i >= 0; i--)
        {
            if (_reminders[i].DateTime <= now)
            {
                ShowReminderNotification(_reminders[i].Message);
                _reminders.RemoveAt(i);
                UpdateRemindersList();
            }
        }
    }

    private void UpdateRemindersList()
    {
        remindersContainer.Children.Clear();

        foreach (var reminder in _reminders.OrderBy(r => r.DateTime))
        {
            Label reminderLabel = new Label
            {
                Text = $"{reminder.DateTime:dd.MM.yyyy HH:mm} - {reminder.Message}",
                FontSize = 14
            };
            remindersContainer.Children.Add(reminderLabel);
        }
    }

    private async void ShowReminderNotification(string message)
    {
        await DisplayAlert("Напоминание", message, "OK");
    }
}

public class Reminder
{
    public DateTime DateTime { get; set; }
    public string Message { get; set; }
}