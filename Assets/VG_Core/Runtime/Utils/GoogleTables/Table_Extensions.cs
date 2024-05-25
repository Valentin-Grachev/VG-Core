using UnityEngine;


namespace VG
{
    public static class Table_Extensions
    {

        public static float GetFloat(this Table table, int row, Column column)
        {
            float result = -1;

            string dataString = table.Get(row, column);
            if (dataString == string.Empty || dataString.StartsWith('-')) return result;

            if (!float.TryParse(dataString, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.LogError($"Table {table.key}: Error parse Float {dataString}, " +
                    $"row - {row}, column - {column}.");
            }

            return result;
        }

        public static int GetInt(this Table table, int row, Column column)
        {
            int result = -1;

            string dataString = table.Get(row, column);
            if (dataString == string.Empty || dataString.StartsWith('-')) return result;

            if (!int.TryParse(dataString, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.LogError($"Table {table.key}: Error parse Int {dataString}, " +
                    $"row - {row}, column - {column}.");
            }

            return result;
        }

    }
}


