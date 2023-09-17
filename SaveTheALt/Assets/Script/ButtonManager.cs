using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void OnCLickStage()
    {
        SceneManager.LoadScene("Stage");
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
        SceneManager.LoadScene("Main");

        Time.timeScale = 1;
    }

    public void OnClickNextStage()
    {

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            GameObject.Find("Clear").GetComponent<Canvas>().enabled = false;
            GameObject.Find("UI").transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
