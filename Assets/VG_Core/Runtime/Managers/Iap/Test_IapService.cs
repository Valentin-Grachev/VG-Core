using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG.Internal;


namespace VG
{
    public class Test_IapService : IapService
    {
        [SerializeField] private bool _useInBuild;

        [Header("Purchasing:")]
        [SerializeField] private GameObject _purchasePanel;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _rejectButton;
        [SerializeField] private TextMeshProUGUI _idText;


        public override bool supported => _useInBuild || Environment.editor;

        public override void MarkAsConsumed(string key_product) => Core.LogEditor("Consumed: " + key_product);


        public override string GetPriceString(string key_product) => key_product.ToString();


        public override void Initialize() => InitCompleted();


        public override void Purchase(string key_product, Action<bool> onSuccess)
        {
            _purchasePanel.SetActive(true);
            _idText.text = key_product;

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



