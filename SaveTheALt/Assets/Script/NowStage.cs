using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NowStage : MonoBehaviour
{
    public bool nPlayer;
    public int stage;
    public bool gameStart;
    public GameObject over;
    public GameObject clear;
    private Sprite img;
    public List<GameObject> stars = new List<GameObject>();
    private int count;


    void Awake()
    {

        stage = SceneManager.GetActiveScene().buildIndex - 1;

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

        nPlayer = true;
    }


    void Update()
    {

        if (gameStart)
            StartCoroutine(GameStart());

        if (!nPlayer) 
            StartCoroutine(GameOver());
        
    }

    IEnumerator GameOver()
    {

        yield return new WaitForSeconds(0.4f);

        SoundManager.instance.PlaySFX(SoundManager.sfxClips.GameLose);

        over.SetActive(true);

        Time.timeScale = 0;
    }

    IEnumerator GameStart()
    {

        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlaySFX(SoundManager.sfxClips.GameWin);

        gameStart = false;

        GameManager gameManager;
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

        gameManager.clearStage = SceneManager.GetActiveScene().buildIndex - 1;

        count = GameObject.Find("GameManager").GetComponent<LineMaker>().num; 

         for (int i = 0; i < 3; i++) {
            if (count + i >= 2) {
                gameManager.Stars[((stage - 1) * 3) + i] = 1;
            } else {
                gameManager.Stars[((stage - 1) * 3) + i] = 0;
            }
            
        }

        clear.SetActive(true);

        for (int i = 2; i > -1; i--) {
            stars[i].SetActive(true);
            if (count + i >= 2) {
                stars[i].GetComponent<SpriteRenderer>().sprite = img;
            }
        }

        gameManager.changeValue = true;

        Time.timeScale = 0;
    }
}