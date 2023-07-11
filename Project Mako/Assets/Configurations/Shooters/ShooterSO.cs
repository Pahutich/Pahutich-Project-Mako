using Mako.Shooting;
using UnityEngine;


public class ShooterSO : ScriptableObject
{
    [SerializeField] public float Cooldown { get; private set; }
    [SerializeField] public float OverheatThreshold { get; private set; }
    [SerializeField] public float OverheatPerShot { get; private set; }
    [SerializeField] public float CoolMultiplier { get; private set; }
    [SerializeField] public GameObject Projectile { get; private set; }
    [SerializeField] public WeaponType WeaponType { get; private set; }
}
