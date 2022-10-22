using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] List<Animator> playerAnimators = new List<Animator>();

    public static AnimationManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void WalkUp(int playerNum, bool isWalking = true)
    {
        playerAnimators[playerNum - 1].SetBool("Up", isWalking);
    }

    public void WalkDown(int playerNum, bool isWalking = true)
    {
        playerAnimators[playerNum - 1].SetBool("Down", isWalking);
    }

    public void WalkLeft(int playerNum, bool isWalking = true)
    {
        playerAnimators[playerNum - 1].SetBool("Left", isWalking);
    }

    public void WalkRight(int playerNum, bool isWalking = true)
    {
        playerAnimators[playerNum - 1].SetBool("Right", isWalking);
    }
}
