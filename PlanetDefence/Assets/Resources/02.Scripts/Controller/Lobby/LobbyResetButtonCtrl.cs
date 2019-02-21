using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyResetButtonCtrl : MonoBehaviour
{
    public GameObject ResetPopUp = null;


    public void OnClick()
    {
        ResetPopUp.SetActive(true);

    }
}
