using UnityEngine;
public class Node : IHeapItem<Node>
{
    public bool walkable;                                                 //O(1)
    public Vector3 nodePosition;                                          //O(1)
    public int gCost;                                                     //O(1)
    public int hCost;                                                     //O(1)
    public int gridX;                                                     //O(1)
    public int gridY;                                                     //O(1)
    public Node ParentNode;                                               //O(1)
    int HeapIndex;                                                          //O(1)
    public Node(bool walkable, Vector3 nodePostion, int gridX, int gridY) //Total = O(1)
    {                                  
        this.walkable = walkable;                                         //O(1)
        this.nodePosition = nodePostion;                                  //O(1)
        this.gridX = gridX;                                               //O(1)
        this.gridY = gridY;                                               //O(1)
    }               
    public int fCost                                                 //Total = O(1)
    {                
        get                    
        {                               
            return gCost + hCost;                                         //O(1)
        }                                              
    }                                                               
    public int HeapIdx                                            //Total = O(1)
    {                                                 
        get            
        {                                                         
            return HeapIndex;                                               //O(1)
        }                                 
        set                                           
        {
            HeapIndex = value;                                              //O(1)
        }                      
    }                                                        
    public int CompareTo(Node other)                                  //Total = O(1)
    {                                                                    
        // current fcost < other.cost --> returns -1   
        int cmp = fCost.CompareTo(other.fCost);                           //O(1)
        if (cmp == 0)                                                     //O(1)
        {                                                               
            cmp = hCost.CompareTo(other.hCost);                           //O(1)
        }                                                            
        return -cmp;                                                      //O(1)
    }                                                                    
}                                                                       
                                                                          