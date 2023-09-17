using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowStage : MonoBehaviour
{
    [HideInInspector] public bool nPlayer;

    [HideInInspector] public int stage;

    public bool gameStart;

    public Canvas over;

    public Canvas clear;

    private Sprite img;

    public List<GameObject> stars = new List<GameObject>();

    private int count;


    void Awake()
    {

        stage = 1;

        over = GameObject.Find("UI/End").GetComponent<Canvas>();

        clear = GameObject.Find("UI/Clear").GetComponent<Canvas>();

        Object[] sprites = Resources.LoadAll("IMG/GUI");
        img = sprites[25] as Sprite;

        for(int i = 0; i < 3; i++) {
            stars.Add(GameObject.Find("UI/Clear/Star/ClearStar" + (i + 1)));
            stars[i].SetActive(false);
        }

        over.enabled = false;

        clear.enabled = false;

        nPlayer = false;
    }


    void Update()
    {

        if (gameStart)
            StartCoroutine(GameStart());

        if (nPlayer) 
            StartCoroutine(GameOver());
        
    }

    IEnumerator GameOver()
    {

        yield return new WaitForSeconds(0.4f);

        over.enabled = true;

        Time.timeScale = 0;
    }

    IEnumerator GameStart()
    {

        yield return new WaitForSeconds(5.0f);

        GameObject.Find("GameSystem").GetComponent<GameManager>().clearStage++;

        GameObject.Find("GameSystem").GetComponent<GameManager>().nStage = stage;

        count = GameObject.Find("GameManager").GetComponent<LineMaker>().num; 

        GameObject.Find("GameSystem").GetComponent<GameManager>().clearStage = count;

        GameObject.Find("GameSystem").GetComponent<GameManager>().changeValue = true;

        clear.enabled = true;

        for (int i = 2; i >= 0; i--) {
            stars[i].SetActive(true);
            if(count + i >= 2) {
                stars[i].GetComponent<SpriteRenderer>().sprite = img;
            }
        }

        Time.timeScale = 0;

    }
}