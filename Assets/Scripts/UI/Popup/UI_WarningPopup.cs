using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_WarningPopup : UI_Popup
{
    enum Texts
    {
        Text_Back,
        Text_Content,
    }

    enum Images
    {
        Image_Back,
    }

    private bool m_Init = false;

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        m_Init = true;
        Get<Image>((int)Images.Image_Back).gameObject.BindEvent(OnButtonClickedBack);
        Get<TMP_Text>((int)Texts.Text_Back).gameObject.BindEvent(OnButtonClickedBack);
        PlayAnimation();
        Debug.Log("초기화 완료");
    }

    public void SetText(string content)
    {
        //if (!m_Init)
        //    Init();

        Get<TMP_Text>((int)Texts.Text_Content).text = content;
    }

    void PlayAnimation()
    {
        // TODO : ??
    }

    void OnButtonClickedBack(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("back");
        UI_Manager.Instance.ClosePopupUI();
    }

}
