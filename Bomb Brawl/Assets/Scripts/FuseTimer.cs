using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseTimer : MonoBehaviour
{
    [SerializeField]
    private Transform fuseStart, fuseFinish, fuseSpark, boom;

    private LineRenderer lineRend;

    private bool coroutineAllowed;


    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, new Vector2(fuseStart.position.x, fuseStart.position.y));
        lineRend.SetPosition(1, new Vector2(fuseFinish.position.x, fuseFinish.position.y));
        fuseSpark.position = new Vector3(fuseStart.position.x, fuseStart.position.y, 0f);
        fuseSpark.gameObject.SetActive(false);
        boom.position = transform.position;
        boom.gameObject.SetActive(false);
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && coroutineAllowed)
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        coroutineAllowed = false;
        fuseSpark.gameObject.SetActive(true);

        while (fuseStart.position.x <= fuseFinish.position.x)
        {
            fuseStart.Translate(0f, -0.05f, 0f);
            lineRend.SetPosition(0, new Vector2(fuseStart.position.x, fuseStart.position.y));
            fuseSpark.position = new Vector3(fuseStart.position.x, fuseStart.position.y, 0f);
            yield return new WaitForSeconds(0.025f);
        }

        fuseSpark.gameObject.SetActive(false);
        boom.gameObject.SetActive(true);
        //Destroy(boom.gameObject, 0.5f);
        lineRend.positionCount = 0;
        Destroy(gameObject);
    }
}
