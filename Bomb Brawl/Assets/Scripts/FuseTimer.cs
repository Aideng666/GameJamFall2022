using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class FuseTimer : MonoBehaviour
{
    [SerializeField]
    private Transform fuseStart, fuseFinish, fuseSpark, boom;

    private LineRenderer lineRend;

    public float fuseDuration;

    public TMP_Text fuseDurationText;

    // Start is called before the first frame update
    void Start()
    {
        fuseDuration = 10f;

        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, new Vector2(fuseStart.position.x, fuseStart.position.y));
        lineRend.SetPosition(1, new Vector2(fuseFinish.position.x, fuseFinish.position.y));
        fuseSpark.position = new Vector3(fuseStart.position.x, fuseStart.position.y, 0f);
        fuseSpark.gameObject.SetActive(false);
        boom.position = transform.position;
        boom.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
                 
        StartCoroutine(Explode());
        
        fuseDurationText.text = fuseDuration.ToString("0");
        fuseDuration -= Time.deltaTime;

        if (fuseDuration <= 0)
        {
            fuseSpark.gameObject.SetActive(false);
            boom.gameObject.SetActive(true);
            Destroy(boom.gameObject, 0.5f);       
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            fuseDuration += 10f;
        }
    }

    private IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
    }
    private IEnumerator Explode()
    {
        fuseSpark.gameObject.SetActive(true);   

        while (fuseStart.position.x <= fuseFinish.position.x)
        {
            StartCoroutine(MoveOverSeconds(fuseStart.gameObject, fuseFinish.position, fuseDuration));

            lineRend.SetPosition(0, new Vector2(fuseStart.position.x, fuseStart.position.y));
            fuseSpark.position = new Vector3(fuseStart.position.x, fuseStart.position.y, 0f);
            yield return new WaitForSeconds(0.025f);
        }

        lineRend.positionCount = 0;
    }
}
