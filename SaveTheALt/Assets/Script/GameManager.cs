using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private static int StageType = 5;

    public List<GameObject> starsGame;

    public List<int> Stars;

    public List<GameObject> stageType;

    public bool changeValue;

    public int nStage;

    public int clearStars;

    public int clearStage;

    public Sprite img;

    int starIndex;

    private void Awake() 
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        Object[] sprites = Resources.LoadAll("IMG/GUI");
        img = sprites[25] as Sprite;

        for (int i = 0; i < StageType * 3; i++) {
            Stars.Add(0);
        }
    }

    private void FixedUpdate() {
        if(changeValue && SceneManager.GetActiveScene().buildIndex == 1) {

            changeValue = false;
        
            for(int i = 1; i < StageType; i++) {
                 stageType.Add(GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + (i + 1) + "/Panel"));
                 if (clearStage >= i) 
                    stageType[i - 1].SetActive(false);
            }

            starIndex = 0;
            for (int i = 0; i < StageType; i++) {
                for (int j = 0; j < 3; j++) {
                    GameObject Star = GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + (i + 1) + "/GUI_58/Star/ClearStar" + (j + 1));
                    if (Stars[starIndex] == 1) {
                        Star.GetComponent<SpriteRenderer>().sprite = img;
                        starIndex++;    
                    }
                }
            }

            if (clearStage != -5) {
              for(int i = 0; i < 3; i++) {
                    starsGame.Add(GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + clearStage + "/GUI_58/Star/ClearStar" + (i + 1)));
                    if (clearStars + i >= 2) {
                        starsGame[i].GetComponent<SpriteRenderer>().sprite = img;
                    }
                }
                clearStage = -5;
            }


            starIndex = 0;
            for (int i = 0; i < StageType; i++) {
                for (int j = 0; j < 3; j++) {
                    GameObject Star = GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + (i + 1) + "/GUI_58/Star/ClearStar" + (j + 1));
                    if (Star.GetComponent<SpriteRenderer>().sprite == img) {
                        Stars[starIndex++] = 1;
                    } else {
                        Stars[starIndex++] = 0;
                    }
                }
            }


            starsGame = new List<GameObject>();
            stageType = new List<GameObject>();

        }
    }
}