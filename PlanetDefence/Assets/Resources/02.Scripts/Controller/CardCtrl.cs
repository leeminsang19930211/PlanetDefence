using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCtrl : MonoBehaviour
{
    //Cards Prefab On Contents Viewport
    public Transform Contents;

    private string sCardPrefabDir;
    private Object[] Cards;
    private GameObject[] CardsClone;

    private Ray ray;
    private RaycastHit hit;

    [SerializeField]
    private int nCardNum = 3;

    private bool ChoiceRandomCard(CardType eEvnetType)
    {
        for (int i = 0; i < nCardNum; i++)
        {
            if (CardsClone[i])
                return false;
        }

        if (eEvnetType == CardType.Normal)
        {
            sCardPrefabDir = "03.Prefabs/Card/Normal";
        }
        else if(eEvnetType == CardType.Boss)
        {
            sCardPrefabDir = "03.Prefabs/Card/Boss";
        }
        else if(eEvnetType == CardType.Event)
        {
            sCardPrefabDir = "03.Prefabs/Card/Event";
        }

        Cards = Resources.LoadAll(sCardPrefabDir);
        int nMaxCards = Cards.Length;
        RandomInstanceCardCreate(nMaxCards);

        return true;
    }

    private void RandomInstanceCardCreate(int nMaxNum)
    {
        for (int i = 0; i< nCardNum; i++)
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
        for (int i = 0; i < nCardNum; i++)
        {
            Destroy(CardsClone[i]);
            CardsClone[i] = null;
        }
    }

    void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChoiceRandomCard(CardType.Normal);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DeleteCards();
        }
    }
    
    void Awake()
    {
        CardsClone = new GameObject[nCardNum];
        CardsClone.Initialize();
    }

    void OnEnable()
    {
        ChoiceRandomCard(CardType.Normal);
    }

    void OnDisable()
    {
        //DeleteCards();
    }

}
