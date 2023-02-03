using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    enum Texts
    {
        Text_Title,

    }

    enum Images
    {
        Image_Time,
        Image_Gold,
        Image_Setting,
        Image_Shop,
        Image_Back,
    }

    enum Sliders
    {
        //Slider_
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Image>((int)Images.Image_Setting).gameObject.BindEvent(OnButtonClickedSettings);
        Get<Image>((int)Images.Image_Shop).gameObject.BindEvent(OnButtonClickedSettings);

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





}
