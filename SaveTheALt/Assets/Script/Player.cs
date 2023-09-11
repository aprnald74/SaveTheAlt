using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour 
{

    private ParticleSystem particle; // 오브젝트 터질때 생기는 파티클

    private SpriteRenderer thisImg; // 오브젝트 이미지

    private GameObject xIcon;

    private Sprite change_Icon; // 바뀔 이미지

    private bool hitSomething;

    private bool count;


    /// <summary>
    /// 각종 게임 세팅
    /// </summary>
    void Awake()
    {

        count = true;

        xIcon = Resources.Load<GameObject>("Prefab/X");

        particle = GameObject.Find("Boom").GetComponent<ParticleSystem>();

        thisImg = GetComponent<SpriteRenderer>();

        change_Icon = Resources.Load<Sprite>("IMG/GyeongjuDie");

    }

    /// <summary>
    /// 이 오브젝트가 -5 이하로 떨어지면 작동
    /// </summary>
    void Update() 
    {
        Vector2 currentPosition = transform.position;
        if (currentPosition.y <= -5) 
            transform.position = new Vector2(0, 4);
        
    }

    /// <summary>
    /// 충돌한 오브젝트가 Monster이면 이미지 바꾸고 <br />
    /// Trap이면 파티클 실행 시키고 코르틴 작동시킴
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Lava") && count) {
            particle.transform.position = gameObject.transform.position;

            particle.Play();

            hitSomething = true;

            StartCoroutine(ComeBack());
            
        } else if ((collision.collider.CompareTag("Trap") || collision.collider.CompareTag("Monster")) && count) {
            particle.transform.position = gameObject.transform.position;

            particle.Play();

            hitSomething = false;

            StartCoroutine(ComeBack());
        }
            
    }

    /// <summary>
    /// 이 오브젝트 이미지를 없애고 현제 Player수를 줄이고 이 오브젝트를 없애는 코드
    /// </summary>
    IEnumerator ComeBack()
    {

        count = false;

        GameObject t = Instantiate(xIcon, GameObject.Find("Instantiate").transform);
        t.transform.position = this.gameObject.transform.position;

        if (hitSomething)
            thisImg.sprite = null;
        else 
            thisImg.sprite = change_Icon;
        yield return new WaitForSeconds(0.3f);

        GameObject.Find("GameManager").GetComponent<Stage>().nPlayer = true;
    }
}
