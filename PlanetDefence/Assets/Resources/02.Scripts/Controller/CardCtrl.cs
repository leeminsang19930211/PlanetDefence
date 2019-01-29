using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCtrl : MonoBehaviour
{
    //Cards Prefab On Contents Viewport
    public Transform Contents;

    private string sCardPrefabDir;
    Object[] Cards;

    Ray ray;
    RaycastHit hit;

    void ChoiceRandomCard(CardType eEvnetType)
    {
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
    }

    void RandomInstanceCardCreate(int nCardNum)
    {
        for(int i = 0; i< 3; i++)
        {
            int nRandomInt = Random.Range(0, nCardNum);
            GameObject CardPrefab = MonoBehaviour.Instantiate((GameObject)Cards[nRandomInt]);
            CardPrefab.name = "Card";
            CardPrefab.transform.SetParent(Contents);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ChoiceRandomCard(CardType.Normal);
        }
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 10.0f))
            {
                if (hit.collider.tag == "Card")
                {
                    Debug.Log("Load");
                }
            }
        }
    }

}
