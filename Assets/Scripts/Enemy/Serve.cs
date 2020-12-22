using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serve : Enemy {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            currentHP--;
            StartCoroutine(demage());
        }
    }

    void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        enemyTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectsWithTag("EnemyServe"); // Serve 로 되어있는 상속 개체들도 전부 색 변경을 위해
    }
}
