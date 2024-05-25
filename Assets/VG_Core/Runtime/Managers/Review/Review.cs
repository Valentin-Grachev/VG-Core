using System;
using VG.Internal;

namespace VG
{
    public class Review : Manager
    {
        private static Review instance;
        private static ReviewService service => instance.supportedService as ReviewService;


        protected override string managerName => "VG Review";

        protected override void OnInitialized()
        {
            instance = this;
            Log(Core.Message.Initialized(managerName));
        }

        public static void Request(Action onHandled = null)
        {
            service.Request(onHandled);
            instance.Log("Requested.");
        }


    }
}


