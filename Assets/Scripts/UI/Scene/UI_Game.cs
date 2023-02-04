using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{
    enum Texts
    {
        Text_Title,

    }

    enum Images
    {
        Image_Time,
        Image_Gold,
        Image_Settings,
        Image_Shop,
        Image_Back,
    }

    enum Sliders
    {
        Slider_Timer,
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));

        Get<Image>((int)Images.Image_Settings).gameObject.BindEvent(OnButtonClickedSettings);

    }

    void OnButtonClickedSettings(PointerEventData evt)
    {
        Debug.Log("Settings");
        UI_Manager.Instance.ShowPopupUI<UI_SettingsPopup>();
    }

    void OnButtonClickedShop(PointerEventData evt)
    {
        Debug.Log("Shop");
        UI_Manager.Instance.ShowPopupUI<UI_ShopPopup>();
    }

    /// <summary>
    ///  UI Slider의 수치 입력
    ///  value = 남은 시간, 0은 failed, 시작 시간 : 1
    /// </summary>
    public void SetTimeSlider(float value)
    {
        Get<Slider>((int)Sliders.Slider_Timer).value = value;
    }




}
