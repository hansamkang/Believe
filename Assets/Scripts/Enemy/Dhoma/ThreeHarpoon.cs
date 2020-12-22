using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeHarpoon : MonoBehaviour {
    Transform harpoonTransform; //  작살의 Transform
    Transform playerTransform;  //  플레이어의 Transform
    float originX;  //  작살이 돌아갈 위치
    float originY;

    CircleCollider2D polygon;  

    float offset = 3.15f;   //작살 날아갈 방향 조절
    float speed = 0.5f; //창 날라가는 속도

    bool looking = false;   
    bool attacking = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag =="Wall")
        {
            attacking = false;
            polygon.enabled = false;
        }
    }

    // Use this for initialization
    void Start () {
        harpoonTransform = GetComponent<Transform>();
        polygon = GetComponent<CircleCollider2D>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        
    }

    void Update()
    {
        if(looking)
        {
            Vector2 dir = playerTransform.position - harpoonTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) - offset;
            harpoonTransform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
        }
        if (attacking)
        {
            harpoonTransform.Translate(-speed, 0, 0);
        }
    }
    
    public void look()
    {
        originX = harpoonTransform.position.x;
        originY = harpoonTransform.position.y;

        looking = true;
    }
    public void stopLooking()
    {
        looking = false;
    }

    public void attack()
    {
        polygon.enabled = true; 
        looking = false;
        attacking = true;
    }

    public void reOrigin()
    {
        harpoonTransform.position = new Vector2(originX, originY);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 27);
        harpoonTransform.rotation = Quaternion.identity;
        harpoonTransform.rotation *= rotation;
    }
}
