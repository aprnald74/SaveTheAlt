using UnityEngine;

public class LineFind : MonoBehaviour
{

    private Vector3 mPosition; // 마우스 좌표
    
    private float rotateDegree; // 오브젝트 회전

    private float distance; // 오브젝트와 마우스의 거리 

    // 수학 공식을 활용하여 LineFinde오브젝트가 마우스 위치에 따라 다니면서 움직이게 함


    /// <summary>
    /// 마우스 위치와 오브젝트간의 거리를 구한다음 그 거리만큼 오브젝트를 늘리고 줄임
    /// </summary>
    void Update()
    {

        mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 oPosition = transform.position;
        Vector3 target = mPosition - transform.position;

        distance = (float)Mathf.Sqrt(Mathf.Pow(oPosition.x - mPosition.x, 2) + Mathf.Pow(oPosition.y - mPosition.y, 2));
        transform.localScale = new Vector2(distance, 1);

        rotateDegree = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(rotateDegree, Vector3.forward);

    }

    /// <summary>
    /// 이 오브젝트가 충돌에서 벗어나면 다시 그리고 이 오브젝트를 삭제함
    /// </summary>
    void OnTriggerExit2D(Collider2D col)
    {
        GameObject.Find("GameManager").GetComponent<LineMaker>().cheackTwo = true;
        GameObject.Find("Mouse").GetComponent<Mouse>().CMP = true;

        Destroy(gameObject);
    }
}