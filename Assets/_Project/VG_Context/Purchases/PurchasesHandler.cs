

namespace VG
{
    public static class PurchasesHandler
    {
        public static bool ProductPurchased(string productKey)
        {
            switch (productKey)
            {
                case Key_Product.no_ads:
                    return Saves.Bool[Key_Save.ads_enabled].Value == false;
                    
                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
        }



        public static void HandlePurchase(string productKey)
        {

            switch (productKey)
            {
                case Key_Product.no_ads:
                    Saves.Bool[Key_Save.ads_enabled].Value = false;
                    break;

                case Key_Product.test_consumable:
                    Saves.Int[Key_Save.test_count].Value += 3;
                    break;

                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
            
        }


    }
}

