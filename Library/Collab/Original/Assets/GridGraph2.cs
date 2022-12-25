using System.Collections.Generic;
using UnityEngine;
public class GridGraph2 : MonoBehaviour
{
    public LayerMask Walls;            //O(1)
    public Vector2 GraphSize;          //O(1)
    public float nodeRadius;           //O(1)
    int GraphWidth, GraphHeight;       //O(1) 
    public int NoOfNodes;              //O(1)     //Calc The maximum number of nodes in graph
    Node[,] Graph;                     //O(1)     //GridGraph
    public List<Node> path;            //O(1)
    public Transform player;           //O(1)
    /// <summary>
    /// To calc Number of nodes in X-Axis
    /// To calc Number of nodes in Y-Axis
    /// </summary>
    private void Awake()
    {
        // GraphWidth = GraphSize.x / Diameter
        GraphWidth = Mathf.RoundToInt(GraphSize.x / (nodeRadius * 2));      //O(1)
        GraphHeight = Mathf.RoundToInt(GraphSize.y / (nodeRadius * 2));     //O(1)
        CreateGrid();                       //O(V) => V : GraphHeight * GraphWidth
    }
    
    void OnDrawGizmos() //Total: O(V) => V : GraphHeight * GraphWidth
    {
        Gizmos.color = Color.blue;                                                              //O(1)
        Gizmos.DrawWireCube(transform.position, new Vector3(GraphSize.x, 1, GraphSize.y));      //O(1)

        if (Graph != null)                                                                      //O(1)
        {                                                                                       
            Node playerNode = NodeFromGraph(player.position);                                   //O(1)
            foreach (Node n in Graph)                                                           //O(V)
            {                                                                                   
                Gizmos.color = (n.walkable) ? Color.white : Color.red;                          //O(1)
                                                                                                
                if (path != null)                                                               //O(1)
                {                                                                               
                    if (path.Contains(n))                                                       //O(1)
                    {                                                                           
                        Gizmos.color = Color.black;                                             //O(1)
                    }                                                                           
                }                                                                               
                if (playerNode == n)                                                            //O(1)
                {                                                                               
                    Gizmos.color = Color.cyan;                                                  //O(1)
                }                                                                               
                Gizmos.DrawCube(n.nodePosition, Vector3.one * (2* nodeRadius - .1f));           //O(1)
            }
        }
    } 
    void CreateGrid() //Total: O(V) => V : GraphHeight * GraphWidth
    {
        //Make an instance of Graph with its size (GraphWidth & GraphHeight)  
        Graph = new Node[GraphWidth, GraphHeight];                            //O(1)
        //All Number of Nodes in Graph   
        NoOfNodes = (GraphWidth * GraphHeight);                               //O(1) 
        //Get The buttomLeft Node in our Graph
        //                      (0,0,0)                 (1,0,0) *        5                  (0,0,1) *       5        
        Vector3 bottomLeft = transform.position - Vector3.right * GraphSize.x / 2 - Vector3.forward * GraphSize.y / 2;                                //O(1)

        for (int x = 0; x < GraphWidth; x++)                                  //O(GraphWidth)
        {
            for (int y = 0; y < GraphHeight; y++)                             //O(GraphHeight)
            {
                //for example x = 0 & y = 1  , width = 4 , height = 4
                //              (-2,0,-2)          (1,0,0)  *  0.5                                     (0,0,1) * 1.5
                //            (-1.5,0,-0.5)            (0.5,0,0)                                          (0,0,1.5)
                Vector3 point = bottomLeft + 
                    Vector3.right * (x * (nodeRadius * 2) + nodeRadius) +
                    Vector3.forward * (y * (nodeRadius * 2) + nodeRadius);        //O(1)

                bool walkable = !(Physics.CheckSphere(point, nodeRadius, Walls)); //O(1)
                Graph[x, y] = new Node(walkable, point, x, y);                    //O(1)
            }
        }
    }
    /// <summary>
    /// Gets Node from Graph Array
    /// </summary>
    /// <param name="CurPostion"> Current Postion from world </param>
    /// <returns>returns node </returns>
    public Node NodeFromGraph(Vector3 CurPostion)                        //Total: O(1)
    {
        float percentX = (CurPostion.x + GraphSize.x / 2) / GraphSize.x; //O(1)
        float percentY = (CurPostion.z + GraphSize.y / 2) / GraphSize.y; //O(1)

        //if value < 0 returns 0 , if value > 1 returns 1
        percentX = Mathf.Clamp01(percentX);                              //O(1)
        percentY = Mathf.Clamp01(percentY);                              //O(1)

        int x = Mathf.RoundToInt((GraphWidth - 1) * percentX);           //O(1)
        int y = Mathf.RoundToInt((GraphHeight - 1) * percentY);          //O(1)

        return Graph[x, y];                                              //O(1)
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"> Node to Get its neighbours </param>
    /// <returns> Return a list of Nodes(neighbours) </returns>
    /// 
    int[] x = {-1,-1,-1,0,0,1,1,1 };                                       //O(1)
    int[] y = {-1,0,1,-1,1,-1,0,1 };                                       //O(1)
    public List<Node> GetNeighbours(Node node)                             //Total = O(1)
    {                                                                     
        List<Node> neighbours = new List<Node>();                          //O(1)
        for (int i = 0; i < 8; i++)                                       
        {                                                                 
            int dx = x[i] + node.gridX;                                    //O(1)
            int dy = y[i] + node.gridY;                                    //O(1)

            if (dx >= 0 && dx < GraphWidth && dy >= 0 && dy < GraphHeight) //O(1)
            {
                neighbours.Add(Graph[dx, dy]);                             //O(1)
            }                                                            
        }                                                                
        return neighbours;                                                 //O(1)
    }                                                                    
}
