using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SettingsPopup : UI_Popup
{
    enum Texts
    {
        Text_Title,
        Text_Quit,
    }

    enum Images
    {
        Image_Back,
    }

    enum Sliders
    {
        Slider_Background,
        Slider_Effect,

    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));

        Get<Image>((int)Images.Image_Back).gameObject.BindEvent(OnButtonClickedBack);
        Get<TMP_Text>((int)Texts.Text_Quit).gameObject.BindEvent(OnButtonClickedQuit);

        Get<Slider>((int)Sliders.Slider_Effect).onValueChanged.AddListener(OnValueChangedEffect);
        Get<Slider>((int)Sliders.Slider_Background).onValueChanged.AddListener(OnValueChangedBackground);
        
        SetValues();
    }

    void OnButtonClickedBack(PointerEventData evt)
    {
        Debug.Log("Back");
        UI_Manager.Instance.ClosePopupUI();
    }

    void OnButtonClickedQuit(PointerEventData evt)
    {
        Debug.Log("Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void SetValues()
    {
        // TODO : 사운드 매니저에서 값 받아서 넣어주기
        //Get<Slider>((int)Sliders.Slider_Effect).value = 1;
        //Get<Slider>((int)Sliders.Slider_Background).value = 1;
    }

    void OnValueChangedEffect(float value)
    {
        // TODO : 사운드매니저를 통해서 사운드 조절

    }

    void OnValueChangedBackground(float value)
    {
        // TODO : 사운드매니저를 통해서 사운드 조절

    }


}
