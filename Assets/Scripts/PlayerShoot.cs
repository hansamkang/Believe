using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    ButtonPress shootButton;
    public Transform firePoint;
    public Animator animator;
    public GameObject bulletPrefab;
    public PlayerMovement playerMovement;

    public float fireDelay;
    private bool fireState;
    private bool keyEnable;

    void Start () {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        shootButton = GameObject.Find("ShotingButton").GetComponent<ButtonPress>();
        fireDelay = 0.2f;
        fireState = true;
        keyEnable = true;
    }

	void Update () {
        if (FindObjectOfType<HeartSystem>().curHealth > 0)
        {
            if (fireState && (Input.GetKey(KeyCode.Z) || shootButton.pressing))
            {
                //사격관련 애니메이션
                if (playerMovement.enableJump >= 1.5f && keyEnable)
                { //일반
                    keyEnable = false;
                    animator.Play("Player_shoot");
                }
                if (playerMovement.enableJump < 1.5f && keyEnable)
                { //점프샷
                    keyEnable = false;
                    animator.Play("Player_jumpShoot");
                }
                Shoot();//실질적인 사격
            }

            if (Input.GetKeyUp(KeyCode.Z) || shootButton.pressUp)
            {
                shootButton.pressUp = false;
                keyEnable = true;
                if (playerMovement.enableJump < 1.5f)
                {
                    animator.Play("Player_jumping");
                }
                else
                {
                    animator.Play("Player_stopshooting");
                }

            }
        }
    }

    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Projectile_01");
        StartCoroutine(FireCycleControl());
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator FireCycleControl()
    {
        fireState = false; 
        yield return new WaitForSeconds(fireDelay); //Delay
        fireState = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!keyEnable && collision.gameObject.tag == "Ground")
        {
            animator.Play("Player_shooting");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.Play("Player_jumpShoot");
        }
    }
}
