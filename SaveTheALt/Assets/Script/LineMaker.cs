using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class LineMaker : MonoBehaviour
{
    [HideInInspector] public bool cheackOne;
    [HideInInspector] public bool cheackTwo;
    private bool cheackThree;
    private GameObject linePrefab;

    private LineRenderer Ir;
    private EdgeCollider2D col;
    private List<Vector2> points = new List<Vector2>();
    private List<Rigidbody2D> lines = new List<Rigidbody2D>();
    private Rigidbody2D line;

    // player
    private Rigidbody2D[] plRb;
    private List<GameObject> players = new List<GameObject>();


    private Slider dGauge;

    private float current;

    private GameObject fill;


    private int nStage;

    private List<float> max = new List<float>();


    private List<SpriteRenderer> thisImg = new List<SpriteRenderer>();

    [SerializeField] private Sprite ChangeIcon;

    private List<float> star = new List<float>();

    public int num = 2;

    void Awake()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        plRb = new Rigidbody2D[players.Count];
        for (int i = 0; i < players.Count; i++)
        {
            plRb[i] = players[i].GetComponent<Rigidbody2D>();
            plRb[i].velocity = Vector2.zero;
            plRb[i].gravityScale = 0;
        }

        linePrefab = Resources.Load<GameObject>("Prefab/Line");

        line = linePrefab.GetComponent<Rigidbody2D>();

        dGauge = GameObject.Find("Gauge").GetComponent<Slider>();

        fill = GameObject.Find("Fill");

        max.AddRange(new float[] { 500, 800, 700 });

        Object[] sprites = Resources.LoadAll("IMG/GUI");
        ChangeIcon = sprites[26] as Sprite;

        cheackOne = true;
        cheackTwo = true;
        cheackThree = true;

    }

    void Start()
    {

        nStage = GameObject.Find("GameManager").GetComponent<NowStage>().stage - 1;

        current = max[nStage];

        for (int i = 1; i < 4; i++) {
            star.Add((max[nStage] / 4) * i);
            thisImg.Add(GameObject.Find("Map/Star/Star" + (4 - i)).GetComponent<SpriteRenderer>());
        }

    }

    void Update() {

        dGauge.value = current / max[nStage];

        if (num != -1) 
            if (current <= star[num]) 
                thisImg[num--].sprite = ChangeIcon;
        if (current <= 0) {
            Destroy(fill);
            cheackThree = false;
        }

        LineDrow();

    }

    void LineDrow()
    {
        if (Input.GetMouseButtonDown(0) && cheackOne && cheackThree)
        {
            GameObject go = Instantiate(linePrefab);

            lines.Add(go.GetComponent<Rigidbody2D>());

            Ir = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Ir.positionCount = 1;
            Ir.SetPosition(0, points[0]);

        }
        else if (Input.GetMouseButton(0) && cheackOne && cheackTwo && cheackThree)
        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {

                current = current - (Vector2.Distance(points[points.Count - 1], pos) * 20);

                points.Add(pos);
                Ir.positionCount++;
                Ir.SetPosition(Ir.positionCount - 1, pos);
                col.points = points.ToArray();
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            points.Clear();

            cheackOne = false;

            GameObject.Find("GameManager").GetComponent<NowStage>().gameStart = true;

            foreach (Rigidbody2D line in lines)
                line.gravityScale = 1f;

            for (int i = 0; i < players.Count; i++) {
                plRb[i].gravityScale = 1f;
            }
            col.enabled = true;
        }
    }
}