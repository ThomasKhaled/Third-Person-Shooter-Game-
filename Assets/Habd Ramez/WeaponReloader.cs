using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;
    [SerializeField] Text ammoText;

    int ammo;
    public int shotsFiredInClip;
    bool isReloading;

    private void Awake()
    {
        ammoText.text = clipSize.ToString() + "/" + clipSize.ToString();
    }

    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public bool Reload()
    {
        if (isReloading)
            return false;

        if (shotsFiredInClip == 0)
            return false;

        isReloading = true;
        GameManager.Instance.Timer.Add(ExecuteReload, reloadTime);
        return true;
    }

    private void ExecuteReload()
    {
        isReloading = false;
        ammo -= shotsFiredInClip;
        shotsFiredInClip = 0;
        ammoText.text = clipSize.ToString() + "/" + clipSize.ToString();

        if (ammo < 0)
        {
            ammo = 0;
            shotsFiredInClip += -ammo;
        }
    }

    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
        int ammoleft = clipSize - shotsFiredInClip;
        ammoText.text = ammoleft.ToString() + "/" + clipSize.ToString();
    }

}
