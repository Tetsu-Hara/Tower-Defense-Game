using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endCube : MonoBehaviour {

    public float hp = 500;
    public float totalHp;
    private Slider hpSlider;
    public GameObject boomEffect;  //endCube爆炸
    public GameObject textLose; //you lose 文字
    public GameObject bud;
    public bool isGameOver=false;
    public GameObject gameoverCav;
    public GameObject winCanva;

    public Time totalTime;

    private static endCube _instance;
    public static endCube Instance  //共有的唯一的公有访问点
    {
        get
        {
            return _instance;  //返回这个类，方便外面调用
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        textLose.SetActive(false);
        winCanva.SetActive(false);
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
        gameoverCav.SetActive(false);
    }


    void Update()
    {
        hpSlider.value = hp / totalHp;
        if (hp <= 0)
        {
            Lose();

        }
        if (isGameOver)
        {
            gameoverCav.SetActive(true);
        }
       
    }

    public void Lose()
    {
        textLose.SetActive(true);
        Destroy(bud);
        isGameOver = true;
    }


    public void Win()
    {
        winCanva.SetActive(true);
        isGameOver = true;
        gameoverCav.SetActive(true);
    }

    public void OnButtonReplay()
    {
        //SceneManager.LoadScene(1);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OnButtonMenu()
    {
       
        SceneManager.LoadScene(0);
    }

}
