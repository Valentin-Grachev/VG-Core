using System.Collections.Generic;
using UnityEngine;


namespace VG.Internal
{
    public abstract class Manager : Initializable
    {
        [SerializeField] private bool _debugLogs;
        [SerializeField] private List<Service> _services;

        protected Service supportedService { get; set; }

        protected abstract string managerName { get; }

        public override void Initialize()
        {
            supportedService = GetSupportedService();
            supportedService.onInitialized += InitCompleted;
            supportedService.Initialize();
        }

        protected Service GetSupportedService()
        {
            foreach (var service in _services)
                if (service.supported) return service;

            Core.Error.NoSupportedService(managerName);
            return null;
        }

        protected void Log(string message)
        {
            if (_debugLogs) Debug.Log(Core.Prefix(managerName) + message);
        }

    }
}



