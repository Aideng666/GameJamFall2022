using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerNum; // can only be 1 or 2
    [SerializeField] float moveSpeed;
    [SerializeField] float minStrikeSpeed = 6;
    [SerializeField] float strikeDelay = 1;
    [SerializeField] float dodgeDelay = 1;
    [SerializeField] float knockbackStunLength = 0.5f;

    Rigidbody2D body;

    float timeToNextStrike = 0;
    float timeToNextDodge = 0;

    bool knockbackActive = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!knockbackActive)
        {
            Move();

            CheckStrike();

            CheckDodge();
        }
    }

    void Move()
    {
        Vector2 inputDirection = Vector2.zero;

        //Checking move input for player 1 and 2
        if (playerNum == 1)
        {
            inputDirection = InputManager.Instance.Move1();
        }
        if (playerNum == 2)
        {
            inputDirection = InputManager.Instance.Move2();
        }

        body.velocity = inputDirection.normalized * moveSpeed;

        if (body.velocity.x > 0/* && body.velocity.x > body.velocity.y*/)
        {
            AnimationManager.Instance.WalkRight(playerNum);
            AnimationManager.Instance.WalkLeft(playerNum, false);
            AnimationManager.Instance.WalkUp(playerNum, false);
            AnimationManager.Instance.WalkDown(playerNum, false);
        }
        else if (body.velocity.x < 0/* && body.velocity.x < body.velocity.y*/)
        {
            AnimationManager.Instance.WalkLeft(playerNum);
            AnimationManager.Instance.WalkRight(playerNum, false);
            AnimationManager.Instance.WalkUp(playerNum, false);
            AnimationManager.Instance.WalkDown(playerNum, false);
        }
        else if (body.velocity.y < 0/* && body.velocity.x < body.velocity.y*/)
        {
            AnimationManager.Instance.WalkDown(playerNum);
            AnimationManager.Instance.WalkRight(playerNum, false);
            AnimationManager.Instance.WalkUp(playerNum, false);
            AnimationManager.Instance.WalkLeft(playerNum, false);
        }
        else if (body.velocity.y > 0/* && body.velocity.x < body.velocity.y*/)
        {
            AnimationManager.Instance.WalkUp(playerNum);
            AnimationManager.Instance.WalkRight(playerNum, false);
            AnimationManager.Instance.WalkLeft(playerNum, false);
            AnimationManager.Instance.WalkDown(playerNum, false);
        }
        else if (body.velocity.magnitude == 0)
        {
            AnimationManager.Instance.WalkUp(playerNum, false);
            AnimationManager.Instance.WalkRight(playerNum, false);
            AnimationManager.Instance.WalkLeft(playerNum, false);
            AnimationManager.Instance.WalkDown(playerNum, false);
        }
    }

    void CheckStrike()
    {
        //Detects if the player presses strike
        bool strikePressed = false;

        if (playerNum == 1)
        {
            strikePressed = InputManager.Instance.Strike1();
        }
        if (playerNum == 2)
        {
            strikePressed = InputManager.Instance.Strike2();
        }

        //Detects all colliders within striking range
        if (strikePressed && CanStrike())
        {
            transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.4f, 1, 1);

            Collider2D[] collidersHit = Physics2D.OverlapCircleAll(transform.position, 2);

            List<Collider2D> ballsHit = new List<Collider2D>();
            List<Collider2D> playersHit = new List<Collider2D>();

            for (int i = 0; i < collidersHit.Length; i++)
            {
                if (collidersHit[i].CompareTag("Ball"))
                {
                    ballsHit.Add(collidersHit[i]);
                }

                if (collidersHit[i].CompareTag("Player") && collidersHit[i].transform.parent.gameObject != gameObject)
                {
                    playersHit.Add(collidersHit[i]);
                }
            }

            //Strikes all balls that were hit
            for (int i = 0; i < ballsHit.Count; i++)
            {
                Vector2 strikeDirection = (ballsHit[i].transform.position - transform.position).normalized;

                if (ballsHit[i].GetComponent<Rigidbody2D>().velocity.magnitude < minStrikeSpeed)
                {
                    ballsHit[i].GetComponent<Rigidbody2D>().velocity = strikeDirection * minStrikeSpeed;
                }
                else
                {
                    ballsHit[i].GetComponent<Rigidbody2D>().velocity = strikeDirection * ballsHit[i].GetComponent<Ball>().GetSpeed();
                }
            }

            //Strikes players that were hit
            for (int i = 0; i < playersHit.Count; i++)
            {
                Vector2 knockbackDirection = (playersHit[i].transform.position - transform.position).normalized;

                playersHit[i].transform.parent.GetComponent<PlayerController>().ApplyKnockback(knockbackDirection, minStrikeSpeed);
            }
        }
    }

    void CheckDodge()
    {
        bool dodgePressed = false; 

        if (playerNum == 1)
        {
            dodgePressed = InputManager.Instance.Dodge1();
        }
        if (playerNum == 2)
        {
            dodgePressed = InputManager.Instance.Dodge2();
        }

        if (dodgePressed && CanDodge())
        {
            //Do a dodge
        }
    }

    bool CanStrike()
    {
        if (Time.time > timeToNextStrike)
        {
            timeToNextStrike = Time.time + strikeDelay;

            return true;
        }

        return false;
    }

    bool CanDodge()
    {
        if (Time.time > timeToNextDodge)
        {
            timeToNextDodge = Time.time + dodgeDelay;

            return true;
        }

        return false;
    }

    public void ApplyKnockback(Vector2 direction, float initKnockSpeed)
    {
        knockbackActive = true;

        StartCoroutine(Knockback(direction * initKnockSpeed));
    }

    IEnumerator Knockback(Vector2 initKnockVel)
    {
        Vector2 startVel = initKnockVel;
        float elaspedTime = 0;

        while (elaspedTime <= knockbackStunLength)
        {
            body.velocity = Vector3.Lerp(startVel, Vector3.zero, elaspedTime / knockbackStunLength);

            elaspedTime += Time.deltaTime;

            yield return null;
        }

        knockbackActive = false;

        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Gizmos.DrawWireSphere(transform.position, 2);
    }
}
