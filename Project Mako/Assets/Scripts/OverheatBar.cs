using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatBar : MonoBehaviour
{
    [SerializeField] private Shooter shooter;
    [SerializeField] private Image overheatBarImage;
    // Start is called before the first frame update
    void Start()
    {
        shooter.OnOverheatChanged += () => overheatBarImage.fillAmount = shooter.GetOverheatPercent();
    }
}
