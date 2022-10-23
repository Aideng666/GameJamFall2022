using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] PlayerController[] players = new PlayerController[2];

    public static ParticleManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyStrikeEffect(int playerNum)
    {
        players[playerNum].GetComponentInChildren<ParticleSystem>().Play();
    }
}
