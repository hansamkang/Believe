using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adeio : Boss {
    public GameObject serve;

    public float moveSpeed = 0.04f; // 보스 이동속도

    public Transform spawnT;
    bool[] phase = { true, false, false, false };   // 0.Attack Phase 1, Attack Phase 2, Attack Phase 3, Die Phase
    bool[] attack = { false, false, false, false, false, false, false, false };
    // 0. Nomal Attack 1.Spawn Monster 2.Throw Bone 3.Drop the Cross 4.
    bool pre = true;    // 공격과 공격 사이 준비 동작
    bool chasing = false;
    bool rangeIn = false;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        setStartHP(600);
        InvokeRepeating("serveSpawn", 4,6);
    }
	
	// Update is called once per frame
	void Update () {
		if(phase[0]) //페이지 1
        {   

            if(attack[2] && !pre)
            {
                attack[2] = false;
                StartCoroutine(Attack(2));
            }

            if (pre)    // 준비
            {
                pre = false;
                StartCoroutine(doPre());
            }
            else if (attack[0])   // 평타
            {
                chasing = false;
                attack[0] = false;
                StartCoroutine(Attack(0));
            }
            else if (chasing) // 추적
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

                if ((positionDuration < -1 || positionDuration > 1)) //플레이어가 보스의 공격 사정거리 밖에 있음
                {
                    rangeIn = false;
                    enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, playerVectorX, moveSpeed);
                }
                else if (positionDuration < 1 && positionDuration > -1 && !rangeIn) //플레이어가 사정거리 안에 있음
                {
                    rangeIn = true;
                    StartCoroutine(Attack(0));
                }
            }
        }
        else if(phase[1])
        {

        }
        else if(phase[2])
        {
           
        }
        else if(phase[3])
        {

        }
        else if(phase[4])
        {

        }
            
	}

    IEnumerator doPre()
    {
        animator.Play("Adeio_idle");
        yield return new WaitForSeconds(0.5f);
        rangeIn = false;
        chasing = true;
    }

    IEnumerator Attack(int p)
    {
        switch (p)
        {
            case 0:// 평타
                chasing = false;
                animator.Play("Adeio_nomalAttack");
                yield return new WaitForSeconds(1);
                pre = true;
                break;
            case 1: // 쫄 소환
                Vector3 temp = new Vector3(Random.Range(-5,5), spawnT.position.y, spawnT.position.z);
                Instantiate(serve, temp, spawnT.rotation); //지금 쫄 소환
                break;
            case 2: // 뼈 던지기
                //바로실행
                yield return new WaitForSeconds(7);
                attack[1] = true;
                break;               
        }
    }
    
    void serveSpawn()
    {
        Debug.Log("실행");
        StartCoroutine(Attack(1));
    }
}

