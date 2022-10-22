using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [SerializeField] float maxSpeed = 24;
    [SerializeField] float fuseDepletionAmount = 5;
    [SerializeField] List<PlayerController> playerList = new List<PlayerController>();

    Rigidbody2D body;
    SpriteRenderer sprite;
    CircleCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.magnitude >= maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }

    public float GetSpeed()
    {
        return body.velocity.magnitude;
    }

    IEnumerator ResetBall()
    {
        sprite.enabled = false;
        col.enabled = false;
        body.velocity = Vector2.zero;

        yield return new WaitForSeconds(3);

        bool positionFound = false;

        Vector2 newBallPosition = Vector2.zero;

        while (!positionFound)
        {
            //Boundraries for new ball spawn is 14 for x and 5 for y
            newBallPosition = new Vector2(Random.Range(-14f, 14f), Random.Range(-5f, 5f));

            for (int i = 0; i < playerList.Count; i++)
            {
                if (Vector2.Distance(newBallPosition, playerList[i].transform.position) < 3)
                {
                    positionFound = false;

                    continue;
                }

                positionFound = true;
            }

            yield return null;
        }

        transform.position = newBallPosition;

        sprite.enabled = true;
        col.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Camera.main.transform.DOShakePosition(0.4f, 1f, 8, 90);

            collision.gameObject.GetComponentInParent<PlayerController>().ModifyFuseDuration(-fuseDepletionAmount);
            AudioManager.Instance.Play("Hit");

            StartCoroutine(ResetBall());
        }
    }
}
