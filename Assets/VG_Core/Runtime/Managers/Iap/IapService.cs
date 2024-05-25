using System;
using System.Collections.Generic;
using VG.Internal;

namespace VG
{
    public abstract class IapService : Service
    {
        public abstract string GetPriceString(string key_product);
        public abstract void Purchase(string key_product, Action<bool> onSuccess);


        public virtual void MarkAsConsumed(string key_product) { }
        public virtual Dictionary<string, int> GetPurchasesQuantity()
            => new Dictionary<string, int>();

    }
}



