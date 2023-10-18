using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private static int StageNum = 5;
    public List<int> Stars;
    public List<GameObject> stageType;
    public bool changeValue;
    public int clearStage;
    public Sprite img;
    public Sprite initialSprite;
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

        initialSprite = sprites[26] as Sprite;

        for (int i = 0; i < StageNum * 3; i++) {
            Stars.Add(0);
        }
    }
    
    private void FixedUpdate() {
        if(changeValue && SceneManager.GetActiveScene().buildIndex == 1) {

            changeValue = false;
        
            for(int i = 1; i < StageNum; i++) {
                 stageType.Add(GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + (i + 1) + "/Panel"));
                 if (clearStage >= i) 
                    stageType[i - 1].SetActive(false);
            }

            starIndex = 0;
            for (int i = 0; i < StageNum; i++) {
                for (int j = 0; j < 3; j++) {
                    GameObject Star = GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + (i + 1) + "/GUI_58/Star/ClearStar" + (j + 1));
                    if (Stars[starIndex++] == 1) {
                        Star.GetComponent<SpriteRenderer>().sprite = img;
                    } else {
                        Star.GetComponent<SpriteRenderer>().sprite = initialSprite;
                    }
                }
            }

            stageType = new List<GameObject>();
        }
    }
}