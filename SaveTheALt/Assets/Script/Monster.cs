using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private List<GameObject> foundObject; // Player태그를 가진 오브젝트를 저장하기 위한 리스트

    private ParticleSystem particle; // 오브젝트 터질때 생기는 파티클

    private SpriteRenderer thisImg; // 오브젝트의 이미지

    private GameObject enemy; // 가장 가까운 Player

    private float speed; // 오브젝트 이동속도

    private float angle; // 오브젝트 회전

    private float shortDis;


    /// <summary>
    /// 각종 게임 세팅
    /// </summary>
    void Awake()
    {

        thisImg = GetComponent<SpriteRenderer>();

        foundObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        speed = 1;

        particle = GameObject.Find("Boom").GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// 그림을 다 그리면 플레이어 위치로 다가감
    /// </summary>
    void Update()
    {
        if (foundObject[0] != null)
        {
            FindPlayer();
            
            if (!GameObject.Find("GameManager").GetComponent<LineMaker>().cheackOne)
            {
                
                Vector3 dir = enemy.transform.position - transform.position;

                transform.position += dir * speed * Time.deltaTime;

                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }
        }
    }

    /// <summary>
    /// 가장 가까운 Player을 찾고, 그 Player을 enemy에 넣음
    /// </summary>
    void FindPlayer()
    {

        shortDis = Vector2.Distance(gameObject.transform.position, foundObject[0].transform.position);

        enemy = foundObject[0];

        foreach (GameObject found in foundObject) {

            float Distence = Vector2.Distance(gameObject.transform.position, found.transform.position);

            if (Distence < shortDis) {
                enemy = found;
                shortDis = Distence;
            }
        }
    }

    /// <summary>
    /// 충돌한 오브젝트가 Trap이면 파티클 실행 시키고 코르틴 작동시킴
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap")) {
            if (this.gameObject.activeInHierarchy) {

                particle.transform.position = gameObject.transform.position;

                particle.Play();

                StartCoroutine(ComeBack());
            }
        }
    }

    /// <summary>
    /// 이 오브젝트 이미지를 없애고 오브젝트를 없애는 코드
    /// </summary>
    IEnumerator ComeBack()
    {
        thisImg.sprite = null;

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }

    void OnDisable()
    {
        Debug.Log("파괴됨");
    }
}