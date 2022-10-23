using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleEffect : MonoBehaviour
{
    float elaspedLife;

    // Start is called before the first frame update
    void Start()
    {
        elaspedLife = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (elaspedLife >= 0.45f)
        {
            Destroy(gameObject);
        }

        elaspedLife += Time.deltaTime;
    }
}
