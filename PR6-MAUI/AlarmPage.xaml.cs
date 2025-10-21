using Microsoft.Maui.Controls;
using Plugin.LocalNotification;
namespace PR6_MAUI;

public partial class AlarmPage : ContentPage
{
    private TimeSpan _alarmTime; //Время срабатываения будильеика
    private IDispatcherTimer _timer; //Таймер для проверки текузего времени
    public AlarmPage()
	{
		InitializeComponent(); 
        SetupAlarmTimer(); //Настройка таймер будильника
    }

    private void SetupAlarmTimer()
    {
        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1); //мнтервал проверки - 1 секунда
        _timer.Tick += OnAlarmTimerTick; //событие тика таймера
    }

    private void OnAlarmToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value) //если будильник включен
        {
            _alarmTime = timePicker.Time; //сохраняем выбранное время
            timePicker.IsEnabled = false; //блокируем выбор времени
            _timer.Start(); //запуск таймер проверкт
            statusLabel.Text = "Будильник включен";
            statusLabel.TextColor = Colors.Green;
        }
        else //если выключен
        {
            timePicker.IsEnabled = true; //Разблокируем выбор времени
            _timer.Stop(); //Останавливаем таймер
            statusLabel.Text = "Будильник выключен";
            statusLabel.TextColor = Colors.Gray;
        }
    }

    private void OnAlarmTimerTick(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now; 
        TimeSpan currentTime = new TimeSpan(now.Hour, now.Minute, 0); //получаем текущее время без секунд для точного сравнения

        if (_alarmTime.CompareTo(currentTime) == 0) //сравнение текущего времени с временем будильника
        {
            if (CheckDayOfWeek(now.DayOfWeek)) //проверка, установлен ли будильник на текущий день недели
            {
                _timer.Stop(); //оставка таймера
                ShowAlarmNotification(); //показываем уведомление
            }
        }
    }

    private bool CheckDayOfWeek(DayOfWeek day)
    {
        return day switch //проверка дня недели
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
        // Добавляем код уведомления
        var request = new NotificationRequest()
        {
            NotificationId = 1332, //индентификатор уведомления
            Title = "Будильник", //заголовок уведомления
            Subtitle = "Просыпайтесь!", //Подзаголовок
            Description = "Время вставать!", //основной текст уведомлеия
            BadgeNumber = 42, //для ios
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(5), //Когда показать уведомление
                NotifyRepeatInterval = TimeSpan.FromDays(1), //Повторять каждый день
            },
        };
        LocalNotificationCenter.Current.Show(request); //Показ уведомления

        alarmSwitch.IsToggled = false; //Выключаем будильник после срабатывания
    }
}
