using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour 
{

    private ParticleSystem particle; // ������Ʈ ������ ����� ��ƼŬ

    private SpriteRenderer thisImg; // ������Ʈ �̹���

    private GameObject xIcon;

    private Sprite change_Icon; // �ٲ� �̹���

    private bool hitSomething;

    private bool count;


    /// <summary>
    /// ���� ���� ����
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
    /// �� ������Ʈ�� -5 ���Ϸ� �������� �۵�
    /// </summary>
    void Update() 
    {
        Vector2 currentPosition = transform.position;
        if (currentPosition.y <= -5) 
            transform.position = new Vector2(0, 4);
        
    }

    /// <summary>
    /// �浹�� ������Ʈ�� Monster�̸� �̹��� �ٲٰ� <br />
    /// Trap�̸� ��ƼŬ ���� ��Ű�� �ڸ�ƾ �۵���Ŵ
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
    /// �� ������Ʈ �̹����� ���ְ� ���� Player���� ���̰� �� ������Ʈ�� ���ִ� �ڵ�
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
