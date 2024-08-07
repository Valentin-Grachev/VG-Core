using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace VG
{
    public class Test_PurchaseService : PurchaseService
    {
        [SerializeField] private bool _useInBuild;

        [Header("Purchasing:")]
        [SerializeField] private GameObject _purchasePanel;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _rejectButton;
        [SerializeField] private TextMeshProUGUI _idText;

        public override bool supported => _useInBuild || Environment.editor;


        public override string GetPriceString(string productKey) => productKey.ToString();
        public override void Initialize() => InitCompleted();


        public override void Purchase(string productKey, Action<bool> onSuccess)
        {
            _purchasePanel.SetActive(true);
            _idText.text = productKey;

            _acceptButton.onClick.RemoveAllListeners();
            _acceptButton.onClick.AddListener(() =>
            {
                _purchasePanel.SetActive(false);
                onSuccess?.Invoke(true);
            });

            _rejectButton.onClick.RemoveAllListeners();
            _rejectButton.onClick.AddListener(() =>
            {
                _purchasePanel.SetActive(false);
                onSuccess?.Invoke(false);
            });
        }
    }
}



