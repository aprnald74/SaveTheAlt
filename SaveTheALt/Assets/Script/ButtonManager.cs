using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    private Canvas setting;

    private void Start()
    {
        setting = GameObject.Find("Setting").GetComponent<Canvas>();
    }

    public void OnClickStage()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickEnd()
    {
        Application.Quit();
    }

    public void OnClickReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("Main");
    }
}
