using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VG.Internal
{
    public abstract class Manager : Initializable
    {
        [SerializeField] private bool _debugLogs;
        [SerializeField] private List<Service> _services;
        [SerializeField] private List<Initializable> _waitInitializations;

        protected Service supportedService { get; private set; }

        protected abstract string managerName { get; }

        public sealed override void Initialize()
        {
            _waitInitializations ??= new List<Initializable>();

            foreach (var service in _services)
                if (service.supported)
                {
                    _waitInitializations.Add(service);
                    supportedService = service;
                    supportedService.Initialize();
                    break;
                }


            if (supportedService == null) Core.Error.NoSupportedService(managerName);

            if (_waitInitializations != null && _waitInitializations.Count > 0)
                StartCoroutine(WaitInitializations());

            else InitCompleted();
        }


        private IEnumerator WaitInitializations()
        {
            bool initialized = false;

            while(!initialized)
            {
                initialized = true;

                foreach (var waitableInitialization in _waitInitializations)
                    if (!waitableInitialization.initialized && waitableInitialization.gameObject.activeInHierarchy)
                    {
                        initialized = false;
                        break;
                    }

                yield return null;
            }

            InitCompleted();
        }



        protected void Log(string message)
        {
            if (_debugLogs) Debug.Log(Core.Prefix(managerName) + message);
        }

    }
}



