using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destructable
{

    [SerializeField] float inSeconds;
    [SerializeField] ParticleSystem blood;
    [SerializeField] GameObject corpse;

    public override void Die()
    {
        Instantiate(corpse, transform.position, transform.rotation);
        base.Die();

        GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
    }

    void OnEnable()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        Instantiate(blood, transform.position, transform.rotation);
       // print("Remaining: " + HPRemaining);
    }
}
