using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [HideInInspector] public bool nPlayer; // �÷��̾� �׾����� Ȯ��

    [HideInInspector] public int stage; // ���� ��������

    private Canvas over; // ���� ���� �˾� â

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    void Awake()
    {
        over = GameObject.Find("Setting").GetComponent<Canvas>();

        stage = 1;

        over.enabled = false;

        nPlayer = false;
    }

    /// <summary>
    /// ���� �÷��̾ ������ ���� ���� ��Ŵ
    /// </summary>
    void Update()
    {

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
}
