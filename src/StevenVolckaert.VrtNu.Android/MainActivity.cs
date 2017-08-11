namespace StevenVolckaert.VrtNu.Android
{
    using global::Android.App;
    using global::Android.OS;

    [Activity(
        Label = "@string/ApplicationName",
        Icon = "@drawable/vrt_nu_banner",
        MainLauncher = true,
        Enabled = false
    )]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //SetContentView(Resource.Layout.Main);
        }
    }
}
