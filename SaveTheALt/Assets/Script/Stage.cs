using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [HideInInspector] public int nPlayer; // 현제 플레이어 수

    [HideInInspector] public int stage; // 현제 스테이지

    private int[] player = new int[] { 1, 1, 2 }; // 각 스테이지 마다 있는 플레이어 수

    private Canvas over; // 게임 오버 팝업 창

    /// <summary>
    /// 각종 게임 세팅
    /// </summary>
    void Awake()
    {
        over = GameObject.Find("Setting").GetComponent<Canvas>();

        stage = 1;

        nPlayer = player[stage - 1];

        over.enabled = false;
    }

    /// <summary>
    /// 현제 플레이어 수가 0보다 작으면 게임 오버 시킴
    /// </summary>
    void Update()
    {

        if (nPlayer < 0) {
            StartCoroutine(GameOver());
        }
    }

    /// <summary>
    /// 게임 오버때 실행됨 팝업창 띄우고 시간 멈춤
    /// </summary>
    IEnumerator GameOver()
    {

        yield return new WaitForSeconds(0.4f);

        over.enabled = true;

        Debug.Log("작동함");
        Time.timeScale = 0;
    }
}
