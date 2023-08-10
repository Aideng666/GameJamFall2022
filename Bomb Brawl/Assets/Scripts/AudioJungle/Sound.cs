using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip _clip;

    public string name;

    [Range(0f, 1f)]
    public float _volume;
    [Range(0f, 1f)]
    public float _pitch;

    public bool _loop;

    [HideInInspector]
    public AudioSource source;
}