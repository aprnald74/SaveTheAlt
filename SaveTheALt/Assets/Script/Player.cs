using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour 
{

    private ParticleSystem particle; // 오브젝트 터질때 생기는 파티클

    private SpriteRenderer thisImg; // 오브젝트 이미지

    private Sprite change_Icon; // 바뀔 이미지


    /// <summary>
    /// 각종 게임 세팅
    /// </summary>
    void Awake()
    {
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster")) {
            thisImg.sprite = change_Icon;
        }

        if (collision.collider.CompareTag("Trap")) 
            if (this.gameObject.activeInHierarchy) {

                particle.transform.position = gameObject.transform.position;

                particle.Play();

                StartCoroutine(ComeBack());
            } 
    }

    /// <summary>
    /// 이 오브젝트 이미지를 없애고 현제 Player수를 줄이고 이 오브젝트를 없애는 코드
    /// </summary>
    IEnumerator ComeBack()
    {
        thisImg.sprite = null;

        yield return new WaitForSeconds(0.3f);

        GameObject.Find("GameManager").GetComponent<Stage>().nPlayer -= 1;

        Destroy(gameObject);
    }
}
