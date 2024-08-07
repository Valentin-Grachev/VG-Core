using System;
using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    public class Test_LeaderboardService : LeaderboardService
    {
        [SerializeField] private bool _useInBuild = false;
        [Space(10)]
        [SerializeField] private bool _playerRegistered = true;
        [SerializeField] private LeaderboardEntry _playerEntry;
        [SerializeField] private List<LeaderboardEntry> _entries;

        private const string playerPrefsScoreKey = "lb_score";

        public override bool availableForThisPlayer => _playerRegistered;

        public override bool supported => Environment.editor || _useInBuild;

        public override void GetEntries(string leaderboardKey, Action<List<LeaderboardEntry>> onReceived) 
            => onReceived?.Invoke(_entries);

        public override void GetPlayerEntry(string leaderboardKey, Action<LeaderboardEntry> onReceived)
        {
            var entry = _playerEntry;
            entry.score = PlayerPrefs.GetInt(playerPrefsScoreKey, 0);
            onReceived?.Invoke(entry);
        }

        public override void Initialize() => InitCompleted();

        public override void SetScore(string leaderboardKey, int score)
        {
            PlayerPrefs.SetInt(playerPrefsScoreKey, score);
        }
    }

}



