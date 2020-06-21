using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject eff;
    public GameObject granadeFactory;
    public int gCount = 20;
    public List<GameObject> granadePool;
    public float throwPower = 10.0f;

    ParticleSystem ps_eff;
    AudioSource shootAudio;
    bool isZoom = false;
    float currentTime = 0;
    

    void Start()
    {
        ps_eff = eff.GetComponent<ParticleSystem>();
        shootAudio = GetComponent<AudioSource>();

        // 오브젝트 풀 준비 작업하기
        granadePool = new List<GameObject>();

        for(int i = 0; i < gCount; i++)
        {
            // 1. 수류탄 프리팹을 생성한다.
            GameObject go = Instantiate(granadeFactory);

            // 2. 플레이어의 자식 오브젝트로 등록한다.
            go.transform.SetParent(transform);

            // 3. 탄창 리스트에 추가한다.
            granadePool.Add(go);

            // 4. 수류탄 오브젝트를 비활성한다.
            go.SetActive(false);
        }
    }

    void Update()
    {
        ZoomCheck();
        GunShoot();
        GranadeShoot();
    }

    void GunShoot()
    {
        // Input Axis 중에 "Fire1"을 누르면 총을 발사하게 하고 싶다.
        // 1. 레이 캐스트를 이용해서 사격한다. - 시선 방향
        // 2. 피격 지점에 이펙트와 사운드를 발생시킨다.

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                // 1. 이펙트를 발생시킨다.
                eff.transform.position = hitInfo.point;
                eff.transform.forward = hitInfo.normal;
                ps_eff.Stop();
                ps_eff.Play();

            }
            // 2. 사운드를 발생시킨다.
            shootAudio.Play();
        }
    }

    void GranadeShoot()
    {
        // Input axis의 "Fire2"를 누르면, 수류탄을 던진다.
        // 탄창을 20발 크기의 오브젝트 풀로 구현한다.
        if (Input.GetButtonDown("Fire2"))
        {
            if (granadePool.Count > 0)
            {
                granadePool[0].SetActive(true);
                granadePool[0].GetComponent<Rigidbody>().AddForce(transform.forward * throwPower, ForceMode.Impulse);
                granadePool[0].transform.SetParent(null);
                granadePool.RemoveAt(0);
            }
        }
    }

    void ZoomCheck()
    {
        // 만일, 키보드의 'z'키를 누르면 줌 인 상태가 되고, 한번 더 누르면 줌 아웃 상태가 된다.
        // 화면 줌 전환은 0.5초에 걸쳐서 전환되도록 표현한다.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isZoom)
            {
                StopAllCoroutines();
                StartCoroutine("ZoomIn");
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine("ZoomOut");
            }
            isZoom = !isZoom;
        }
    }

    IEnumerator ZoomIn()
    {
        while(currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Lerp(60, 25, currentTime * 2);
            yield return null;
        }
        currentTime = 0;
    }
    IEnumerator ZoomOut()
    {
        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Lerp(25, 60, currentTime * 2);
            yield return null;
        }
        currentTime = 0;
    }
}
