using UnityEngine;

public class Mouse : MonoBehaviour
{
    [HideInInspector] public bool CMP;

    private CircleCollider2D circleCollider;
    private GameObject LineFinder;

    private Vector3 mPosition; 


    void Awake()
    {
        CMP = true;

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
        if (col.gameObject.tag == "Ground" ||
            col.gameObject.tag == "Player" ||
            col.gameObject.tag == "Monster"||
            col.gameObject.tag == "Trap" && CMP)  {
            GameObject.Find("GameManager").GetComponent<LineMaker>().cheackTwo = false;

            CMP = false;
            circleCollider.enabled = col;
            
            Instantiate(LineFinder, transform.position, transform.rotation);
        }
    }
}