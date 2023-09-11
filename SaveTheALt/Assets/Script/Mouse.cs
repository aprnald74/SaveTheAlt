using UnityEngine;

public class Mouse : MonoBehaviour
{
    [HideInInspector] public bool CMP; // CMP에 따라 collider이 켜지고 꺼짐

    private CircleCollider2D circleCollider; // 오브젝트의 collider

    private GameObject LineFinder; // LineFinder Prefab

    private Vector3 mPosition; // 오브젝트의 위치값

    /// <summary>
    /// 각종 게임 세팅
    /// </summary>
    void Awake()
    {
        CMP = true;

        circleCollider = gameObject.GetComponent<CircleCollider2D>();

        LineFinder = Resources.Load<GameObject>("Prefab/LineFinder");
    }

    /// <summary>
    /// 마우스 위치에 따라 오브젝트 계속 이동
    /// </summary>
    void FixedUpdate()
    {
        mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mPosition.x, mPosition.y);

        circleCollider.enabled = CMP; // CMP값에 따라 collider활성화 시킴
    }

    /// <summary>
    /// 충돌한 오브젝트가 Ground, Player, Monster, Trap이라는 태그를 달고 있고, CMP가 true면<br />
    /// 더 이상 못그리게 하고, 현제 위치에 Line_Finder오브젝트를 복제함
    /// </summary>
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