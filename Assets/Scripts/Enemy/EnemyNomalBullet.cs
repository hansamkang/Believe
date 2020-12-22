using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNomalBullet : MonoBehaviour {
    public Rigidbody2D bullet;
    public float bulletSpeed = 4f;
    public float destroyTime = 4f; 

    // Use this for initialization
    void Start () {
        bullet.velocity = -transform.right * bulletSpeed;
        Destroy(gameObject, 4f);
    }

    public void dieDestroy()
    {
        Destroy(gameObject);
    }
}
