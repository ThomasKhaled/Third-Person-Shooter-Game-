using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PathFinding;

public class ListOfZombies : MonoBehaviour
{
    public static GridGraph2 Graph;                        //O(1)
    public static List<GameObject> zombieList;                        //O(1)
    [SerializeField] private GameObject Zombie;                       //O(1)
    public int NumberOfZombies;
    public Transform Player;
    Vector3[] path;
    [SerializeField] Text text;
    [SerializeField] GameObject winScreen;

    void Start()
    {
        Graph = GetComponent<GridGraph2>();                           //O(1)
        zombieList = new List<GameObject>();                              //O(1)

        for (int i = 0; i < NumberOfZombies; i++)                                   //O(1) where 1 = Number of zombies 
        {
            int x = UnityEngine.Random.Range(-50, 50);                //O(1)
            int z = UnityEngine.Random.Range(-50, 50);                //O(1)
            if (Graph.NodeFromGraph(new Vector3(x, 0, z)).walkable)
            {
                GameObject newzombie = Instantiate(Zombie, new Vector3(x, 0, z), Quaternion.identity); //O(1)

                zombieList.Add(newzombie); //O(1)
            }
            else
                i--;   //O(1)
        }
        text.text = NumberOfZombies.ToString();
    }


    void Update()
    {
        int i = 0;

        while (i < zombieList.Count)
        {
            if (zombieList[i].active == false)
            {
                DestroyObject(zombieList[i]);
               // zombieList[i].destroyo
                zombieList.RemoveAt(i);
                i++;
            }
            i++;
        }

        text.text = zombieList.Count.ToString();
        if(zombieList.Count == 0)
        {
            winScreen.SetActive(true);
        }

    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
           // Path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator FollowPath(Vector3[] Path, GameObject Zombie2)
    {

        if (Path.Length > 0)
        {
            int targetIndex = 0;
            Vector3 currentWaypoint = Path[0];
            while (true)
            {

                if (Zombie2.transform.position == currentWaypoint)
                {
                    targetIndex++;

                    if (targetIndex >= Path.Length)
                    {
                        targetIndex = 0;
                        yield break;
                    }
                    currentWaypoint = Path[targetIndex];
                }
                Zombie2.transform.position = Vector3.MoveTowards(Zombie2.transform.position, currentWaypoint, Time.deltaTime * 5);

                Zombie2.transform.rotation = Quaternion.Slerp(Zombie2.transform.rotation, Quaternion.LookRotation(Player.position - Zombie2.transform.position), Time.deltaTime * 2);
                yield return null;
            }
        }
    }
}
