using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VG
{
    [CreateAssetMenu(menuName = "VG/TableHub", fileName = "TableHub")]
    public class TableHub : LoadableFromTable
    {
        [SerializeField] private List<LoadableFromTable> _loadables;


        public override void LoadData(Dictionary<string, Table> allTables)
        {
            foreach (var loadable in _loadables)
            {
#if UNITY_EDITOR
                EditorUtility.SetDirty(loadable);
#endif

                loadable.LoadData(allTables);
            }
                
        }
    }
}


