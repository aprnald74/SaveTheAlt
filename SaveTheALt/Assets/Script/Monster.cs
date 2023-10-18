using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private List<GameObject> foundObject;
    private ParticleSystem particle;
    private SpriteRenderer thisImg;
    private GameObject enemy;
    private float speed;
    private float angle;
    private float shortDis;


    void Awake()
    {

        thisImg = GetComponent<SpriteRenderer>();

        foundObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        particle = GameObject.Find("Boom").GetComponent<ParticleSystem>();

        speed = 1;

        FindPlayer();
    }


    void Update()
    {
        if (!GameObject.Find("GameManager").GetComponent<LineMaker>().cheackOne) {
            Vector3 dir = enemy.transform.position - transform.position;

            transform.position += dir * speed * Time.deltaTime;

            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
    }


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


    IEnumerator ComeBack()
    {
        thisImg.sprite = null;

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }
}