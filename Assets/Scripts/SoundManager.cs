using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager _Instance;

    public CollectAsteroids a;

    public GameObject dialogueAudioGO;
    public GameObject collisionAudioGO;

    void Awake()
    {
        if (_Instance == null)
            _Instance = this;
        else
            Destroy(this);
    }

	public void PlaySound(AudioClip clip, Vector3 pos, float vol, bool dialogue)
    {
        if (dialogue)
        {
            dialogueAudioGO.GetComponent<AudioSource>().PlayOneShot(clip, vol);
            /*foreach(AudioSource a in dialogueAudioGO.GetComponentsInChildren<AudioSource>())
            {
                if (played)
                    return;

                a.clip = clip;
                a.transform.position = pos;
                a.volume = vol;
                a.PlayOneShot();
            }*/
        }
        else
        {
            collisionAudioGO.GetComponent<AudioSource>().PlayOneShot(clip, vol);
        }
    }
}
