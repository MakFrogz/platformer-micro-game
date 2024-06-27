using TMPro;
using UnityEngine;

namespace UI.PlayerStats
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _tokensText;
        [SerializeField] private TMP_Text _distanceText;

        public void UpdateHealth(int health)
        {
            _healthText.text = $"Health:{health}";
        }

        public void UpdateTokens(int coins)
        {
            _tokensText.text = $"Tokens:{coins}";
        }

        public void UpdateDistance(int distance)
        {
            _distanceText.text = $"Distance:{distance}";
        }
    }
}