using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZombie : MonoBehaviour
{
    Animator animator;
    [SerializeField] float timeleft;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isDead", true);
        timeleft -= Time.deltaTime;
        if(timeleft <0)
        {
            Destroy(gameObject);
        }
    }
}
