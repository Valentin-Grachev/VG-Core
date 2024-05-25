using UnityEngine;


namespace VG
{
    public static class String_Exstensions
    {
        public static int ExtractInt(this string str)
        {
            string resultInt = string.Empty;
            bool begin = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i])) { resultInt += str[i]; begin = true; }
                else if (begin) break;
            }
            if (resultInt != string.Empty) return System.Convert.ToInt32(resultInt);
            else return 0;
        }


    }
}


