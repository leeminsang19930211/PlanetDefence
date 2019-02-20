using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class SpcDropPopUpPanelCtrl : MonoBehaviour
{
    public Sprite[] m_sprites = new Sprite[(int)SpaceShipPart.End];
    public Image m_partImg = null;

    public void OnClick()
    {
        gameObject.SetActive(false);
    }

    public void PopUp(SpaceShipPart part)
    {
        if (part == SpaceShipPart.End)
            return;

        m_partImg.sprite = m_sprites[(int)part];

        gameObject.SetActive(true);
    }
}
