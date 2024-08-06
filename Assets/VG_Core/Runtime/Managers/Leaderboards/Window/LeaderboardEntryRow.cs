using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace VG
{
    public class LeaderboardEntryRow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rank;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Image _avatar;



        public void SetValue(LeaderboardEntry entry)
        {
            _rank.text = entry.rank.ToString();
            _playerName.text = entry.playerName;
            _score.text = entry.score.ToString();
            _avatar.sprite = entry.avatar;
        }


    }

}



