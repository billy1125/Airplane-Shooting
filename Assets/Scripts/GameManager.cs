using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyAirplanePrefab; //戰機預置物件
    public float span = 1.0f;
    public float delta = 0;

    int intScore = 0;
    public Text ScoreText;

    public GameObject AirPlanePrefab; //戰機預置物件
    public GameObject StartPoint; //戰機產生的起點

    public GameObject AirPlane; // 戰機物件

    bool IsRestarting = false; //遊戲是不是正在重啟中？True代表遊戲正在重啟，False代表遊戲正常執行中

    int LifeAmount = 2;
    public GameObject[] LifeImage; //宣告一個公開的生命值物件陣列

    public GameObject GameUI; // 遊戲結束畫面

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "分數：0"; //初始化分數表
        InitialAirPlane(); //初始化戰機
    }

    // Update is called once per frame
    void Update()
    {
        //以下我們參考之前做的貓咪忍者遊戲，想想看如何拿箭頭產生的程式，修改成為戰機的產生程式
        this.delta += Time.deltaTime;
        if (this.delta > this.span && IsRestarting == false)
        {
            this.delta = 0;
            float px = Random.Range(-3.0f, 3.0f); // 這次我們產生的是-3到3之間的浮點數
            Instantiate(EnemyAirplanePrefab, new Vector3(px, 7, 0), Quaternion.identity);
        }

        //修正：我們多加一個條件，只有在「戰機被刪除」和「遊戲正常執行中」的兩個條件都成立之下，才開始遊戲重啟
        //避免因為不斷Update，所以不斷產生戰機，最後造成遊戲崩潰...
        //同時也設計一個緩衝時間，五秒內都不會有怪物產生，避免還有怪物時就產生戰機
        if (AirPlane == null && IsRestarting == false && LifeAmount >= 0)
        {
            LifeImage[LifeAmount].SetActive(false); //以生命值當成陣列位置指標，將生命值物件陣列消失
            LifeAmount -= 1; //減掉一個生命值

            IsRestarting = true; //設定「遊戲正在重啟」的狀態
            StartCoroutine(StartGame()); //開始重啟遊戲
        }

        if (LifeAmount < 0)
            GameUI.SetActive(true);
    }

    // 加分的方法（同學要複習一下C#函式與怎麼設定函式參數）
    public void IncreaseScore(int _score)
    {
        intScore += _score;
        ScoreText.text = "分數：" + intScore;
    }

    //初始化戰機的方法
    public void InitialAirPlane()
    {
        intScore = 0; //分數歸零
        IncreaseScore(intScore); //重新顯示分數
        AirPlane = Instantiate(AirPlanePrefab, StartPoint.transform.position, StartPoint.transform.rotation);
        IsRestarting = false;
    }

    // 一個 IEnumerator 介面，用來設定一個重啟遊戲的計時器
    IEnumerator StartGame()
    {
        if (AirPlanePrefab == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(5); //計時五秒後（也就是遊戲五秒之後，要執行下面的方法）

        if (LifeAmount == -1) //如果現在生命值已經是-1，就直接跳出計時器
        {
            yield break;
        }
        else
        {
            InitialAirPlane(); //五秒後，要初始化戰機
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Intro");
    }
}
