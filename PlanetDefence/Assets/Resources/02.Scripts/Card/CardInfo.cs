using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//승완 작업 -------Start-------- 190131 
public enum CardType
{
    None,
    Normal,
    Boss,
    Event,
    Max
}

public enum MobType
{
    Normal,
    Kamikaze,
    Pirate
}

[System.Serializable]
public struct WavesMob
{
    public MobType eMobType;
    public int nMobNum;
    public float fDelayTime;
    public float fFirstDelayTime;
}

public class CardInfo : MonoBehaviour
{
    public string sCardName;
    public CardType eCardType;
    public WavesMob[] Waves;

    public void Selected()
    {
        FindObjectOfType<CardCtrl>().DeleteCards();
        GlobalGameObjectMgr.SelectedCard = this;
        GlobalGameObjectMgr.bSelected = true;
        SceneLoader.LoadScene("Battle");
        Debug.Log(GlobalGameObjectMgr.SelectedCard.sCardName);
    }
}
//승완 작업 --------End--------- 190131 