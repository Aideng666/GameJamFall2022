using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Powerup")
        {
            Powerups.instance.Effect(transform.parent.gameObject);
            Powerups.instance.SetPowerOnFeild(false);
            Instantiate(effect, other.gameObject.transform.position ,Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
