using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("MainGameScene"); 
    }

    public void HutuBtn()
    {
        SceneManager.LoadScene("Hutuu");
    }
    public void  MuzuBtn()
    {
        SceneManager.LoadScene("Muzu");
    }
}
