using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHPBar : MonoBehaviour
{
    private Player player;
    [SerializeField]Slider playerHpSlider;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        player.OnPlayerHealthChanged += UpdateHealth;
    }

    private void UpdateHealth()
    {
        playerHpSlider.value = ((float)player.CurrentHP / player.MaxHP);
    }
}
