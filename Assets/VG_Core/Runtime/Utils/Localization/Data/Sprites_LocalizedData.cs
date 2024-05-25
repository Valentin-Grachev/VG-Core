using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    [CreateAssetMenu(menuName = "VG/Localization/Sprites", fileName = "Sprites")]
    public class Sprites_LocalizedData : LocalizedData<Sprite_Translation>
    {
        public override void LoadData(Dictionary<string, Table> allTables) { }
    }
}

