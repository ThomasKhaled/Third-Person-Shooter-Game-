using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    const float minPathUpdateTime = .2f;                                                    //O(1)
    const float pathUpdateMoveThreshold = .5f;                                              //O(1)
    public Transform target;                                                                //O(1)
    public float speed = 20;                                                                //O(1)
    public float turnSpeed = 3;                                                             //O(1)
    public float turnDst = 5;                                                               //O(1)
    public float stoppingDst = 10;                                                          //O(1)
    public Vector3[] path;          
    
    //Path path;                                                                              //tanesh path
                                                                                            
    void Start()                                                                            
    {                                                                                       
        StartCoroutine(UpdatePath());                                                       //O(1)
                                                                                            
    }                                                                                       
                                                                                            
    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)                       //Total: O(1)
    {                                                                                       
        if (pathSuccessful)                                                                 //O(1)
        {
            //path = new Path(waypoints, transform.position, turnDst, stoppingDst);           
            path = waypoints;                                                               //O(1)             
            StopCoroutine("FollowPath");                                                    //O(1)
            StartCoroutine("FollowPath");                                                   //O(1)
        }
    }
    IEnumerator UpdatePath()                                                                      //Total: O(ELog(V))
    {
        if (Time.timeSinceLevelLoad < .3f)                                                                  //O(1)
        {                                                                                                   
            yield return new WaitForSeconds(.3f);                                                           //O(1)
        }                                                                                                   
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));  //O(ELog(V))
                                                                                                            
        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;                         //O(1)
        Vector3 targetPosOld = target.position;                                                             //O(1)
                                                                                                            
        while (true)                                                                                        //O(ELog(V))
        {                                                                                                   
            yield return new WaitForSeconds(minPathUpdateTime);                                             //O(1)
        //    print(((target.position - targetPosOld).sqrMagnitude) + "    " + sqrMoveThreshold);          
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)                           //O(1)
            {                                                                                               
                PathRequestManager.RequestPath(new PathRequest(                                             
                transform.position, target.position, OnPathFound));                                         //O(ELog(V))
                targetPosOld = target.position;                                                             //O(1)
            }
        }
    }

    //IEnumerator FollowPath()                                                                        //O(1)
    //{                                                                                               

    //    bool followingPath = true;                                                                  //O(1)
    //    int pathIndex = 0;                                                                          //O(1)
    //   // transform.LookAt(path.lookPoints[0]);                                                       //O(1)

    //    float speedPercent = 1;                                                                     //O(1)

    //    while (followingPath)                                                                       //O(1)
    //    {                                                                                           
    //        Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);                //O(1)
    //        while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))                            //O(1)
    //        {                                                                                       
    //            if (pathIndex == path.finishLineIndex)                                              //O(1)
    //            {                                                                                   
    //                followingPath = false;                                                          //O(1)
    //                break;                                                                          //O(1)
    //            }                                                                                   
    //            else                                                                                
    //            {                                                                                   
    //                pathIndex++;                                                                    //O(1)
    //            }
    //        }

    //        if (followingPath)
    //        {

    //            if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
    //            {
    //                speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D)/ stoppingDst);
    //                if (speedPercent < 0.01f)
    //                {
    //                    followingPath = false;
    //                }
    //            }

    //            Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
    //            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    //            transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
    //        }

    //        yield return null;

    //    }
    //}


    IEnumerator  FollowPath()                                                                          //Total: O(Path)   
    {                                                                                                  
                                                                                                       
        if (path.Length > 0)                                                                           //O(1)
        {                                                                                              
            int targetIndex = 0;                                                                       //O(1)
            Vector3 currentWaypoint = path[0];                                                         //O(1)
            while (true)                                                                               //O(Path)
            {                                                                                          
                                                                                                       
                if (transform.position == currentWaypoint)                                             //O(1)
                {                                                                                      
                    targetIndex++;                                                                     //O(1)
                                                                                                       
                    if (targetIndex >= path.Length)                                                    //O(1)
                    {                                                                                  
                        targetIndex = 0;                                                               //O(1)
                        break;                                                                         //O(1)
                    }                                                                                  
                    currentWaypoint = path[targetIndex];                                               //O(1)
                }                                                                                      
                transform.position =                                                                   
                Vector3.MoveTowards(transform.position, currentWaypoint, Time.deltaTime * 5);          //O(1)
                                                                                                       
                transform.rotation =                                                                   
                Quaternion.Slerp(transform.rotation,                                                   
                Quaternion.LookRotation(target.position - transform.position),Time.deltaTime * 2);     //O(1)
                yield return null;                                                                     //O(1)
            }
        }
    }


    //public void OnDrawGizmos()
    //{
    //    if (path != null)
    //    {
    //        path.DrawWithGizmos();
    //    }
    //}
}