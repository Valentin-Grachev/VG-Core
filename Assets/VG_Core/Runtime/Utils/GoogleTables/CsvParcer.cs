using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    public static class CsvParcer
    {

        public static List<List<string>> Parce(string csvData)
        {
            List<string> rows = SplitToRows(csvData);
            List<List<string>> words = new List<List<string>>();
            foreach (string row in rows)
            {
                var wordRow = SplitToWords(row);
                words.Add(wordRow);
            }

            return words;
        }



        private static List<string> SplitToRows(string csvData)
        {
            List<string> result = new List<string>();
            bool ignoreSplit = false;
            string row = string.Empty;

            for (int i = 0; i < csvData.Length; i++)
            {
                char symbol = csvData[i];
                if (symbol == '\"') ignoreSplit = !ignoreSplit;
                if (symbol == '\n' && !ignoreSplit)
                {
                    result.Add(row);
                    row = string.Empty;
                    continue;
                }
                row += symbol;
            }

            result.Add(row);

            return result;
        }

        private static List<string> SplitToWords(string csvRowData)
        {
            List<string> result = new List<string>();
            bool ignoreSplit = false;
            string word = string.Empty;

            for (int i = 0; i < csvRowData.Length; i++)
            {
                char symbol = csvRowData[i];
                if (symbol == '\"')
                {
                    ignoreSplit = !ignoreSplit;
                    continue;
                }
                    
                if (symbol == ',' && !ignoreSplit)
                {
                    result.Add(word);
                    word = string.Empty;
                    continue;
                }

                word += symbol;
            }

            result.Add(word);

            return result;
        }



    }
}

