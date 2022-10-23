using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<PlayerController> players = new List<PlayerController>();
    [SerializeField] List<Ball> balls = new List<Ball>();
    [SerializeField] float startingFuseDuration;

    bool gameOver = false;
    bool gameStarted;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            for (int j = 0; j < balls.Count; j++)
            {
                balls[j].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            if (InputManager.Instance.PlayAgain())
            {
                gameOver = false;
                gameStarted = false;

                UIManager.Instance.NewGameScreen();

                AnimationManager.Instance.Restart(1);
                AnimationManager.Instance.Restart(2);

                players[0].transform.position = new Vector3(-5, 0, 0);
                players[1].transform.position = new Vector3(5, 0, 0);

                players[0].ModifyFuseDuration(startingFuseDuration - players[0].GetFuseDuration());
                players[1].ModifyFuseDuration(startingFuseDuration - players[1].GetFuseDuration());

                balls[0].transform.position = new Vector3(-10, 0, 0);
                balls[1].transform.position = new Vector3(0, 0, 0);
                balls[2].transform.position = new Vector3(10, 0, 0);
            }
        }
        else if (!gameStarted)
        {
            if (InputManager.Instance.PlayAgain())
            {
                players[0].transform.position = new Vector3(-5, 0, 0);
                players[1].transform.position = new Vector3(5, 0, 0);

                players[0].ModifyFuseDuration(startingFuseDuration - players[0].GetFuseDuration());
                players[1].ModifyFuseDuration(startingFuseDuration - players[1].GetFuseDuration());

                balls[0].transform.position = new Vector3(-10, 0, 0);
                balls[1].transform.position = new Vector3(0, 0, 0);
                balls[2].transform.position = new Vector3(10, 0, 0);

                UIManager.Instance.StartCountdown();
            }
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetIsdead())
                {
                    gameOver = true;
                    if (i == 0)
                    {
                        UIManager.Instance.GameOverScreen(1);
                    }
                    else
                    {
                        UIManager.Instance.GameOverScreen(0);
                    }
                }

                players[i].ModifyFuseDuration(-Time.deltaTime);
            }
        }
    }

    public bool GetGameStarted()
    {
        return gameStarted;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void SetGameStarted(bool started)
    {
        gameStarted = started;
    }
}
