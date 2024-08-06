using UnityEngine;


namespace VG
{
    public class LeaderboardWindow : MonoBehaviour
    {
        [SerializeField] private LeaderboardEntryRow _entryRowPrefab;
        [SerializeField] private LeaderboardEntryRow _playerEntryRow;
        [SerializeField] private Transform _content;


        private void OnEnable() => UpdateValues();


        private void UpdateValues()
        {
            foreach (Transform child in _content)
                Destroy(child.gameObject);

            Leaderboards.GetPlayerEntry(onReceived: entry =>
                _playerEntryRow.SetValue(entry));
                
            Leaderboards.GetEntries(onReceived: entries =>
            {
                foreach (var entry in entries)
                    Instantiate(_entryRowPrefab, _content).SetValue(entry);
            });
        }


    }
}



