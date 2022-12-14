using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<Sprite> countdownImages = new List<Sprite>();
    [SerializeField] Sprite[] winnerImages = new Sprite[2];
    [SerializeField] GameObject panel;
    [SerializeField] Image countdownImage;
    [SerializeField] Image playAgainImage;
    [SerializeField] Image spaceToStartImage;
    [SerializeField] Image winnerImage;

    [SerializeField] GameObject gameOverPanel;

    float pulseDelay = 1;
    float timeToNextPulse = 0;

    public static UIManager Instance { get; set; }


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
        if (CanPulse())
        {
            spaceToStartImage.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.95f, 2);
        }
    }

    bool CanPulse()
    {
        if (Time.time > timeToNextPulse)
        {
            timeToNextPulse = Time.time + pulseDelay;

            return true;
        }

        return false;
    }

    public void GameOverScreen(int playerNum)
    {
        gameOverPanel.SetActive(true);
        winnerImage.enabled = true;
        winnerImage.sprite = winnerImages[playerNum];   
        playAgainImage.enabled = true;   
    }

    public void NewGameScreen()
    {
        winnerImage.enabled = false;
        playAgainImage.enabled = false;
        spaceToStartImage.enabled = true;
    }

    public void StartCountdown()
    {
        winnerImage.enabled = false;
        playAgainImage.enabled = false;
        spaceToStartImage.enabled = false;

        StartCoroutine(BeginCountdown());
    }

    IEnumerator BeginCountdown()
    {
        countdownImage.DOFade(1, 0);
        countdownImage.sprite = countdownImages[0];
        countdownImage.transform.DOPunchPosition(new Vector3(0, 5, 0), 0.5f, 6);
        countdownImage.transform.DOPunchScale(new Vector3(1, 1, 1), 0.5f, 4);
        countdownImage.DOFade(0, 1);

        yield return new WaitForSeconds(1);

        countdownImage.DOFade(1, 0);
        countdownImage.sprite = countdownImages[1];
        countdownImage.transform.DOPunchPosition(new Vector3(0, 5, 0), 0.5f, 6);
        countdownImage.transform.DOPunchScale(new Vector3(1, 1, 1), 0.5f, 4);
        countdownImage.DOFade(0, 1);

        yield return new WaitForSeconds(1);

        countdownImage.DOFade(1, 0);
        countdownImage.sprite = countdownImages[2];
        countdownImage.transform.DOPunchPosition(new Vector3(0, 5, 0), 0.5f, 6);
        countdownImage.transform.DOPunchScale(new Vector3(1, 1, 1), 0.5f, 4);
        countdownImage.DOFade(0, 1);

        yield return new WaitForSeconds(1);

        countdownImage.DOFade(1, 0);
        countdownImage.sprite = countdownImages[3];
        countdownImage.transform.DOPunchPosition(new Vector3(0, 5, 0), 0.5f, 6);
        countdownImage.transform.DOPunchScale(new Vector3(5, 1, 1), 1, 4);
        countdownImage.DOFade(0, 1);

        GameManager.Instance.SetGameStarted(true);

        yield return new WaitForSeconds(5);

        countdownImage.enabled = false;
    }
}
