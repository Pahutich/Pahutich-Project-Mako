using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatBar : MonoBehaviour
{
    [SerializeField] private Shooter shooter;
    [SerializeField] private Image overheatBarImage;
    [SerializeField] private RawImage iconColor;
    // Start is called before the first frame update
    void Start()
    {
        shooter.OnOverheatChanged += () => overheatBarImage.fillAmount = shooter.GetOverheatPercent();
        shooter.OnOverheatChanged += () => iconColor.color = shooter.GetOverhearStatus() ? iconColor.color = Color.red : iconColor.color = Color.white;
    }
}
