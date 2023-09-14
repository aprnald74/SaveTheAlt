using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [HideInInspector] public bool nPlayer; // �÷��̾� �׾����� Ȯ��

    [HideInInspector] public int stage; // ���� ��������

    public bool gameStart;

    private Canvas over; // ���� ���� �˾� â

    private Canvas setting;

    private Canvas clear;

    


    /// <summary>
    /// ���� ���� ����
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
    /// ���� �÷��̾ ������ ���� ���� ��Ŵ
    /// </summary>
    void Update()
    {

        if (gameStart)
            StartCoroutine(GameStart());

        if (nPlayer) 
            StartCoroutine(GameOver());
        
    }

    /// <summary>
    /// ���� ������ ����� �˾�â ���� �ð� ����
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