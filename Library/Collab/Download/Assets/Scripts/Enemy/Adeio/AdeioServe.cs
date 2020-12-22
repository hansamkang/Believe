using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdeioServe : Enemy {
    bool randing = false;
    float moveSpeed = 0.06f;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            randing = true;
            // 착지 애니메이션
        }
    }
    // Use this for initialization
    void Start () {
        setStartHP(5);
	}
	
	// Update is called once per frame
	void Update () {
        if (randing)
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
