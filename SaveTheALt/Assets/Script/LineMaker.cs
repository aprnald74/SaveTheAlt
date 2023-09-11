using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineMaker : MonoBehaviour
{
    // cheackOne는 한번만 그릴수 있도록 하는것
    [HideInInspector] public bool cheackOne;

    // cheachTwo는 라인 주변에 오브젝트에 있는지 확인용
    [HideInInspector] public bool cheackTwo;

    // cheackThree는 그릴수 있는지 확인용
    private bool cheackThree;


    // 라인을 저장하기 위한 변수
    private GameObject linePrefab;

    // 라인
    private LineRenderer Ir;
    private EdgeCollider2D col;
    private List<Vector2> points = new List<Vector2>();
    private List<Rigidbody2D> lines = new List<Rigidbody2D>();
    private Rigidbody2D line;

    // player
    private Rigidbody2D[] plRb;
    private float gravityScale;
    private List<GameObject> players = new List<GameObject>();



    // 게이지를 관리하기 위한 변수
    private Slider dGauge;

    // 게이지에 현재값을 위한 변수
    private float current;

    // 게이지 그 자체를 저장하기 위한 변수
    private GameObject fill;


    // 현제 몇 라운드인지 확인하기 위한 변수
    private int nStage;

    // 라운드마다 최대로 그릴수 있는 횟수를 저장하기 위한 변수
    private List<float> max = new List<float>();



    // InGame에 있는 별을 저장하기 위한 변수
    private List<SpriteRenderer> thisImg = new List<SpriteRenderer>();

    // InGame에 있는 별이 바뀔 이미지를 저장하기 위한 변수
    [SerializeField] private Sprite ChangeIcon;

    // thisImg의 별마다 사라질 수를 저장하기 위한 변수
    private List<float> star = new List<float>();

    // List에 사용할 간단한 변수
    private int num = 2;

    void Awake()
    {
        // 게임 필드에 있는 플레이어를 모두 찾고
        // 모든 플레이어의 Rigidbody를 관리할수 있게 만들고
        // 모든 플레이어의 중력값과 떨어지는 값을 0으로 만듬
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        plRb = new Rigidbody2D[players.Count];
        for (int i = 0; i < players.Count; i++)
        {
            plRb[i] = players[i].GetComponent<Rigidbody2D>();
            plRb[i].velocity = Vector2.zero;
            plRb[i].gravityScale = 0;
        }

        // 라인 프리팹 찾아옴
        linePrefab = Resources.Load<GameObject>("Prefab/Line");

        // 라인의 중력값
        line = linePrefab.GetComponent<Rigidbody2D>();

        // 슬라이더 가져옴
        dGauge = GameObject.Find("Gauge").GetComponent<Slider>();

        // 슬라이더 게이지 가져옴
        fill = GameObject.Find("Fill");

        // 요기서 라운드마다 그릴수 있는 최대를 정함
        max.AddRange(new float[] { 500, 800, 700 });

        // 바꿀 이미지 찾아옴
        Object[] sprites = Resources.LoadAll("IMG/GUI");
        ChangeIcon = sprites[26] as Sprite;

        cheackOne = true;
        cheackTwo = true;
        cheackThree = true;

    }

    void Start()
    {

        nStage = GameObject.Find("GameManager").GetComponent<Stage>().stage - 1;

        // 현재 스테이지에 최대로 그릴수 있는 수를 저장
        current = max[nStage];

        // 100이라면 Star[0]에 50 -> 100 -> 150
        // 별의 오브젝트를 찾고 그 오브젝트의 스프라이트를 thisImg에 저장함
        for (int i = 1; i < 4; i++)
        {
            star.Add((max[nStage] / 4) * i);
            thisImg.Add(GameObject.Find("Star" + (4 - i)).GetComponent<SpriteRenderer>());
        }

    }

    // 라인을 그리기 위한 코드
    void Update() {

        // 게이지에 값을 조정해서 적용시킴
        dGauge.value = current / max[nStage];

        // 게이지 값에따라 별의 이미지를 바꿈
        if (num != -1) 
            if (current <= star[num])
                thisImg[num--].sprite = ChangeIcon;

        // 게이지를 다 쓰면 Hpbar를 없애고 더이상 못그리게 함
        if (current <= 0) {
            Destroy(fill);
            cheackThree = false;
        }

        LineDrow();

    }

    void LineDrow()
    {
        // if문은 마우스를 클릭하면 마우스 위치값 list에 저장
        if (Input.GetMouseButtonDown(0) && cheackOne && cheackThree)
        {
            GameObject go = Instantiate(linePrefab);

            lines.Add(go.GetComponent<Rigidbody2D>());

            Ir = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Ir.positionCount = 1;
            Ir.SetPosition(0, points[0]);

            // 마우스 위치값에 라인 복제한거 넣음
        }
        else if (Input.GetMouseButton(0) && cheackOne && cheackTwo && cheackThree)
        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 밑에 있는 if문 방금 마우스 위치 확임 (없으면 같은 위치에 계속 라인 오브젝트 추가함)
            if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {

                // Distance함수를 활용하여 움직이는 거리만큼 게이지를 줄임
                current = current - (Vector2.Distance(points[points.Count - 1], pos) * 20);

                points.Add(pos);
                Ir.positionCount++;
                Ir.SetPosition(Ir.positionCount - 1, pos);
                col.points = points.ToArray();
            }

            // 마우스를 떼면 그리고 있던 마지막 포인터를 지우고, 플레이어와 라인에 중력을 다시 넣는다
        }
        else if (Input.GetMouseButtonUp(0))
        {
            points.Clear();

            cheackOne = false;

            foreach (Rigidbody2D line in lines)
                line.gravityScale = 1f;

            for (int i = 0; i < players.Count; i++) {
                plRb[i].gravityScale = 1f;
            }
            col.enabled = true;
        }
    }
}