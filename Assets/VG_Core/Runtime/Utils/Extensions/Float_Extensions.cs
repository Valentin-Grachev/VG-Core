
namespace VG
{
    public static class Float_Extensions
    {
        static string[] names = { "", "K", "M", "B", "T" };


        public static string ToShortNumber(this float value)
        {
            int n = 0;

            if (value < 1000f) return ((int)value).ToString();

            if (1000f <= value && value <= 9999f)
            {
                int head = (int)(value / 1000f);
                int tail = (int)(value % 1000f);

                return head.ToString() + " " + tail.ToString("000");
            }


            while (n + 1 < names.Length && value >= 1000f)
            {
                value /= 1000f;
                n++;
            }
            return value.ToString("0.##") + names[n];
        }

        public static string ToTimeString(this float value)
        {
            int seconds = (int)value;
            string secondStr = (seconds % 60).ToString();
            string minuteStr = (seconds / 60 % 60).ToString();
            string hourStr = (seconds / 3600).ToString();
            if (hourStr.Length == 1) hourStr = "0" + hourStr;
            if (minuteStr.Length == 1) minuteStr = "0" + minuteStr;
            if (secondStr.Length == 1) secondStr = "0" + secondStr;
            return hourStr + ":" + minuteStr + ":" + secondStr;
        }


    }
}


