using System;
using System.Collections.Generic;
using VG.Internal;


namespace VG
{
    public class Leaderboards : Manager
    {
        private static Leaderboards instance;
        protected override string managerName => "VG Leaderboards";

        private static LeaderboardService service 
            => instance.supportedService as LeaderboardService;

        public static bool availableForThisPlayer => service.availableForThisPlayer;


        protected override void OnInitialized()
        {
            instance = this;
            Log(Core.Message.Initialized(managerName));
        }

        public static void SetScore(int score, string leaderboardKey = Key_Leaderboard.main)
        {
            service.SetScore(leaderboardKey, score);
            instance.Log($"Set {score} to leaderboard {leaderboardKey}");
        }

        public static void GetEntries(Action<List<LeaderboardEntry>> onReceived, 
            string leaderboardKey = Key_Leaderboard.main)
        {
            instance.Log("Get entries");
            onReceived += (entries) => instance.Log("On entries received");

            service.GetEntries(leaderboardKey, onReceived);
        }

        public static void GetPlayerEntry(Action<LeaderboardEntry> onReceived,
            string leaderboardKey = Key_Leaderboard.main)
        {
            instance.Log("Get player entry");
            onReceived += (entry) => instance.Log("On player entry received");

            service.GetPlayerEntry(leaderboardKey, onReceived);
        }



    }
}



