namespace StevenVolckaert.VrtNu.Android
{
    using global::Android.App;
    using global::Android.Content;
    using global::Android.Content.PM;
    using global::Android.OS;

    [Activity(
        Label = "@string/ApplicationName"
        , Icon = "@drawable/vrt_nu_banner"
        , Theme = "@style/Theme.Leanback"
        , ScreenOrientation = ScreenOrientation.Landscape
        //, MainLauncher = true
    )]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new string[] { Intent.CategoryLeanbackLauncher })]
    public class MainTvActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainTvLayout);
        }
    }
}
