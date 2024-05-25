using System.Collections.Generic;
using VG.Internal;


namespace VG
{
    public class Analytics : Manager
    {

        private static Analytics instance;
        private static AnalyticsService service => instance.supportedService as AnalyticsService;

        protected override string managerName => "VG Analytics";



        protected override void OnInitialized()
        {
            instance = this;
            Log(Core.Message.Initialized(managerName));
        }


        public static void Track(string key_analytics, Dictionary<string, object> parameters)
        {
            service.Track(key_analytics, parameters);

            string message = "Event tracked: " + key_analytics;
            foreach (var parameter in parameters)
                message += "\n" + parameter.Key + ": " + parameter.Value.ToString();

            instance.Log(message);
        }


    }
}



