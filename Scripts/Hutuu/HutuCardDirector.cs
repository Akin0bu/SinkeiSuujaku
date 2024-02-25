using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutuuCardsDirector : MonoBehaviour
{
    [SerializeField] List<GameObject> HutuuprefabSpades;
    [SerializeField] List<GameObject> HutuuprefabClubs;
    [SerializeField] List<GameObject> HutuuprefabDiamonds;
    [SerializeField] List<GameObject> HutuuprefabHearts;
    [SerializeField] List<GameObject> HutuuprefabJokers;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //神経衰弱のカードを返す
    public List<CardController> GetMemoryCards()
    {
        List<CardController> ret = new List<CardController>();

        //使うカード種類
        ret.AddRange(HutuucreateCards(SuitType.Spade, 6));
        ret.AddRange(HutuucreateCards(SuitType.Diamond, 6));

        HutuuShuffleCards(ret);

        return ret;
    }
    //シャッフル
    public void HutuuShuffleCards(List<CardController> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int rnd = Random.Range(0, cards.Count);
            CardController tmp = cards[i];

            cards[i] = cards[rnd];
            cards[rnd] = tmp;
        }
    }

    //カード作成
    List<CardController> HutuucreateCards(SuitType suittype, int count = -1)
    {
        List<CardController> ret = new List<CardController>();

        //カード種類
        List<GameObject> prefabcards = HutuuprefabSpades;
        Color suitcolor = Color.black;

        if (SuitType.Club == suittype)
        {
            prefabcards = HutuuprefabClubs;
        }
        else if (SuitType.Diamond == suittype)
        {
            prefabcards = HutuuprefabDiamonds;
            suitcolor = Color.red;
        }
        else if (SuitType.Heart == suittype)
        {
            prefabcards = HutuuprefabHearts;
            suitcolor = Color.red;
        }
        else if (SuitType.Joker == suittype)
        {
            prefabcards = HutuuprefabJokers;
        }

        //枚数に指定がなければ全てのカードを作成する
        if (0 > count)
        {
            count = prefabcards.Count;
        }


        //カード生成
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefabcards[i]);

            //当たり判定
            BoxCollider bc = obj.AddComponent<BoxCollider>();
            //当たり判定検知
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            //カード同士の当たり判定と物理演算を使わない
            bc.isTrigger = true;
            rb.isKinematic = true;

            //カードにデータをセット
            CardController ctrl = obj.AddComponent<CardController>();

            ctrl.Suit = suittype;
            ctrl.SuitColor = suitcolor;
            ctrl.No = i + 1;

            ret.Add(ctrl);
        }
        return ret;
    }
}

