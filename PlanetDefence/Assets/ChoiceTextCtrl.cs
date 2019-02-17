using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceTextCtrl : MonoBehaviour
{
    [SerializeField]
    private Text DaysText;

    void UpdateDayText()
    {
        DaysText.text = GlobalGameObjectMgr.Inst.CurDay.ToString() + "/" + GlobalGameObjectMgr.Inst.MaxDay.ToString();
    }

    void OnEnable()
    {
        UpdateDayText();
    }
}
