using UnityEngine;

public class Mouse : MonoBehaviour
{
    public bool CMP;
    private CircleCollider2D circleCollider;
    public GameObject LineFinder;
    private Vector3 mPosition; 


    void Awake()
    {
        CMP = false;

        circleCollider = gameObject.GetComponent<CircleCollider2D>();

        LineFinder = Resources.Load<GameObject>("Prefab/LineFinder");
    }


    void FixedUpdate()
    {
        mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mPosition.x, mPosition.y);

        circleCollider.enabled = CMP;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Ground" ||
             col.gameObject.tag == "Player" ||
             col.gameObject.tag == "Monster"||
             col.gameObject.tag == "Trap") && CMP)  {

            Instantiate(LineFinder, transform.position, transform.rotation);

            GameObject.Find("GameManager").GetComponent<LineMaker>().cheackTwo = false;
            CMP = false;
            
            circleCollider.enabled = col;
        }
    }
}