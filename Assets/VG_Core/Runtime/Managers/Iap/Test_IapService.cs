using System;
using System.Collections.Generic;
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

        public override void Consume(string key_product) => Core.LogEditor("Consumed: " + key_product);

        public override void DeletePurchases() => Core.LogEditor("Purchases deleted.");


        public override string GetPriceString(string key_product) => key_product.ToString();


        public override void Initialize() => InitCompleted();

        public override void InitializeProducts(List<Iap.Product> products)
        {
            foreach (var product in products) 
                product.Initialize(0);
        }

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

        protected override void OnInitialized() { }
    }
}



