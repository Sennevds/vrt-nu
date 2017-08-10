namespace StevenVolckaert.VrtNu.Android
{
    using global::Android.App;
    using global::Android.OS;
    using global::Android.Widget;

    [Activity(
        Label = "@string/ApplicationName",
        Icon = "@drawable/vrt_nu_banner",
        ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Landscape,
        MainLauncher = true
    )]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
        }
    }
}
