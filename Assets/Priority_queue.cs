using UnityEngine;
using System.Collections;
using System;

public class Priority_queue<T> where T : HeapElement<T>
{

    T[] Elements;                                        //O(1)
    int Cnt;                                             //O(1)

    public Priority_queue(int maxHeapSize)               //Total: O(1)
    {
        Elements = new T[maxHeapSize];                   //O(1)
    }

    public void Add(T item)                              //Total: O(Log(V))
    {                                                    
        item.HeapIdx = Cnt;                              //O(1)
        Elements[Cnt] = item;                            //O(1)
        HeapifyUp(item);                                 //O(Log(V))
        Cnt++;                                           //O(1)
    }

    public T GetMin()                                 //Total: O(Log(V))
    {
        T firstItem = Elements[0];                    //O(1)
        Cnt--;                                        //O(1)
        Elements[0] = Elements[Cnt];                  //O(1)
        Elements[0].HeapIdx = 0;                      //O(1)
        HeapifyDown(Elements[0]);                     //O(Log(V))
        return firstItem;                             //O(1)
    }
    /// <summary>
    /// O(Log(V))
    /// </summary>
    /// <param name="item"></param>
    public void UpdateItem(T item)                     //Total: O(Log(V))
    {
        HeapifyUp(item);                               //O(Log(V))
    }
    /// <summary>
    /// O(1)
    /// </summary>
    public int Count                                   //Total: O(1)
    {
        get
        {
            return Cnt;                                //O(1)
        }
    }
    /// <summary>
    /// O(1)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(T item)                       //Total: O(1)
    {   // Accessing array in O(1) + comparing with item in O(1)
        return Equals(Elements[item.HeapIdx], item);      //O(1)
    }

    void HeapifyDown(T item){                           //Total: O(Log(V))
        // Number of Levels in Heap is Log(V)
        while (true){                                   //O(Log(V)) * O(1) = O(Log(V))
            int LeftNodeIndex = item.HeapIdx * 2 + 1; //O(1)
            int RightNodeIndex = item.HeapIdx * 2 + 2;//O(1)
            int Smallest = 0;                         //O(1)
            if (LeftNodeIndex < Cnt)                  //O(1)
            {
                Smallest = LeftNodeIndex;             //O(1)
                if (RightNodeIndex < Cnt)             //O(1)
                {
                    // if left Node > right Node
                    if (Elements[LeftNodeIndex].CompareTo(Elements[RightNodeIndex]) < 0)  //O(1)
                    {
                        Smallest = RightNodeIndex;                                  //O(1)
                    }
                }
                // if item > smallest 
                if (item.CompareTo(Elements[Smallest]) < 0)                            //O(1)
                {                                                               
                    Swap(item, Elements[Smallest]);                                    //O(1)
                }
                else return;                                                         //O(1)
            }
            else return;                                                             //O(1)
        }
    }

    void HeapifyUp(T item)                                       //Total: O(Log(V))
    {
        int parentIndex = (item.HeapIdx - 1) / 2;                //O(1)

        // Number of Levels in Heap is Log(V)
        while (true)                                             //O(Log(V)) * O(1) = O(Log(V))
        {                                                        
            T parentItem = Elements[parentIndex];                   //O(1)
            if (item.CompareTo(parentItem) > 0)                  //O(1)
            {                                                    
                Swap(item, parentItem);                          //O(1)
            }                                                    
            else                                                 
            {                                                    
                break;                                           //O(1)
            }                                                    
                                                                
            parentIndex = (item.HeapIdx - 1) / 2;                //O(1)
        }
    }

    void Swap(T firstNode, T secondNode)                         //Total: O(1)
    {                                                            
        Elements[firstNode.HeapIdx] = secondNode;                   //O(1)
        Elements[secondNode.HeapIdx] = firstNode;                   //O(1)
        int firstNodeIndex = firstNode.HeapIdx;                  //O(1)
        firstNode.HeapIdx = secondNode.HeapIdx;                  //O(1)
        secondNode.HeapIdx = firstNodeIndex;                     //O(1)
    }
}

public interface HeapElement<T> : IComparable<T>                   //Total: O(1)
{
    int HeapIdx                                                 
    {
        get;                                                     //O(1)
        set;                                                     //O(1)
    }
}