using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatBar : MonoBehaviour
{
    [HideInInspector] public Shooter overheatableWeapon;
    [SerializeField] private Image overheatBarImage;
    [SerializeField] private RawImage iconColor;
    // Start is called before the first frame update
    void Start()
    {
        overheatableWeapon.OnOverheatChanged += () => overheatBarImage.fillAmount = overheatableWeapon.GetOverheatPercent();
        overheatableWeapon.OnOverheatChanged += () => iconColor.color = overheatableWeapon.GetOverhearStatus() ? iconColor.color = Color.red : iconColor.color = Color.white;
    }
}
