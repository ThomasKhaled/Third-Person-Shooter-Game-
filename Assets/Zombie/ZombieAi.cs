using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class ZombieAi : MonoBehaviour
{
    
    public int timeLeft = 5; //Seconds Overall
    private Animator animator;
    public static bool startRunning;
    bool entercollision;

    // Start is called before the first frame update
    void Start()
    {
        startRunning = false;
        animator = GetComponentInChildren<Animator>();
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        animator.SetBool("goRun", false);
       
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            entercollision = true;
            animator.SetBool("ReachedTarget", true);
        }
        else
        {
            entercollision = false;
        }
        //Debug.Log("Entered condition");

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            entercollision = true;
            animator.SetBool("ReachedTarget", true);
        }
        else
        {
            entercollision = false;
        }
        //Debug.Log("Entered condition");
    }




    // Update is called once per frame
    void Update()
    {
        if (timeLeft == 0)
        {
            animator.SetBool("goRun", true);
            startRunning = true;
        }
        if (!entercollision)
        {
            animator.SetBool("ReachedTarget", false);
        }


    }

    

    IEnumerator LoseTime()
    {
        while (timeLeft>0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
