using System.Diagnostics;
using UnityEngine;

public class Job : ThreadedJob
{
    public Vector3[] InData;  // arbitary job data
    public Vector3[] OutData; // arbitary job data

    protected override void ThreadFunction()
    {
        // PathFinding.g
        // Do your threaded task. DON'T use the Unity API here
        void ZombieMovement(Vector3[] D, GameObject Zombie2)
        {
            if (D.Length > 0)
            {
                int targetIndex = 0;
                Vector3 currentWaypoint = D[0];
                while (true)
                {

                    if (Zombie2.transform.position == currentWaypoint)
                    {
                        targetIndex++;

                        if (targetIndex >= D.Length)
                        {
                            targetIndex = 0;
                            break;
                        }
                        currentWaypoint = D[targetIndex];
                    }
                    Zombie2.transform.position = Vector3.MoveTowards(Zombie2.transform.position, currentWaypoint, Time.deltaTime * 5);

                //    Zombie2.transform.rotation = Quaternion.Slerp(Zombie2.transform.rotation, Quaternion.LookRotation(Player.position - Zombie2.transform.position), Time.deltaTime * 2);
                }
            }
        }
        for (int i = 0; i < 100000000; i++)
        {
            OutData[i % OutData.Length] += InData[(i + 1) % InData.Length];
        }
    }
    protected override void OnFinished()
    {
        // This is executed by the Unity main thread when the job is finished
        for (int i = 0; i < InData.Length; i++)
        {
          //  Debug.Log("Results(" + i + "): " + InData[i]);
        }
    }
}