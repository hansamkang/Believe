using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rigid2d;
    ButtonPress jumpButton;
    ButtonPress shootButton;
    HeartSystem heartSystem;
    GameObject[] players;
    Animator animator;

    public float runSpeed = 35f;
    public float enableJump = 1.5f; //점프 가능 시간
    public float horizontalMove = 0f;
    public int key = 0;
    public float speedx = 0;

    float jumpForce = 90f;
    float maxSpeed = 10f;
    float jumpdecrement = 0.2f; //점프 가능 시간 감소량
    float waitTime = 0.5f; //피격시 반짝이는 시간 간격
    bool viewRight = true;
    bool damaging = false;
    bool alive = true;
    public bool matchless = false;

    void Start()
    {
        jumpButton = GameObject.Find("JumpButton").GetComponent<ButtonPress>();
        shootButton = GameObject.Find("ShotingButton").GetComponent<ButtonPress>();
        heartSystem = GetComponent<HeartSystem>();
        players = GameObject.FindGameObjectsWithTag("Player");
        this.rigid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && FindObjectOfType<HeartSystem>().curHealth > 0)
        {
            animator.Play("Player_groundreach");
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            float speedy = Mathf.Abs(this.rigid2d.velocity.y);
            if(speedy ==0)
            {
                enableJump = 1.5f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack" && !matchless && FindObjectOfType<HeartSystem>().curHealth > 0) //피격시
        {
            StartCoroutine(damage());
        }

    }

    public IEnumerator damage()
    {
        damaging = true;
        FindObjectOfType<AudioManager>().Play("Demege_01");
        animator.Play("Player_damage");
        heartSystem.takeDamage(1);
        matchless = true;
        for (int i = 0; i < 3; i++)
        {
            Color originalColor = new Color(1f, 1f, 1f, 1f);
            Color cColor = new Color(1f, 1f, 1f, 0.3f);

            changeColor(cColor);
            yield return new WaitForSeconds(waitTime);
            changeColor(originalColor);
            yield return new WaitForSeconds(waitTime);
        }
        damaging = false;
        matchless = false;
    }

    void Update() //좌우이동
    {
        horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");
        key = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || horizontalMove < 0) key = -1;
        if (Input.GetKey(KeyCode.RightArrow) || horizontalMove > 0) key = 1;

    }

    void FixedUpdate()
    {
        if (FindObjectOfType<HeartSystem>().curHealth <= 0)
        {
            if (alive == true)
            {
                alive = false;
                StartCoroutine(die());
            }
        }
        else
        {
            if (key == -1 && viewRight) //좌측대비
            {
                contrast();
            }

            if (key == 1 && !viewRight) //우측 대비
            {
                contrast();
            }

            if ((Input.GetKey(KeyCode.X) || jumpButton.pressing) && enableJump > 0) //점프 
            {
                if (!Input.GetKey(KeyCode.Z) || !shootButton.pressing)
                {
                    animator.Play("Player_jumpup");
                }
                Jump();
            }

            speedx = Mathf.Abs(this.rigid2d.velocity.x); // X축 이동값 계산

            if (speedx < maxSpeed && key == 1) //우측 이동
            {
                runRight();
            }

            if (speedx < maxSpeed && key == -1) //좌측 이동
            {
                runLeft();
            }

            if (key == 0 && !damaging && enableJump >= 1.5f && !(Input.GetKey(KeyCode.Z) || shootButton.pressing))
            {  //달리기 애니메이션 차단
                animator.Play("Player_idle");
            }
        }

    }

    void runLeft()
    {
        if ((key != 0) && !(Input.GetKey(KeyCode.Z) || shootButton.pressing) && enableJump > 0) {
            animator.Play("Player_walk");
        }
        this.rigid2d.AddForce(transform.right * -key * runSpeed);
    }
 
    void runRight() 
    {
        if ((key != 0) && !(Input.GetKey(KeyCode.Z) || shootButton.pressing) && enableJump > 0) {
            animator.Play("Player_walk");
        }
        this.rigid2d.AddForce(transform.right * key * runSpeed);
    }

    void Jump() //점프 
    {
        this.rigid2d.AddForce(transform.up * this.jumpForce);
        enableJump -= jumpdecrement;
    }

    void contrast() // 좌우 변경
    {
        viewRight = !viewRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void changeColor(Color color) //색 변경 함수
    {
        for (int i = 0; i < players.Length; i++)
        {
            SpriteRenderer render = players[i].GetComponent<SpriteRenderer>();
            render.color = color;
        }
    }

    IEnumerator die()
    {
        animator.Play("Player_diying");
        yield return new WaitForSeconds(1f);
        Animator gameover = GameObject.Find("GameOver").GetComponent<Animator>();
        gameover.Play("GameOver_opening");
    }
}

