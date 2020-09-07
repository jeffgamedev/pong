using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static void PlayUISound(AudioClip clip)
    {
        if (clip != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}
