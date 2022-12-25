using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour
{

    Queue<PathResult> results = new Queue<PathResult>();                         //O(1)
                                                                                 
    static PathRequestManager instance;                                          //O(1)
    PathFinding pathfinding;                                                     //O(1)
                                                                                 
    void Awake()                                                                 
    {                                                                          
        instance = this;                                                         //O(1)
        pathfinding = GetComponent<PathFinding>();                               //O(1)
    }                                                                            
                                                                                 
    void Update()                                                                
    {                                                                            
        if (results.Count > 0)                                                   //O(1)
        {                                                                        
            int itemsInQueue = results.Count;                                    //O(1)
            lock (results)                                                       //O(1)
            {                                                                    
                for (int i = 0; i < itemsInQueue; i++)                           //O(N)  N = Number of Zombies
                {                                                                
                    PathResult result = results.Dequeue();                       //O(1) 
                    result.callback(result.path, result.success);                //O(1)
                }
            }
        }
    }

    public static void RequestPath(PathRequest request)                                             //Total: O(ELog(V))
    {                                                                                               
        ThreadStart threadStart = delegate {                                                        //O(1)
            instance.pathfinding.GetShortestPath(request, instance.FinishedProcessingPath);         //O(ELog(V))
        };                                                                                          
        threadStart.Invoke();                                                                       //O(1)
    }                                                                                               
                                                                                                    
    public void FinishedProcessingPath(PathResult result)                                           //Total: O(N)
    {                                                                                               
        lock (results)                                                                              //O(1)
        {                                                                                           
            results.Enqueue(result);                                                                //O(N) 
            /*If Count is less than the capacity of the internal array,this method is an O(1) operation.
             *If the internal array needs to be reallocated to accommodate the new element,
             * this method becomes an O(N) operation, where N is Count.
             */
        }                                                                                           
    }                                                                                               
                                                                                                    
                                                                                                    
                                                                                                  
}                                                                                                  
                                                                                                    
public struct PathResult                                                                            //O(1)
{                                                                                                   
    public Vector3[] path;                                                                          //O(1)
    public bool success;                                                                            //O(1)
    public Action<Vector3[], bool> callback;                                                        //O(1)
                                                                                                    
    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)               //O(1)
    {                                                                                               
        this.path = path;                                                                           //O(1)
        this.success = success;                                                                     //O(1)
        this.callback = callback;                                                                   //O(1)
    }                                                                                               
                                                                                                    
}                                                                                                   
                                                                                                    
public struct PathRequest                                                                           //O(1)
{                                                                                                   
    public Vector3 pathStart;                                                                       //O(1)
    public Vector3 pathEnd;                                                                         //O(1)
    public Action<Vector3[], bool> callback;                                                        //O(1)
                                                                                                    
    public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)             //O(1)
    {                                                                                               
        pathStart = _start;                                                                         //O(1)
        pathEnd = _end;                                                                             //O(1)
        callback = _callback;                                                                       //O(1)
    }

}