using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Image healthBarImage;
    [SerializeField] private GameObject player;
    private void Start() {
        healthBarImage = transform.Find("Foreground").GetComponent<Image>();
        healthSystem = player.GetComponent<Health>().GetHealthSystem();
        healthSystem.OnHealthChanged += () => healthBarImage.fillAmount = healthSystem.GetPercent();
    }
}