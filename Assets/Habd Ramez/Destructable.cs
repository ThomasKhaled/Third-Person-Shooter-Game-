using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] float HP;

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    float damageTaken;

    public float HPRemaining
    {
        get
        {
           return HP - damageTaken;
        }
    }

    public bool IsAlive
    {
        get
        {
            return HPRemaining > 0;
        }
    }

    public virtual void Die()
    {
        if (!IsAlive)
            return;
        if (OnDeath != null)
            OnDeath();
    }

    public virtual void TakeDamage(float amount)
    {
        damageTaken += amount;
        if(OnDamageReceived != null)
        {
            OnDamageReceived();
        }
        if(HPRemaining <= 0)
        {
            Die();
        }
    }

    public void Reset()
    {
        damageTaken = 0;        
    }

    

}
