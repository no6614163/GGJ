using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CreditsPopup : UI_Popup
{
    enum Texts
    {
        Text_Title,
        Text_Settings,
        Text_Controls,
        Text_Languages,
    }

    enum Images
    {
        Image_Back,
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Image>((int)Images.Image_Back).gameObject.BindEvent(OnButtonClickedBack);

    }

    void OnButtonClickedBack(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Back");
        UI_Manager.Instance.ClosePopupUI();
    }
}
