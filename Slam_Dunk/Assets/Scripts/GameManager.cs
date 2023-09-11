using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("--- Base Level Objects ---")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Hoop;
    [SerializeField] private GameObject GrowPot;
    [SerializeField] private GameObject[] specialPos;
    [SerializeField] private AudioSource[] Sounds;
    [SerializeField] private ParticleSystem[] Effects;
    SceneManager scene;

    [Header("--- UI ---")]
    [SerializeField] private Image[] misImage;
    [SerializeField] private Sprite misCompleteSprite;
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private int scoredBall;
    [SerializeField] private GameObject[] Panels;
    int numofBaskets;
    float fingerPosX;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this); 
        }else
        {
            Instance = this;
        }
    }
    void Start() 
    {
        numofBaskets = 0;
        for (int i = 0; i < scoredBall; i++)
        {
            misImage[i].gameObject.SetActive(true); 
        }
        Invoke("SpawnSpecial",3f);
        levelName.text = "Level : " + SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if(Time.timeScale != 0)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10));

                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        fingerPosX = TouchPosition.x - Platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if(TouchPosition.x - fingerPosX > -1.70f && TouchPosition.x-fingerPosX < 1.70f)
                        {
                            Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(TouchPosition.x - fingerPosX,
                                Platform.transform.position.y, Platform.transform.position.z), 8f);
                        }
                        break;
                }
            }
        } 
    }
    public void Basket(Vector3 Poz)
    {
        numofBaskets++;
        misImage[numofBaskets-1].sprite = misCompleteSprite;
        Sounds[3].Play();
        Effects[0].transform.position = Poz;
        Effects[0].gameObject.SetActive(true);
        if(numofBaskets == scoredBall)
            Won();   
        if(numofBaskets ==2)
            Invoke("SpawnSpecial",1f);
    }
    public void Won()
    {
        Sounds[0].Play();
        Panels[1].SetActive(true);
        PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
        Time.timeScale = 0f;
    }
    public void Lose()
    {
        Debug.Log("You Lose!");
        Sounds[2].Play();
        Panels[2].SetActive(true);
        Time.timeScale =0f;
    }
    public void PotGrow(Vector3 Poz)
    {
        Sounds[1].Play();
        Effects[1].transform.position = Poz;
        Effects[1].gameObject.SetActive(true);
        Hoop.transform.localScale = new Vector3(55f,55f,55f);
    }
    void SpawnSpecial()
    {
        int RandomNum = Random.Range(0,specialPos.Length-1);

        GrowPot.transform.position = specialPos[RandomNum].transform.position;
        GrowPot.SetActive(true);
        
    }

    public void ButtonOperations(string deger)
    {
        switch(deger)
        {
            case "Pause":
                Time.timeScale =0f;
                Panels[0].SetActive(true);
                break;
            case "Resume":
                Time.timeScale = 1f;
                Panels[0].SetActive(false);
                break;
            case "TryAgain":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;    
                break;
            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                Time.timeScale = 1f;
                break;
            case "Settings":
                //Settings Panel
                break;
            case "Quit":
                Application.Quit();
                break;
            
        }
    }
}
