using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void OnCLickStage()
    {
        SceneManager.LoadScene("Stage");

        Time.timeScale = 1;
    }

    public void OnClickEnd()
    {
        Application.Quit();
    }


    public void OnClickOneStage()
    {
        SceneManager.LoadScene("OneStage");
    }


    public void OnClickReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1;

    }

    public void OnClickMain()
    {
        GameObject.Find("GameSystem").GetComponent<GameManager>().changeValue = true;

        SceneManager.LoadScene("Main");

        Time.timeScale = 1;
    }

    public void OnClickNextStage()
    {
        GameObject UiClear = GameObject.Find("UI/Clear");
        GameObject.Find("UI/Clear").SetActive(false);
            
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            GameObject.Find("UI").transform.GetChild(3).gameObject.SetActive(true);
        }

        Time.timeScale = 1;
    }
}
