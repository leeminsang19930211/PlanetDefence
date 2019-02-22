using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//승완 작업 -------Start-------- 190131
public class CardCtrl : MonoBehaviour
{
    //Cards Prefab On Contents Viewport
    public RectTransform Contents;

    private string sCardPrefabDir;
    private Object[] Cards;
    private GameObject[] CardsClone;

    [SerializeField]
    private int nCardNum = 3;

    [SerializeField]
    private int nBossDay = 1;

    [SerializeField]
    private bool nBossEnable = true;

    private bool ChoiceRandomCard(CardType eEvnetType)
    {
        // 다시 들어올때 카드위치 고정
        Contents.position = new Vector3(0, Contents.position.y, Contents.position.z);

        for (int i = 0; i < nCardNum; i++)
        {
            if (CardsClone[i])
                return false;
        }

        if (eEvnetType == CardType.Normal)
        {
            sCardPrefabDir = "03.Prefabs/Card/Normal";
        }
        else if (eEvnetType == CardType.Boss)
        {
            sCardPrefabDir = "03.Prefabs/Card/Boss";
        }

        Cards = Resources.LoadAll(sCardPrefabDir);
        int nMaxCards = Cards.Length;
        RandomInstanceCardCreate(nMaxCards);

        return true;
    }

    private void RandomInstanceCardCreate(int nMaxNum)
    {
        if (nMaxNum == 0)
        {
            Debug.Log("Cards Loading Error");
        }

        for (int i = 0; i < nCardNum; i++)
        {
            int nRandomInt = Random.Range(0, nMaxNum);
            GameObject CardPrefab = MonoBehaviour.Instantiate((GameObject)Cards[nRandomInt]);
            CardPrefab.name = "Card";
            CardPrefab.transform.SetParent(Contents);
            CardsClone[i] = CardPrefab;
        }
    }

    public void DeleteCards()
    {
        //Debug.Log("Delete");
        for (int i = 0; i < nCardNum; i++)
        {
            Destroy(CardsClone[i]);
            CardsClone[i] = null;
        }
    }

    void Awake()
    {
        CardsClone = new GameObject[nCardNum];
        CardsClone.Initialize();
    }

    void OnEnable()
    {
        if (GlobalGameObjectMgr.Inst.CurDay == 0)
        {
            Debug.Log("Day Zero Error");
        }
        else if (GlobalGameObjectMgr.Inst.CurDay > GlobalGameObjectMgr.Inst.MaxDay)
        {
            Debug.Log("Day Max Error");
        }
        else if (GlobalGameObjectMgr.Inst.CurDay % nBossDay != 0)
        {
            ChoiceRandomCard(CardType.Normal);
        }
        else if (nBossEnable && GlobalGameObjectMgr.Inst.CurDay % nBossDay == 0)
        {
            ChoiceRandomCard(CardType.Boss);
        }
    }

    void OnDisable()
    {
        //DeleteCards();
    }
}
//승완 작업 --------End--------- 190131 
