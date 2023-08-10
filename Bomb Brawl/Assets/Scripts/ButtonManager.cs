using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Select()
    {
        AudioManager.Instance.Play("Select");
    }

    public void Cancel()
    {
        AudioManager.Instance.Play("Cancel");
    }
}
