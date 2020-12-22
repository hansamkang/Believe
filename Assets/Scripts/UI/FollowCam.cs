using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public Transform target; // 추적할 타켓 오브젝트의 Transform

    //public float offset = 0f;
    public const float offsetY = 0f; //  카메라 Y축 기본값
    public const float offsetZ = -10f; //   카메라 Z축 기본값 
    public float limitright = 2f;      // 화면 오른쪽 제한값
    public float limitleft = 2f;        //화면 왼쪽 제한값
    //public float smoothRotate = 5.0f;   //  부드러운 회전을 위한 변수
    public float followSpeed = 10.0f;   // 타겟을 따라가는 속도

    public float shakeTimer = 0; //흔들림 효과 시간 
    public float shakeAmount; //흔들림 범위

    private Transform tr; //    카메라 자신의 Transform 변수
    Vector3 cameraPosition;
    Vector2 originPos;

    void Start () {
        tr = GetComponent<Transform>();
        originPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeTimer >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount + originPos;

            transform.position = transform.position + new Vector3(ShakePos.x, ShakePos.y, 0);

            shakeTimer -= Time.deltaTime;
        }
    }

    void LateUpdate () {
        if (target.position.x < limitright && target.position.x > -(limitleft))
        {
            cameraPosition = new Vector3(target.position.x, offsetY, offsetZ);
        }
        else if(target.position.x >limitright)
        {
            cameraPosition = new Vector3(limitright, offsetY, offsetZ);
        }
        else if(target.position.x < -(limitleft))
        {
            cameraPosition = new Vector3(-limitleft, offsetY, offsetZ);
        }
        transform.position = Vector3.Lerp(tr.position, cameraPosition, followSpeed * Time.deltaTime);
        //  타겟을 바라보게 하기
        //tr.LookAt(target);
    }

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmount = shakePwr;
        shakeTimer = shakeDur;
    }
}
