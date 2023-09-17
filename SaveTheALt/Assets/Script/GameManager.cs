using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private static int StageType = 5;

    private List<GameObject> stars = new List<GameObject>();

    public List<Canvas> stageType = new List<Canvas>();

    public bool changeValue = false;

    public int nStage;

    public int clearStars;

    public int clearStage = 1;

    private void Awake() 
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        for(int i = 0; i <= StageType; i++) {
            stageType.Add(GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + (i + 1) + "/Panel").GetComponent<Canvas>());
        }

    }

    private void Start() {
        List<List<int>> stage = new List<List<int>>() {
            new List<int> {-1},
            new List<int> {-1},
            new List<int> {-1}, 
            new List<int> {-1},
            new List<int> {-1},
        };
    }

    private void FixedUpdate() {
        if(changeValue && SceneManager.GetActiveScene().buildIndex == 1) {
           for(int i = 1; i <= StageType; i++) {
                if (clearStage > i) {
                    stageType[i].enabled = false;
                }
            }

        for(int i = 0; i < 3; i++) {
            stars.Add(GameObject.Find("UI/Canvas/ScrollView/Viewport/Content/Stage" + (i + 1) + "/GUI_58/Star/ClearStar" + (i + 1)));
        }

        for (int i = 2; i >= 0; i--) {
            if(clearStage + i >= 2) {
                //stars[i].GetComponent<SpriteRenderer>().sprite = img;
            }
        }


            changeValue = false;
        }
    }



}
