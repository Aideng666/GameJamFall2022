using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject effectObj;

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Powerup")
        {
            Powerups.instance.Effect(transform.parent.gameObject);
            Powerups.instance.SetPowerOnField(false);
            Instantiate(effectObj, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
