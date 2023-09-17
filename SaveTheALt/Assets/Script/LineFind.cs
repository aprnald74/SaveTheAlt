using UnityEngine;

public class LineFind : MonoBehaviour
{

    private Vector3 mPosition;
    
    private float rotateDegree;

    private float distance;


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

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject.Find("GameManager").GetComponent<LineMaker>().cheackTwo = true;
        GameObject.Find("Mouse").GetComponent<Mouse>().CMP = true;

        Destroy(gameObject);
    }
}