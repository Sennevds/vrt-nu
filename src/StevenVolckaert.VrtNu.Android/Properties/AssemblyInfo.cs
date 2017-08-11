using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Android.App;
using Android;

[assembly: AssemblyTitle("StevenVolckaert.VrtNu.Android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("StevenVolckaert.VrtNu.Android")]
[assembly: AssemblyCopyright("Copyright © 2017 Steven Volckaert")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: UsesPermission(Manifest.Permission.Internet)]
[assembly: UsesPermission(Manifest.Permission.RecordAudio)]
// TODO Replace string literals with constants provided by Xamarin.Android. Steven Volckaert. August 11, 2017.
[assembly: UsesFeature("android.hardware.touchscreen", Required = false)]
[assembly: UsesFeature("android.software.leanback", Required = false)]

[assembly: Application(Banner = "@drawable/vrt_nu_banner")]
