using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    List<GameObject> powerups;  

    [SerializeField]
    List<GameObject> players;

    [SerializeField]
    private float spawnDelay = 20;

    private float timeToNextSpawn;

    private bool positionValid= false;
    private Vector2 powerupPosition;

    // Update is called once per frame
    void Update()
    {
        if(SpawnPower())
        {
            powerupPosition = new Vector2(Random.Range(-14,14), Random.Range(-5,5));

            while(!positionValid)
            {
                for (int i = 0; i< players.Count; i++)
                {
                    if (Vector2.Distance(powerupPosition, players[i].transform.position) < 3)
                    {
                        positionValid = false;
                        continue;
                    }

                    positionValid = true;
                }
            }

            
           
        }

    }

    private bool SpawnPower()
    {
        if (Time.time > timeToNextSpawn)
        {
            timeToNextSpawn = Time.time + spawnDelay;

            return true;
        }

        return false;
    }


}
