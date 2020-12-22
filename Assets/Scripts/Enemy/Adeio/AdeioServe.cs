using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdeioServe : Serve {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            randing = true;
        }
    }
    bool randing = false;
    bool pre = true;
    bool move = false;
    float moveSpeed = 0.06f;
    
    // Use this for initialization
    void Start () {
        setStartHP(5);
        
    }
	
	// Update is called once per frame
	void Update () {
        if(currentHP <= 0)
        {
            StartCoroutine(Die());
        }

        if (randing)
        {
            if (pre)
            {
                pre = false;
                StartCoroutine(Pre());
            }
            else if (move)
            {
                positionDuration = playerTransform.position.x - enemyTransform.position.x;
                playerVectorX = new Vector2(playerTransform.position.x, enemyTransform.position.y);

                if (positionDuration > 0 && !viewRight) //  보스 좌우 반전
                {
                    viewRight = true;
                    enemyTransform.Rotate(0f, 180f, 0f);
                }
                else if (positionDuration < 0 && viewRight)
                {
                    viewRight = false;
                    enemyTransform.Rotate(0f, 180f, 0f);
                }

                enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, playerVectorX, moveSpeed);
            }
        }
	}
    IEnumerator Pre()
    {
        animator.Play("AdeioServe_randing");
        yield return new WaitForSeconds(0.3f);
        animator.Play("AdeioServe_walk");
        move = true;
    }
    IEnumerator Die()
    {
        move = false;
        animator.Play("AdeioServe_die");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}

