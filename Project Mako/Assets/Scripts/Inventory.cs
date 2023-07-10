using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private void Awake()
    {
        //Singleton method
        if (instance == null)
        {
            //First run, set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    [field: SerializeField] public GameObject[] PrimaryWeapons { get; private set; }
    [field: SerializeField] public GameObject[] SecondaryWeapons { get; private set; }
    public int primaryWeaponIndex = 0;
    public int secondaryWeaponIndex = 0;
    public GameObject GetPrimaryWeapon()
    {
        return PrimaryWeapons[primaryWeaponIndex];
    }
    public GameObject GetSecondaryWeapon()
    {
        return SecondaryWeapons[secondaryWeaponIndex];
    }
}
