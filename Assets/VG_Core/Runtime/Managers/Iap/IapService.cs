using System;
using System.Collections.Generic;
using VG.Internal;

namespace VG
{
    public abstract class IapService : Service
    {
        public abstract void InitializeProducts(List<Iap.Product> products);

        public abstract string GetPriceString(string key_product);
        public abstract void Purchase(string key_product, Action<bool> onSuccess);

        public abstract void Consume(string key_product);

        public abstract void DeletePurchases();

    }
}



