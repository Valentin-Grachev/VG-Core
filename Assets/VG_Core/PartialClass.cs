using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class PartialClass
{
    public static class Ads
    {
        public static string ad;
    }
}



public static partial class PartialClass
{
    public static class Purch
    {
        public static string ad;
    }
}


public class Program
{
    public void Test()
    {
        PartialClass.Purch.ad = "da";
        PartialClass.Ads.ad = "d";
    }
}
