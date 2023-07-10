using UnityEngine;
using UnityEngine.UI;

public class ShieldsBar : MonoBehaviour
{
    [SerializeField] private Image shieldsBarImage;
    [SerializeField] private GameObject player;
    private Shields shields;
    private void Awake() {
        shields = player.GetComponent<Shields>();
    }
    private void Start() {
        shields.OnShieldCapacityChanged += () => shieldsBarImage.fillAmount = shields.GetPercent();
    }
}
