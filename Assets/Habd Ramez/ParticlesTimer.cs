using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesTimer : MonoBehaviour
{
    [SerializeField] float timeToLive;
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if(timeToLive<=0)
        {
            Destroy(gameObject);
        }
    }
}
