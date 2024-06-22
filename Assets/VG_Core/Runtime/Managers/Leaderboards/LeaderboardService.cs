using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VG.Internal;

namespace VG
{
    public abstract class LeaderboardService : Service
    {

        public abstract bool availableForThisPlayer { get; }

        public abstract void SetScore(string leaderboardKey, int score);

        public abstract void GetEntries
            (string leaderboardKey, Action<List<LeaderboardEntry>> onReceived);

        public abstract void GetPlayerEntry
            (string leaderboardKey, Action<LeaderboardEntry> onReceived);


    }
}


