namespace StevenVolckaert.VrtNu.Android
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::Android.Support.V17.Leanback.App;
    using global::Android.Util;

    public class BrowseViewFragment : BrowseFragment
    {
        private static readonly string _className = nameof(BrowseViewFragment);

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            // TODO Create a LogService, and use that to log messages.
            Log.Info(_className, nameof(OnActivityCreated));
            base.OnActivityCreated(savedInstanceState);

            //LoadVideoData();
            InitializeUIElements();
            //InitializeEventListeners();
        }

        private void InitializeUIElements()
        {
            Title = GetString(Resource.String.BrowseViewTitle);
            HeadersState = HeadersEnabled;
            HeadersTransitionOnBackEnabled = true;
            //BrandColor = Resources.GetColor(Resource.Color.fastlane_background);
            //SearchAffordanceColor = Resources.GetColor(Resource.Color.search_opaque);
        }
    }
}
