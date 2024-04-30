using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using JetBrains.Annotations;
using System.Collections;

public class SceneDirector : MonoBehaviour
{
    [SerializeField] CardsDirector cardsDirector;
    [SerializeField] Text textTimer;
    [SerializeField] Text textAnswer;
    [SerializeField] List<GameObject> cardsNumber1;
    [SerializeField] List<GameObject> cardsNumber2;

    //SE
    [SerializeField] AudioClip correctSE;
    [SerializeField] AudioClip mistakeSE;
    [SerializeField] AudioClip turnCardSE;
    AudioSource audioSource;

    public string sceneName;

    List<CardController> cards;
    List<CardController> selectCards;
    int width = 3;
    int height = 2;
    int selectCountMax = 2;
    bool isGameEnd;
    float gameTimer;
    int oldSecond;
    int targetSum = 4;
    private GameObject scoreText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cards = cardsDirector.GetMemoryCards();
        Vector2 offset = new Vector2((width - 1) / 2.0f, (height + 0.2f) / 2.0f);
        selectCards = new List<CardController>();
        oldSecond = -1;
        scoreText = GameObject.Find("ScoreText");

        if (cards.Count < selectCountMax)
        {
            Debug.Log("カードが足りません");
        }

        for (int i = 0; i < width * height; i++)
        {
            float x = (i % width - offset.x) * CardController.Width;
            float y = (i / width - offset.y) * CardController.Height;

            cards[i].transform.position = new Vector3(x, 0, y);
            cards[i].FlipCard(false);
        }

        foreach (var card in cards)
        {
            card.FlipCard(false);
        }
    }

    void Update()
    {
        if (isGameEnd) return;

        gameTimer += Time.deltaTime;
        textTimer.text = getTimerText(gameTimer);

        //マウスを離したとき
        if (Input.GetMouseButtonUp(0))
        {
            if (!canOpen()) return;
            //Rayを飛ばして当たり判定をとる
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //ヒットしたゲームオブジェクトからCardControllerを取得
                CardController card = hit.collider.gameObject.GetComponent<CardController>();
                audioSource.PlayOneShot(turnCardSE);
                //カードじゃないか、もうめくったカードなら終了
                if (!card || selectCards.Contains(card)) return;
                //カードオープン
                card.FlipCard();
                //選択したカードを保存
                selectCards.Add(card);
            }
        }
    }

    string getTimerText(float timer)
    {
        int sec = (int)timer % 60;
        string ret = textTimer.text;

        if (oldSecond != sec)
        {
            int min = (int)timer / 60;
            string pmin = string.Format("{0:D2}", min);
            string psec = string.Format("{0:D2}", sec);

            ret = pmin + ":" + psec;
            oldSecond = sec;
        }

        return ret;
    }

    public bool canOpen()
    {
        if (selectCards.Count < selectCountMax) return true;

        int sum = 0;
        foreach (var card in selectCards)
        {
            sum += card.No;
        }

        if (sum == targetSum)
        {
            foreach (var item in selectCards)
            {
                item.gameObject.SetActive(false);
            }
            scoreText.GetComponent<ScoreManager>().score = scoreText.GetComponent<ScoreManager>().score + 300;
            audioSource.PlayOneShot(correctSE);
        }
        else
        {
            foreach (var item in selectCards)
            {
                item.FlipCard(false);
            }
            audioSource.PlayOneShot(mistakeSE);
        }

        selectCards.Clear();

        isGameEnd = true;
        foreach (var item in cards)
        {
            if (item.gameObject.activeSelf)
            {
                isGameEnd = false;
                break;
            }
        }

        if (isGameEnd)
        {
            textTimer.text = getTimerText(gameTimer);
            SceneManager.LoadScene(sceneName);
        }

        return true;


    }
}
