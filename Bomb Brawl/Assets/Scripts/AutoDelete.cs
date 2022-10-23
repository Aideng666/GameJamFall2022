using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    [SerializeField]
    private float expireTime = 0.35f;

    private float upTime = 0;

    // Update is called once per frame
    void Update()
    {
            upTime += Time.deltaTime;
            if(upTime > expireTime)
            {
                Destroy(gameObject);
            }
    }
}
