using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dhoma : Enemy {
    public GameObject[] harpoon;   //  작살 오브젝트
    public GameObject laser;    
    public GameObject projectileBig;  //보스 공격 큰 투사체
    public GameObject projectileSmall;
    public GameObject plazma;
    public GameObject bloody;
    public Transform attackPoint;   //보스 공격 위치
    public Transform attackPointT;  
    public Transform attackPointM;
    public Transform attackPointD;
    public float moveSpeed = 0.03f; // 보스 이동 속도
    public float decreaseHPtime = 1f; //  마지막 페이지의 보스 피 감소량
    float phaseSpeed = 1f;   //페이즈 변경시 보스 준비 위치로 이동 속도
    float fallingSpeed = 0.2f;

    Animator harpoonAnimator;
    FollowCam followCamera;
   
    SpriteRenderer diyingDhoma;
    float nextDecreaseHP = 0f;
   
    bool[] attack = { false, false, true, false, false }; // 0.NomalAttack 1.BreathAttack 2.projectileBig 3.PlazmaAttack
    bool[] phase = { true, false, false };

    bool pre = true;
    bool breath = false;
    bool rangeIn = false;
    bool chasing = false;
    bool lastAni = true;
    bool die = true;
    bool prePhase2 = true;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        harpoonAnimator = GameObject.Find("harpoon").GetComponent<Animator>();
        followCamera = GameObject.Find("Main Camera").GetComponent<FollowCam>();
        diyingDhoma = GameObject.Find("Dhoma Phase3 Form").GetComponent<SpriteRenderer>();

        setStartHP(600); //680 480

        //InvokeRepeating("stackAttack", 5, 20);
        Invoke("breathAttack", 6);
    }
	
	// Update is called once per frame0
	void Update () {
        if (phase[0]) // 페이즈 1
        {
            if (pre)    //  전준
            {
                pre = false;
                StartCoroutine(doPre());
            }
            else if (attack[0]) //  평타
            {
                chasing = false;
                attack[0] = false;
                StartCoroutine(Attack(0));
            }
            else if (attack[1]) //  브레스
            {
                chasing = false;
                attack[1] = false;
                StartCoroutine(Attack(1));
            }
            else if(chasing)
            {
                if (currentHP < 482)
                {
                    phase[1] = true;
                    phase[0] = false;
                    FindObjectOfType<AudioManager>().Play("Bell_02");
                }
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

                if ((positionDuration < -3 || positionDuration > 3) && !breath) //플레이어가 보스의 공격 사정거리 밖에 있음
                {
                    rangeIn = false;
                    enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, playerVectorX, moveSpeed);
                }
                else if (positionDuration < 3 && positionDuration > -3 && !rangeIn && !breath) //플레이어가 사정거리 안에 있음
                {
                    rangeIn = true;
                    attack[0] = true;
                }
                else if (breath) //브레스 어택 판정
                {
                    CancelInvoke("BreathAttack");
                    breath = false;
                    attack[1] = true;
                }
            }
        }
        else if(phase[1]) // 페이즈 2
        {
            if (prePhase2) {
                StartCoroutine(prePhaseCorutine2());
            }
            else {

                if (viewRight)
                {
                    viewRight = false;
                    enemyTransform.Rotate(0f, 180f, 0f);
                }

                CancelInvoke("BreathAttack");

                positionDuration = 8f - enemyTransform.position.x;
                Vector2 tempP = new Vector2(8f, enemyTransform.position.y);
                enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, tempP, phaseSpeed);

                if (attack[2] && positionDuration < 0.1f && !viewRight)
                {
                    attack[2] = false;
                    StartCoroutine(Attack(2));
                }

                if (attack[3])
                {
                    attack[3] = false;
                    StartCoroutine(Attack(6));
                }
            }
        }
        else if(phase[2]) // 페이즈 3
        {

            Vector2 tempP = new Vector2(0f, 4f);
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, tempP, phaseSpeed);

            float distance = Vector2.Distance(enemyTransform.position, tempP);

            if (distance < 0.2f)
            {
                if(lastAni)
                {
                    lastAni = false;
                    animator.Play("Dhoma_last");
                    harpoonAnimator.Play("Harpoon_last");
                }

                if( Time.time> nextDecreaseHP)
                {
                    currentHP--;
                    DecreaseUIHP();
                    nextDecreaseHP = Time.time + decreaseHPtime;
                }
                float ran = Random.Range(0, 180);
                attackPointM.Rotate(0f, 0f, ran);
                Instantiate(projectileSmall, attackPointD.position, attackPointM.rotation);
                attackPointM.rotation = Quaternion.identity;
            }
        }

        if (currentHP < 1 && FindObjectOfType<HeartSystem>().curHealth > 0)  //사망
        {
            if (die)
            {
                die = false;
                StartCoroutine(dieRoutine());
            }
            Vector2 tempP = new Vector2(0f, -3f);

            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, tempP, fallingSpeed);

          
        }
	}

    IEnumerator dieRoutine()
    {
        phase[2] = false;
        Director.DhomaCleared = true;
        SaveSystem.SaveData();
        FindObjectOfType<AudioManager>().BGM_Stop("Darkest day");
        FindObjectOfType<AudioManager>().BGM_Play("EndGame");
        GameObject bosshpbar = GameObject.Find("BossHpbar");
        Destroy(bosshpbar);
        harpoonAnimator.enabled = false;
        animator.Play("Dhoma_die");
        for (int i = 0; i < 3; i++)
        {
            harpoon[i].GetComponent<PolygonCollider2D>().enabled = true;
            harpoon[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        yield return new WaitForSeconds(0.8f);
        Instantiate(bloody, attackPointM.position, attackPointM.rotation);
        yield return new WaitForSeconds(2f);
        animator.enabled = false;
        diyingDhoma.enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(4f);
        FindObjectOfType<DhomaDirector>().backLobby();
    }

    IEnumerator doPre()
    {
        yield return new WaitForSeconds(0.5f);
        animator.Play("Dhoma_idle");
        rangeIn = false;
        chasing = true;
        if (currentHP < 480)
        {
            phase[1] = true;
            phase[0] = false;
            FindObjectOfType<AudioManager>().Play("Bell_02");
        }
    }

    IEnumerator Attack(int p)  //어택에서 Idle로 돌아가는 시간
    {
        switch (p){
            case 0:
                ThreeHarpoon h;

                animator.Play("Dhoma_nomalAttack");
                harpoonAnimator.Play("Harpoon_tremble");
                FindObjectOfType<AudioManager>().Play("Laugh");
                yield return new WaitForSeconds(2);

                harpoonAnimator.enabled = false;
                for (int i = 0; i < harpoon.Length; i++)
                {
                    h = harpoon[i].GetComponent<ThreeHarpoon>();
                    h.look();
                }
                yield return new WaitForSeconds(2);
                for (int i = 0; i < harpoon.Length; i++)
                {
                    h = harpoon[i].GetComponent<ThreeHarpoon>();
                    h.stopLooking();
                }
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < harpoon.Length; i++)
                {
                    h = harpoon[i].GetComponent<ThreeHarpoon>();
                    h.attack();
                    yield return new WaitForSeconds(0.8f);
                }
                yield return new WaitForSeconds(2f);
                for (int i = 0; i < harpoon.Length; i++)
                {
                    h = harpoon[i].GetComponent<ThreeHarpoon>();
                    h.reOrigin();
                }
                harpoonAnimator.enabled = true;
                harpoonAnimator.Play("Harpoon_idle");
                if (currentHP < 480)
                {
                    phase[1] = true;
                    phase[0] = false;
                    FindObjectOfType<AudioManager>().Play("Bell_02");
                }
                else
                    pre = true;
                break;
            case 1:
                FindObjectOfType<AudioManager>().Play("Laugh");
                Instantiate(laser, attackPoint.position, attackPoint.rotation);
                yield return new WaitForSeconds(1.1f);
                followCamera.ShakeCamera(1.5f, 1.5f);
                yield return new WaitForSeconds(2.2f);
                if (currentHP < 480)
                {
                    phase[1] = true;
                    phase[0] = false;
                    FindObjectOfType<AudioManager>().Play("Bell_02");
                }
                else
                {
                    Invoke("breathAttack", 11);
                    pre = true;
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++)
                {
                    int ran = Random.Range(3, 6);
                    StartCoroutine(Attack(ran));
                    yield return new WaitForSeconds(1.5f);
                }
                yield return new WaitForSeconds(1f);
                if (currentHP < 270)
                {
                    phase[2] = true;
                    phase[1] = false;
                }
                else
                    attack[3] = true;
                break;
            case 3: //  하단
                Instantiate(projectileBig, attackPointM.position, attackPointM.rotation);
                Instantiate(projectileBig, attackPointT.position, attackPointT.rotation);
                break;
            case 4: //  중단
                Instantiate(projectileBig, attackPointT.position, attackPointT.rotation);
                Instantiate(projectileBig, attackPointD.position, attackPointD.rotation);
                break;
            case 5: //  상단
                Instantiate(projectileBig, attackPointM.position, attackPointM.rotation);
                Instantiate(projectileBig, attackPointD.position, attackPointD.rotation);
                break;
            case 6:
                for (int i = 0; i < 15; i++)
                {
                    float ran = Random.Range(-8f, 9f);
                    Vector2 tempP = new Vector2(ran, -4f);
                    Instantiate(plazma, tempP, Quaternion.identity);
                    yield return new WaitForSeconds(0.3f);
                }
                yield return new WaitForSeconds(4f);

                if (currentHP < 270)
                {
                    FindObjectOfType<AudioManager>().Play("Bell_02");
                    phase[2] = true;
                    phase[1] = false;
                }
                else
                    attack[2] = true;
                break;
        }
    }

    IEnumerator prePhaseCorutine2()
    {
        yield return new WaitForSeconds(3F);
        prePhase2 = false;
    }

    void stackAttack()
    {
        Debug.Log("스택 공격");
    }

    void breathAttack()
    {
        breath = true;
    }
  
}
