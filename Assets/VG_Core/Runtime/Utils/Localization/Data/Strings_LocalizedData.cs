using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    [CreateAssetMenu(menuName = "VG/Localization/Strings", fileName = "Strings")]
    public class Strings_LocalizedData : LocalizedData<String_Translation> 
    {

        public override void LoadData(Dictionary<string, Table> allTables)
        {
            _translations = new List<String_Translation>();

            foreach (var tableKey in Key_Table.localization_tables)
            {
                var table = allTables[tableKey];

                for (int i = 2; i <= table.rows; i++)
                {
                    var translation = new String_Translation();
                    translation.key = table.Get(row: i, column: 0);

                    translation.ru = table.Get(row: i, Column.B);
                    translation.en = table.Get(row: i, Column.C);
                    translation.tr = table.Get(row: i, Column.D);

                    _translations.Add(translation);
                }
            }
        }
    }
}


