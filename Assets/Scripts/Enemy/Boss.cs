using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            currentHP--;
            decreaseUIHP();
            StartCoroutine(demage());
        }
    }

    void Awake()
    {
        enemyTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectsWithTag("EnemyBoss"); // Enemy로 되어있는 상속 개체들도 전부 색 변경을 위해
        HpGauge = GameObject.Find("HpGauge");
    }
}
