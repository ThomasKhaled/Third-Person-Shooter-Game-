using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public event System.Action<PlayerScript> OnLocalPlayerJoined;
    private GameObject gameObject;

    // Making a Singleton of GameManager
    private static GameManager instance;
    public static GameManager Instance
    {
        get {
            if (instance == null)
            {
                instance = new GameManager();
                instance.gameObject = new GameObject("_gameManager");
                instance.gameObject.AddComponent<InputController>();
                instance.gameObject.AddComponent<Timer>();
                instance.gameObject.AddComponent<Respawner>();
            }
            return instance;
        }
    }

    // We Control Inputs Here
    private InputController ic;
    public InputController InputController
    {
        get
        {
            if(ic == null)
            {
                ic = gameObject.GetComponent<InputController>();
            }
            return ic;
        }
    }

    private Timer timer;
    public Timer Timer
    {
        get
        {
            if (timer == null)
                timer = gameObject.GetComponent<Timer>();
            return timer;
        }
    }

    private Respawner respawner;
    public Respawner Respawner
    {
        get
        {
            if(respawner==null)
            {
                respawner = gameObject.GetComponent<Respawner>();
            }
            return respawner;
        }
    }

    private PlayerScript localPlayer;
    public PlayerScript LocalPlayer
    {
        get
        {
            return localPlayer;
        }
        set
        {
            localPlayer = value;
            if (OnLocalPlayerJoined != null)
                OnLocalPlayerJoined(localPlayer);
        }
    }
}
