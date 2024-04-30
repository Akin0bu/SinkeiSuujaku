using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControll : MonoBehaviour
{

    //タイトルに戻るボタン
    public void HowtoTitleBtn()
    {
        SceneManager.LoadScene("Title");
    }

    //あそびかたへのボタン
    public void HowtoBtn()
    {
        SceneManager.LoadScene("Howto");
    }

    //簡単へのボタン
    public void StartBtn()
    {
        SceneManager.LoadScene("MainGameScene"); 
    }
    
    //普通へのボタン
    public void HutuBtn()
    {
        SceneManager.LoadScene("Hutuu");
    }

    //難しいへのボタン
    public void  MuzuBtn()
    {
        SceneManager.LoadScene("Muzu");
    }

    //前のシーンに戻るボタン
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //ゲーム終了のボタン
    public void Quit()
    {
        Application.Quit();
    }
}