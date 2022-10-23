using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class FuseTimer : MonoBehaviour
{
    [SerializeField]
    private Transform fuseStart, fuseFinish, fuseSpark;

    private LineRenderer lineRend;

    public float fuseDuration;

    private bool coroutineStart = false;

    // Start is called before the first frame update
    void Start()
    {
        fuseDuration = 60f;

        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2; 
        lineRend.SetPosition(0, new Vector2(fuseStart.position.x, fuseStart.position.y));
        lineRend.SetPosition(1, new Vector2(fuseFinish.position.x, fuseFinish.position.y));
        fuseSpark.position = new Vector3(fuseStart.position.x, fuseStart.position.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGameStarted())
        {
           
            fuseDuration -= Time.deltaTime;
            if (!coroutineStart)
            {
                StartCoroutine(Explode());
            }
          
            coroutineStart = true;      
        }
           
    }

    private IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 endPos)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < fuseDuration)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, endPos, (elapsedTime / fuseDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //objectToMove.transform.position = endPos;
    }
    private IEnumerator Explode()
    {
        while (fuseStart.position.x <= fuseFinish.position.x)
        {
            StartCoroutine(MoveOverSeconds(fuseStart.gameObject, fuseFinish.position));

            lineRend.SetPosition(0, new Vector2(fuseStart.position.x, fuseStart.position.y));
            fuseSpark.position = new Vector3(fuseStart.position.x, fuseStart.position.y, 0f);
            yield return new WaitForSeconds(0.025f);
        }

        lineRend.positionCount = 0;
    }
}
