using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void OnCLickStage()
    {
        SoundManager.instance.PlaySFX(SoundManager.sfxClips.Select);
        SceneManager.LoadScene("Stage");

        Time.timeScale = 1;
    }

    public void OnClickEnd()
    {
        SoundManager.instance.PlaySFX(SoundManager.sfxClips.Select);
        Application.Quit();
    }

    public void OnClickOneStage()
    {
        SoundManager.instance.PlaySFX(SoundManager.sfxClips.Select);
        SceneManager.LoadScene("OneStage");
    }


    public void OnClickReStart()
    {
        SoundManager.instance.PlaySFX(SoundManager.sfxClips.Select);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1;
    }

    public void SoundShow() 
    {
        SoundManager.instance.PlaySFX(SoundManager.sfxClips.Select);
        GameObject.Find("Manager").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnClickMain()
    {
        SoundManager.instance.PlaySFX(SoundManager.sfxClips.Select);
        GameObject.Find("Manager").GetComponent<GameManager>().changeValue = true;

        SceneManager.LoadScene("Main");

        Time.timeScale = 1;
    }

    public void OnClickNextStage()
    {
        SoundManager.instance.PlaySFX(SoundManager.sfxClips.Select);
        GameObject UiClear = GameObject.Find("UI/Clear");
        UiClear.SetActive(false);
            
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            GameObject.Find("UI").transform.GetChild(2).gameObject.SetActive(true);
        }

        Time.timeScale = 1;
    }
}
