using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audiocontroller : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float DelayBetweenClips;

    bool canPlay;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }

    // Update is called once per frame
    public void Play()
    {
        if (!canPlay)
            return;
        GameManager.Instance.Timer.Add(() =>
        {
            canPlay = true;
        },DelayBetweenClips);
        canPlay = false;
        int index = Random.Range(0, clips.Length);
        AudioClip clip = clips[index];
        source.PlayOneShot(clip);
    }
}
