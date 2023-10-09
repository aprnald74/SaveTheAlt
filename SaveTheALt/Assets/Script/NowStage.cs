using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NowStage : MonoBehaviour
{
    [HideInInspector] public bool nPlayer;

    [HideInInspector] public int stage;

    public bool gameStart;

    public GameObject over;

    public GameObject clear;

    private Sprite img;

    public List<GameObject> stars = new List<GameObject>();
    
    private int count;


    void Awake()
    {

        stage = 1;

        over = GameObject.Find("UI/End");

        clear = GameObject.Find("UI/Clear");

        Object[] sprites = Resources.LoadAll("IMG/GUI");
        img = sprites[25] as Sprite;

        for(int i = 0; i < 3; i++) {
            stars.Add(GameObject.Find("UI/Clear/Star/ClearStar" + (i + 1)));
            stars[i].SetActive(false);
        }

        over.SetActive(false);

        clear.SetActive(false);

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

        over.SetActive(true);

        Time.timeScale = 0;
    }

    IEnumerator GameStart()
    {

        yield return new WaitForSeconds(5.0f);

        GameObject.Find("GameSystem").GetComponent<GameManager>().clearStage = SceneManager.GetActiveScene().buildIndex - 1;

        GameObject.Find("GameSystem").GetComponent<GameManager>().nStage = stage;

        count = GameObject.Find("GameManager").GetComponent<LineMaker>().num; 

        GameObject.Find("GameSystem").GetComponent<GameManager>().clearStars = count;

        GameObject.Find("GameSystem").GetComponent<GameManager>().changeValue = true;

        clear.SetActive(true);

        gameStart = false;

        for (int i = 2; i > -1; i--) {
            stars[i].SetActive(true);
            if(count + i >= 2) {
                stars[i].GetComponent<SpriteRenderer>().sprite = img;
            }
        }

        Time.timeScale = 0;

    }
}