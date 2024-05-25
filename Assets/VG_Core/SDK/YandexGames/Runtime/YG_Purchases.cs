using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace VG.YandexGames
{
    public class YG_Purchases : MonoBehaviour
    {
        public static bool available 
        { 
            get 
            {
#if UNITY_WEBGL
                CheckPaymentsAvailable();
#endif

                return _paymentsAvailable;
            } 
        }

        public static void Purchase(string productId, Action<bool> onPurchasedSuccess)
        {
            _onPurchasedSuccess = onPurchasedSuccess;
#if UNITY_WEBGL
            RequestPurchasing(productId);
#endif

        }

        public static void GetPurchasedProducts(Action<List<string>> onPurchasedProductIdsReceived)
        {
            _onPurchasesReceived = onPurchasedProductIdsReceived;
#if UNITY_WEBGL
            RequestPurchasesIds();
#endif

        }

        public static void GetPrices(Action<Dictionary<string, string>> onPricesReceived)
        {
            _onPricesReceived = onPricesReceived;

#if UNITY_WEBGL
            GetCatalogPrices();
#endif

        }


        public static void Consume(string productId)
        {
#if UNITY_WEBGL
            RequestPurchaseConsuming(productId);
#endif

        }






        #region Internal

        private static bool _paymentsAvailable = false;


        
        private void HTML_OnPaymentsAvailableChecked(int available) => _paymentsAvailable = Convert.ToBoolean(available);



        private static event Action<bool> _onPurchasedSuccess;
        
        private void HTML_OnPurchaseHandled(int success) => _onPurchasedSuccess?.Invoke(Convert.ToBoolean(success));



        private static event Action<List<string>> _onPurchasesReceived;
        
        private void HTML_OnPurchasesReceived(string purchasedProductIdsString)
        {
            string[] purchasedProductIds = purchasedProductIdsString.Split(',');
            List<string> purchasedProductIdsList = new List<string>(purchasedProductIds);
            _onPurchasesReceived?.Invoke(purchasedProductIdsList);
        }


        private static event Action<Dictionary<string, string>> _onPricesReceived;
        
        private void HTML_OnPricesReceived(string pricesData)
        {
            var productPrices = new Dictionary<string, string>();

            string[] productPriceStrings = pricesData.Split(';');

            foreach (string productPriceString in productPriceStrings)
            {
                if (productPriceString == string.Empty) continue;

                string[] idPricePair = productPriceString.Split(',');
                string productId = idPricePair[0];
                string price = idPricePair[1];
                productPrices.Add(productId, price);
            }
            _onPricesReceived?.Invoke(productPrices);
        }




#endregion


#if UNITY_WEBGL
        [DllImport("__Internal")] private static extern void CheckPaymentsAvailable();
        [DllImport("__Internal")] private static extern void RequestPurchasing(string productId);
        [DllImport("__Internal")] private static extern void RequestPurchaseConsuming(string productId);
        [DllImport("__Internal")] public static extern void InitializePayments();
        [DllImport("__Internal")] private static extern void RequestPurchasesIds();
        [DllImport("__Internal")] private static extern void GetCatalogPrices();

#endif

    }
}


