using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    private AudioSource audioSource;
    private float Volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Volume;
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
    }
}
