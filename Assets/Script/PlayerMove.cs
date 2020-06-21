using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float gravity = 10.0f;
    public float jumpPower = 20.0f;
    // 최대 점프 횟수
    public int maxJump = 2;
    // 최대 체력
    public int maxHP = 100;

    // 캐릭터 컨트롤러 변수
    CharacterController cc;

    // 수직 항력 변수
    float yVelocity = 0;

    // 점프 중인지 체크하는 변수
    //bool isJump = false;

    // 현재 점프 횟수
    int currentJump = 0;
    // 현재 체력
    int curHP = 0;
    // 체력 바 UI
    public Slider hpBar;

    // 현재 HP량을 UI 슬라이더 반영하는 것

   

    void Start()
    {
        // 캐릭터 컨트롤러 가져오기
        cc = transform.GetComponent<CharacterController>();

        curHP = maxHP;
    }

    void Update()
    {
        // 1. 나는 키보드의 w,a,s,d 키를 이용해서 이동하고 싶다!
        // 1-1. Input 클래스의 Axis를 받아서 그 값을 이용해서 이동한다.
        // 1-2. Axes는 "Horizontal"과 "Vertical"을 사용한다.
        // 1-3. 카메라가 바라보는 방향으로 이동한다.
        // 2. 나는 키보드의 space 키를 이용해서 점프하고 싶다!
        // 2-1. 땅에 닿았는지 여부, 키 입력, 점프력, 점프 횟수

        Move();
        hpBar.value = (float)curHP / (float)maxHP;
    }

    void Move()
    {
        // 플레이어의 입력 축 값을 받아온다.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 방향을 만든다.
        //Vector3 dir = new Vector3(h, 0, v);

        // 상대 좌표로 변환하기 방법 1
        Vector3 dir = transform.forward * v + transform.right * h;

        // 상대 좌표로 변환하기 방법 2
        //Vector3 dir = new Vector3(h, 0, v);
        //dir = Camera.main.transform.TransformDirection(dir);

        dir.Normalize();    // 벡터의 정규화
        dir *= moveSpeed;

        // 만일, 캐릭터 콘트롤러의 바닥 부분이 충돌 되었다면...
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            //isJump = false;
            currentJump = 0;
            yVelocity = 0;
        }

        if(Input.GetButtonDown("Jump") && currentJump < maxJump)
        {
            //isJump = true;
            currentJump++;
            yVelocity = jumpPower;
        }
        yVelocity -= gravity * Time.deltaTime;

        dir.y = yVelocity;



        // 현재 위치 = 직전의 위치 * 속도 * 시간 보간
        // p = p0 +vt
        //transform.position += dir * moveSpeed * Time.deltaTime;
        cc.Move(dir * Time.deltaTime);
    }

    // 데미지 처리 함수
    public void OnDamaged(int damage)
    {
        curHP -= damage;
    }
}
