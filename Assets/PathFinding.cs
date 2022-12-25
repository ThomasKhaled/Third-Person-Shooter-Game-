using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Collections;
using Unity.Jobs;
using System.Threading;
using System.Collections;
using System;

public class PathFinding : MonoBehaviour
{
    public static GridGraph2  Graph;                                  //O(1)
    //public  Transform Player;                                          //O(1)
    //public float speed;                                               //O(1)
    //public float RotationSpeedOfZombie;                               //O(1)
   // static Vector3[] Path;                                            //O(1)
   // [SerializeField] private GameObject Zombie;                       //O(1)
    //Thread th;
    PathRequestManager requestManager;
    void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        Graph = GetComponent<GridGraph2>();
    }

    private void Start() // Instantiate Zombies
    {
        //Graph = GetComponent<GridGraph2>();
        /*
        myJob = new Job();
        myJob.InData = GetShortestPath(ListOfZombies.zombieList[0].transform.position, Player.position);
        myJob.Start(); // Don't touch any data in the job class after you called Start until IsDone is true.
   
            */
    }
    /*
     void ZombieMovement(Vector3[] D , GameObject Zombie2)
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

                Zombie2.transform.rotation = Quaternion.Slerp(Zombie2.transform.rotation, Quaternion.LookRotation(Player.position - Zombie2.transform.position), Time.deltaTime * 2);
            }
        }
    }
    private void Update()
    {
        if (myJob != null)
        {
            if (myJob.Update())
            {
                // Alternative to the OnFinished callback
                myJob = null;
            }
        }
    }
    */
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Zombie">Starting postion </param>
    /// <param name="Player">Ending postion  </param>
    /// <returns>Array of Vector3 </returns>
    public void GetShortestPath(PathRequest request, Action<PathResult> callback)                 //Total: O(ELog(V))
    {
       // print("GetShortestPath");
        Vector3[] Path = new Vector3[0];
        Node ZombieNode = Graph.NodeFromGraph(request.pathStart);                                   //O(1)
        Node PlayerNode = Graph.NodeFromGraph(request.pathEnd);                                     //O(1)
        if (euclideandistance(ZombieNode, PlayerNode) <= 200)
        {
            bool pathfound = false;                                                            //O(1)

            Priority_queue<Node> openSet = new Priority_queue<Node>(Graph.NoOfNodes);      //O(1)
            //to prevent repetition of Nodes
            HashSet<Node> closedSet = new HashSet<Node>();                                 //O(1) 

            ZombieNode.ParentNode = ZombieNode;
            openSet.Add(ZombieNode);                                                       //O(Log(V))
            while (openSet.Count > 0)                                                      //O(E)
            {
                Node currentNode = openSet.GetMin();                                       //O(Log(V))
                closedSet.Add(currentNode);                                                //O(1)
                if (currentNode == PlayerNode) //pathfound                                 //O(1)
                {
                    pathfound = true;                                                      //O(1)
                    break;                                                                 //O(1)
                }
                foreach (Node neighbour in Graph.GetNeighbours(currentNode))//O(neighbours) : neighbours = 8 so its O(1)
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))              //O(1)
                    {
                        continue;                                                          //O(1)
                    }
                    //                  gcost        + calculate euclidean distance between current node and its neighbour
                    int newGcost = currentNode.gCost + euclideandistance(currentNode, neighbour);    //O(1)

                    if (newGcost < neighbour.gCost || !openSet.Contains(neighbour))                  //O(1)
                    {
                        neighbour.gCost = newGcost;                                                  //O(1)
                        neighbour.hCost = euclideandistance(neighbour, PlayerNode);                 //O(1)
                        neighbour.ParentNode = currentNode;                                         //O(1)

                        if (!openSet.Contains(neighbour))                                           //O(1)
                        {
                            openSet.Add(neighbour);                                                 //O(Log(V))
                        }
                        else openSet.UpdateItem(neighbour);                                         //O(Log(V))
                    }
                }
            }
            //   }
            if (pathfound)                                                                              //O(1)
            {
                Path = GetPath(ZombieNode, PlayerNode);                                                 //O(1)
                pathfound = Path.Length > 0;
                //sw.Stop();                                                                             
                Array.Reverse(Path);

            }
            callback(new PathResult(Path, pathfound, request.callback));
        }
    }
    static int euclideandistance(Node nodeA, Node nodeB)                                                      //Total: O(1)
    {                                                                                                               
        int Dx = Mathf.Abs(nodeA.gridX - nodeB.gridX);                                                        //O(1)
        int Dy = Mathf.Abs(nodeA.gridY - nodeB.gridY);                                                        //O(1)
        if (Dx>Dy)                                                                                            //O(1)
        {                                                                                                           
            return 14 * Dy + 10 * (Dx - Dy);                                                                  //O(1)
        }                                                                                                           
        return 14 * Dx + 10 * (Dy - Dx);                                                                      //O(1)
    }

    static Vector3[] PathDirections(List<Node> path)                                                            //Total: O(V)
    {                                                                                                          
        List<Vector3> Directions = new List<Vector3>();                                                         //O(1)
        Vector2 OldDirection = Vector2.zero;                                                                    //O(1)
                                                                                                               
        Graph.path = path;                                                                                      //O(1)
        for (int i = 1; i < path.Count; i++)                                                                    //O(V)
        {
            Vector2 NewDirection = new Vector2(path[i - 1].gridX - path[i].gridX  ,  path[i - 1].gridY - path[i].gridY); //O(1)
            if (NewDirection != OldDirection)
            {
                Directions.Add(path[i].nodePosition);                                                           //O(1)
            }
            OldDirection = NewDirection;                                                                        //O(1)
        }
        return Directions.ToArray();                                                                            //O(V)
    }
    static Vector3[] GetPath(Node startNode, Node endNode)                                     //O(V) == O(GraphHeight * GraphWidth)
    {                                                                                              
        List<Node> path = new List<Node>();                                                    //O(1)
        Node currentNode = endNode;                                                            //O(1)
                                                                                               
        while (currentNode != startNode)                                                       //O(V)
        {                                                                                      
            path.Add(currentNode);                                                             //O(1)
            currentNode = currentNode.ParentNode;                                              //O(1)
        }                                                                                      
        Vector3[] Directions = PathDirections(path);                                           //O(1)
        return Directions;                                                                     //O(1)

    }

}
/*
struct Job : IJobParallelForTransform
{
    [ReadOnly] public float deltaTime;
    public Vector3 seeeker;
    public void Execute(int index, TransformAccess Zombie)
    {
        if (ZombieAi.startRunning)
        {
            Vector3[] D = PathFinding.GetShortestPath(seeeker, Zombie.position);

            if (D.Length > 0)
            {
                int targetIndex = 0;
                Vector3 currentWaypoint = D[0];
                // while (true)
                //    {

                if (Zombie.position == currentWaypoint)
                {
                    targetIndex++;

                    if (targetIndex >= D.Length)
                    {
                        targetIndex = 0;
                        //break;
                    }
                    currentWaypoint = D[targetIndex];
                }
                Zombie.position = Vector3.MoveTowards(Zombie.position, currentWaypoint, deltaTime * 5);

                Zombie.rotation = Quaternion.Slerp(Zombie.rotation, Quaternion.LookRotation(seeeker - Zombie.position), deltaTime * 2);
                //  }
            }
        }

    }
}
*/

