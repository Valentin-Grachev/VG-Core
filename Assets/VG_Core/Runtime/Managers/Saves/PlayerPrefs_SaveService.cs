using UnityEngine;
using NaughtyAttributes;
using System;

namespace VG
{
    public class PlayerPrefs_SaveService : SaveService
    {
        [SerializeField] private bool _onlyEditor;


        public override bool supported
        {
            get
            {
                if (_onlyEditor) return Environment.editor;
                else return true;
            }
        }

        public override void Commit(string data, Action<bool> onCommited)
        {
            PlayerPrefs.SetString("data", data);
            PlayerPrefs.Save();
            onCommited?.Invoke(true);
        }

        public override string GetData() => PlayerPrefs.GetString("data", string.Empty);

        public override void Initialize() => InitCompleted();

        [Button("Clear data")]
        private void ClearData() => PlayerPrefs.DeleteAll();

    }

}

