using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssualtRifle : ShooterScript
{
    public override void Fire()
    {
        base.Fire();

        if(canFire)
        {

        }
    }

    public void Update()
    {
        if(GameManager.Instance.InputController.Reload)
        {
            Reload();
        }
    }
}
