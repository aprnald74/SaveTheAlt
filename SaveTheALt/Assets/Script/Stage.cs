using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [HideInInspector] public bool nPlayer; // 플레이어 죽었는지 확인

    [HideInInspector] public int stage; // 현제 스테이지

    public bool gameStart;

    private Canvas over; // 게임 오버 팝업 창

    private Canvas setting;

    private Canvas clear;

    


    /// <summary>
    /// 각종 게임 세팅
    /// </summary>
    void Awake()
    {
        over = GameObject.Find("End").GetComponent<Canvas>();

        setting = GameObject.Find("Setting").GetComponent<Canvas>();

        clear = GameObject.Find("Clear").GetComponent<Canvas>();

        stage = 1;

        over.enabled = false;

        setting.enabled = false;

        clear.enabled = false;

        nPlayer = false;
    }

    /// <summary>
    /// 현제 플레이어가 죽으면 게임 오버 시킴
    /// </summary>
    void Update()
    {

        if (gameStart)
            StartCoroutine(GameStart());

        if (nPlayer) 
            StartCoroutine(GameOver());
        
    }

    /// <summary>
    /// 게임 오버때 실행됨 팝업창 띄우고 시간 멈춤
    /// </summary>
    IEnumerator GameOver()
    {

        yield return new WaitForSeconds(0.4f);

        over.enabled = true;

        Time.timeScale = 0;
    }

    IEnumerator GameStart()
    {

        yield return new WaitForSeconds(10.0f);

        clear.enabled = true;

        Time.timeScale = 0;

    }
}