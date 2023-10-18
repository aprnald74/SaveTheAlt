using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour 
{

    private ParticleSystem particle;
    private SpriteRenderer thisImg;
    private GameObject xIcon;
    private Sprite change_Icon;
    private bool hitSomething;
    private bool count;


    void Awake()
    {
        count = true;

        thisImg = GetComponent<SpriteRenderer>();

        particle = GameObject.Find("Boom").GetComponent<ParticleSystem>();

        xIcon = Resources.Load<GameObject>("Prefab/X");

        change_Icon = Resources.Load<Sprite>("IMG/GyeongjuDie");

    }

    private void FixedUpdate() 
    {
        if (transform.position.y <= -5.5f && count) {
            particle.transform.position = gameObject.transform.position;

            particle.Play();
            hitSomething = true;

            StartCoroutine(ComeBack());
        }
    }

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

    IEnumerator ComeBack()
    {
        count = false;

        GameObject t = Instantiate(xIcon, GameObject.Find("Instantiate").transform);
        t.transform.position = this.gameObject.transform.position;

        if (hitSomething)
            thisImg.sprite = null;
        else 
            thisImg.sprite = change_Icon;
        yield return new WaitForSeconds(0.2f);

        GameObject.Find("GameManager").GetComponent<NowStage>().nPlayer = false;

    }
}
