using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class PathFinding : MonoBehaviour
{
    GridScript grid;
    public Transform seeker, target;




    void Awake()
    {
        grid = GetComponent<GridScript>();

    }

    private void Update()
    {
        //   if (Input.GetKey(KeyCode.Q))
        FindPath(seeker.position, target.position);
    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();


        Node startNode = grid.NodeFromPoint(startPos);
        Node targetNode = grid.NodeFromPoint(targetPos);

        Priority_queue<Node> openSet = new Priority_queue<Node>();

        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.AddElement(startNode);
        while (openSet.count > 0)
        {
            Node currentNode = openSet.pop_min();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                sw.Stop();
                print("Path founded after : " + sw.ElapsedMilliseconds);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.element.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.element.Contains(neighbour))
                    {
                        openSet.AddElement(neighbour);
                    }
                }
            }
        }
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        for (int i = 0; i < path.Count; i++)
        {
            target.transform.position = path[i].nodePosition * Time.deltaTime;
            //target.position = path[i].nodePosition;
        }

        grid.path = path;
    }

}
