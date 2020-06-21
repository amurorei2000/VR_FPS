using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 100;
    public float angleLimit = 60.0f;

    // 회전 가능 체크용 변수
    public bool lock_X = false;
    public bool lock_Y = false;


    // 회전 값 누적 변수
    float mx = 0;
    float my = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // 마우스를 상하좌우로 움직이면, 이 오브젝트도 상하좌우로 회전하게 하고 싶다!

        // 마우스 입력 값을 받아서 회전 변수에 누적시킨다.
        float mouseX = lock_Y ? 0 : Input.GetAxis("Mouse X");
        float mouseY = lock_X ? 0 : Input.GetAxis("Mouse Y");

        mx += mouseX * rotSpeed * Time.deltaTime;
        my += mouseY * rotSpeed * Time.deltaTime;
        my = Mathf.Clamp(my, -angleLimit, angleLimit);

        // 회전 방향을 설정한다.
        Vector3 rot = new Vector3(-my, mx, 0);

        // 그 방향으로 회전한다.(회전 속도)
        // r = r0 +vt
        transform.localEulerAngles = rot;

        // 상하 회전의 경우 -60도 ~ 60도로 각도를 제한한다.
        /*Vector3 angle = transform.eulerAngles;
        angle.x = Mathf.Clamp(angle.x, -60.0f, 60.0f);
        transform.eulerAngles = angle;*/
    }
}
