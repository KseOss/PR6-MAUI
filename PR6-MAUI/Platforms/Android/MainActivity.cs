using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace PR6_MAUI
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState) //преопределение метода занаия Activity
        {
            base.OnCreate(savedInstanceState); //выов базовой реаиации метода OnCreate
            const int requestNotifictation = 0; //константа для индетификации запроса разрешения на уведомления
            string[] notiPermission = //массив строк с разрешениями, которые мы хтим запроить
                { 
        Manifest.Permission.PostNotifications //разрешение на отправку уведомлений
    };
            if ((int)Build.VERSION.SdkInt < 33) //проверка версии Android SDK - 33 соответствует Android 13, где появилось новое разрешение на увеомление
                return; //если версия ниже 13, выходим - разрешение не требуется
            if (CheckSelfPermission(Manifest.Permission.PostNotifications) == Permission.Granted) //проверяем, есть ли уже у нас разрешение на отправку уведомления
                return; //если разррешение уже представленно, выходим
            //запрашиваем разрешение у пользователя. notiPermission - код запроса для индентификации в callback
            RequestPermissions(notiPermission, requestNotifictation);
        }
    }
}
