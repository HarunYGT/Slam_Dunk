using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("--- Base Level Objects ---")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Hoop;
    [SerializeField] private GameObject GrowPot;
    [SerializeField] private GameObject[] specialPos;

    [Header("--- UI ---")]
    [SerializeField] private Image[] misImage;
    [SerializeField] private Sprite misCompleteSprite;
    [SerializeField] private int scoredBall;
    int numofBaskets;

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
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if(Platform.transform.position.x > -1.70f)
            Platform.transform.position = Vector3.Lerp(Platform.transform.position,new Vector3(Platform.transform.position.x-.8f,
                Platform.transform.position.y,Platform.transform.position.z),.08f); 
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            if(Platform.transform.position.x < 1.70f)
            Platform.transform.position = Vector3.Lerp(Platform.transform.position,new Vector3(Platform.transform.position.x+.8f,
                Platform.transform.position.y,Platform.transform.position.z),.08f); 
        }
    }

    public void Basket()
    {
        numofBaskets++;
        misImage[numofBaskets-1].sprite = misCompleteSprite;
        if(numofBaskets == scoredBall)
        {
            Debug.Log("You Won!");
        }
        if(numofBaskets ==2)
        {
            Invoke("SpawnSpecial",1f);
        }
    }
    public void Lose()
    {
        Debug.Log("You Lose!");
    }
    public void PotGrow()
    {
        Hoop.transform.localScale = new Vector3(55f,55f,55f);
    }
    void SpawnSpecial()
    {
        int RandomNum = Random.Range(0,specialPos.Length-1);

        GrowPot.transform.position = specialPos[RandomNum].transform.position;
        GrowPot.SetActive(true);
    }
}
