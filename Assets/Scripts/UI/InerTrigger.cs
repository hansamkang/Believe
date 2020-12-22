using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InerTrigger : MonoBehaviour {
    PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectileAttack" && !playerMovement.matchless) //피격시
        {
            Debug.Log("테스트");
            StartCoroutine(playerMovement.damage());
        }
    }

    // Use this for initialization
    void Start () {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}

}
