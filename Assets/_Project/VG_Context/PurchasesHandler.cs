

namespace VG
{
    public static class PurchasesHandler
    {
        public static bool ProductPurchased(string key_product)
        {
            switch (key_product)
            {
                case Key_Product.no_ads:
                    return Saves.Bool[Key_Save.ads_enabled].Value == false;
                    
                default: throw new System.Exception("Wrong product: " + key_product.ToString());
            }
        }



        public static void HandlePurchase(string key_product)
        {

            switch (key_product)
            {
                case Key_Product.no_ads:
                    Saves.Bool[Key_Save.ads_enabled].Value = false;
                    break;

                default: throw new System.Exception("Wrong product: " + key_product.ToString());
            }
            
        }


    }
}

