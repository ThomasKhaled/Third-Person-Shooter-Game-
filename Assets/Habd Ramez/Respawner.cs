using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void Despawn(GameObject go, float inSeconds)
    {
        go.SetActive(false);
        if(inSeconds < 1)
            return;
        GameManager.Instance.Timer.Add(() => {
            go.SetActive(true);
        
        }, inSeconds);
    }
}
