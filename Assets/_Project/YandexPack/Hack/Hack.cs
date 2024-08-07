using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using VG;

public class Hack : Initializable
{
    [SerializeField] private bool alwaysRelease;
    [SerializeField] private int hackPrice;
    [SerializeField] private Initializable _iap;

    public static bool release { get; private set; }

    public static void SetRelease() => PlayerPrefs.SetInt("Released", 1);

    public override void Initialize()
    {
        if (PlayerPrefs.GetInt("Released", 0) == 1 || alwaysRelease)
        {
            release = true;
            InitCompleted();
            return;
        }

        if (Environment.editor)
        {
            release = false;
            InitCompleted();
            return;
        }

        StartCoroutine(WaitIap());
    }


    private IEnumerator WaitIap()
    {
        yield return new WaitUntil(() => _iap.initialized);

        release = priceIsRelease;
        InitCompleted();
    }

    private bool priceIsRelease
    {
        get
        {
            string priceString = Purchases.GetPriceString(Key_Product.no_ads);
            Debug.Log($"Price = {priceString}");

            if (priceString == string.Empty)
            {
                Debug.Log($"Price is empty");
                return false;
            }

            string[] numbers = Regex.Split(priceString, @"\D+");
            Debug.Log("Splitted Price = " + numbers[0]);
            int price = int.Parse(numbers[0]);
            Debug.Log("Parsed int = " + price);

            return price >= hackPrice;
        }
    }




}
