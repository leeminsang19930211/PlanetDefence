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

//승완 작업 -------Start-------- 190208 
public enum MobType
{
    Normal,
    Kamikaze,
    Pirate,
    BattleShip,
    DummyShip,
    BionicShip,
    StealthShip,
    Little,
    Meteor
}
//승완 작업 --------End--------- 190208 

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
        //승완 작업 -------Start-------- 190208 
        FindObjectOfType<CardCtrl>().DeleteCards();
        GlobalGameObjectMgr.Inst.SelectedCard = this;
        GlobalGameObjectMgr.Inst.bSelected = true;
        SceneLoader.LoadScene("Battle");
        //Debug.Log(GlobalGameObjectMgr.Inst.SelectedCard.sCardName);
        //승완 작업 --------End--------- 190208 
    }
}
//승완 작업 --------End--------- 190131 