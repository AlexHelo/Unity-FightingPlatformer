using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    public static AudioClip jump, dash, grapple;
    public static AudioSource audiosrc;

    // Start is called before the first frame update
    void Start()
    {
        jump = Resources.Load<AudioClip>("Jump2");
        dash = Resources.Load<AudioClip>("Hit_Hurt8");
        grapple = Resources.Load<AudioClip>("Randomize3");

        audiosrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {

        switch (clip)
        {
            case "jump":
                audiosrc.PlayOneShot(jump);
                break;
            case "dash":
                audiosrc.PlayOneShot(dash);
                break;
            case "grapple":
                audiosrc.PlayOneShot(grapple);
                break;
        }
    }
}
