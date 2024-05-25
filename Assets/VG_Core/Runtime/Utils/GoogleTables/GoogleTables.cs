using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



namespace VG
{
    public class GoogleTables : Initializable
    {
        [SerializeField] private bool _requestDataBeforeStart; 
        [SerializeField] private List<Table> _tables;
        [SerializeField] private List<LoadableFromTable> _loadables;

        private Dictionary<string, Table> tables;

        private static GoogleTables instance;

        private static Action<bool> onSuccess;


        public override void Initialize()
        {
            instance = this;

            if (_requestDataBeforeStart && Environment.editor) RequestData();
            else InitCompleted();
        }

        public static void LoadData(Action<bool> success)
        {
            onSuccess = success;
            instance.RequestData();
        }

        [Dropdown(nameof(GetTableNames))]
        [SerializeField] private string _openTable;


        [Button("Open Table")]
        private void OpenTableURL() 
            => Application.OpenURL(_tables[GetTableIndex(_openTable)].checkUrl);



        [Button("Load Data")]
        private void RequestData()
        {
            tables = new Dictionary<string, Table>();

            foreach (var table in _tables)
            {
                tables.Add(table.key, table);
                StartCoroutine(table.RequestData());
            }

            StartCoroutine(WaitData());
        }


        private int GetTableIndex(string tableKey)
        {
            for (int i = 0; i < _tables.Count; i++)
                if (_tables[i].key == tableKey) return i;
            return -1;
        }

        private List<string> GetTableNames()
        {
            var list = new List<string>();
            foreach (var table in _tables)
                list.Add(table.key);

            return list;
        }


        IEnumerator WaitData()
        {
            bool ready = false;
            bool dataError = false;

            while (!ready)
            {
                ready = true;

                foreach (var table in _tables)
                {
                    if (!table.dataAccepted) ready = false;
                    else if (table.error) dataError = true;
                }
                yield return null;
            }

            if (dataError) throw new System.Exception("Table data error!");

            foreach (var loadable in _loadables)
            {
#if UNITY_EDITOR
                EditorUtility.SetDirty(loadable);
#endif
                loadable.LoadData(tables);
            }

#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif

            onSuccess?.Invoke(true);
            InitCompleted();
        }

    }

}



