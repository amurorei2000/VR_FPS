using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAction : MonoBehaviour
{
    PlayerFire pm;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerFire>();
    }

    // 수류탄이 무언가에 충돌하였을 때의 처리
    private void OnCollisionEnter(Collision col)
    {
        // 풀로 돌아가기
        pm.granadePool.Add(gameObject);
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
    }
}
