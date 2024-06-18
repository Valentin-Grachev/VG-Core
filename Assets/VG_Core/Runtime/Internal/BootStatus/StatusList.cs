using System.Collections.Generic;
using UnityEngine;


namespace VG.Internal
{
    public class StatusList : MonoBehaviour
    {
        [SerializeField] private bool _showStatusList;
        [Space(10)]
        [SerializeField] private Startup _startup;
        [SerializeField] private StatusItem _statusItemPrefab;
        [SerializeField] private Transform _container;

        private Dictionary<Initializable, StatusItem> _initDictionary;


        private void Start()
        {
            enabled = _showStatusList;
            _container.gameObject.SetActive(_showStatusList);
            _initDictionary = new Dictionary<Initializable, StatusItem>();

            foreach (var initializable in _startup.initializables)
            {
                var statusItem = Instantiate(_statusItemPrefab, _container);
                statusItem.SetName(initializable.name);
                _initDictionary.Add(initializable, statusItem);
            }
        }



        private void Update()
        {
            if (Startup.loaded)
            {
                enabled = false;
                _container.gameObject.SetActive(false);
            }

            foreach (var initializable in _startup.initializables)
                _initDictionary[initializable].SetStatus(initializable.initialized);
        }

    }
}

