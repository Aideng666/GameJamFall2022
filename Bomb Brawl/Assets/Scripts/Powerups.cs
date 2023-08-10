using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{

    public static Powerups instance;

    private void Awake() {
        
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }

    [SerializeField]
    List<GameObject> powers;

    [SerializeField]
    List<GameObject> players;

    [SerializeField]
    private float spawnDelay = 20;

    private float timeToNextSpawn = 2;

    private bool positionValid= false;
    private Vector2 powerupPosition;

    private int powerType;

    private bool powerOnField = false;

    bool roundStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGameStarted() && !GameManager.Instance.GetGameOver()/* && roundStarted*/)
        {
            if (!powerOnField)
            {
                if (SpawnPower())
                {
                    while (!positionValid)
                    {
                        powerupPosition = new Vector2(Random.Range(-14, 14), Random.Range(-5, 5));

                        for (int i = 0; i < players.Count; i++)
                        {
                            if (Vector2.Distance(powerupPosition, players[i].transform.position) < 5)
                            {
                                positionValid = false;
                                break;
                            }

                            positionValid = true;
                        }
                    }

                    powerType = Random.Range(1, 4);
                    Spawn(powerType);
                }
            }

            //if (!roundStarted)
            //{
            //    roundStarted = true;

            //    timeToNextSpawn = Time.time + 5;
            //}
        }
    }

    public void SetPowerOnField(bool state)
    {
        powerOnField = state;
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
  
    private void Spawn(int type)
    {
        Instantiate(powers[type-1], powerupPosition, Quaternion.identity);
        powerOnField = true;
    }

    public void Effect(GameObject player)
    {
        switch (powerType)
        {
            case 1:
                //strength
                player.GetComponent<PlayerController>().GrantStrengthBoost();
                break;
            case 2:
                //speed
                player.GetComponent<PlayerController>().GrantSpeedBoost();
                break;
            case 3:
                //time
                player.GetComponent<PlayerController>().GrantTimeBoost();
                break;
            default:
                break;
        }
    }

}
