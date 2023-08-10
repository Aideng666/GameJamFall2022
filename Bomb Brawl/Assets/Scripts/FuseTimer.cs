using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FuseTimer : MonoBehaviour
{
    [SerializeField]
    private Transform fuseStart, fuseFinish, fuseSpark;

    [SerializeField] PlayerController attachedPlayer;

    [SerializeField] Image fuseImage;

    float defaultFuseDuration;

    private float currentFuseDuration;

    // Start is called before the first frame update
    void Start()
    {
        defaultFuseDuration = attachedPlayer.GetFuseDuration();
        currentFuseDuration = defaultFuseDuration;

        fuseSpark.position = fuseStart.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentFuseDuration = attachedPlayer.GetFuseDuration();

        if (GameManager.Instance.GetGameStarted() && !GameManager.Instance.GetGameOver())
        {
            fuseSpark.position = Vector3.Lerp(fuseFinish.position, fuseStart.position, currentFuseDuration / defaultFuseDuration);
            fuseImage.fillAmount = Mathf.Lerp(0, 1, currentFuseDuration / defaultFuseDuration);

            currentFuseDuration -= Time.deltaTime;    
        }
    }

    public void RestartFuse()
    {
        currentFuseDuration = defaultFuseDuration;
    }
}
