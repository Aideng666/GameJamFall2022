using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!FindObjectOfType<AudioManager>().IsPlaying("BGaMe") && !FindObjectOfType<AudioManager>().IsPlaying("BGaMe"))
        {
            FindObjectOfType<AudioManager>().Play("BGaMe");
            FindObjectOfType<AudioManager>().Loop("BGaMe");
        }
    }


}
