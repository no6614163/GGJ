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
        Text_Start,
        Text_Time,
        Text_Gold,
        Text_HappyPoint,
        Text_FoodPoint,

    }

    enum Images
    {
        Image_Time,
        Image_Gold,
        Image_Setting,
        Image_Shop,
        Image_Back,
        Image_Home,
    }

    enum Sliders
    {
        Slider_Happy,
        Slider_Food,
    }

    public override void Init()
    {
        base.Init();

        EventManager.Instance.GameEvent.OnPurchaseRequest += GameEvent_OnPurchaseRequest;

        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));

        Get<TMP_Text>((int)Texts.Text_Start).gameObject.BindEvent(OnButtonClickedStart);

        Get<Slider>((int)Sliders.Slider_Happy).maxValue = 100;
        Get<Slider>((int)Sliders.Slider_Food).maxValue = 100;
        
        SetInit();
        
        Get<Image>((int)Images.Image_Setting).gameObject.BindEvent(OnButtonClickedSettings);
        Get<Image>((int)Images.Image_Shop).gameObject.BindEvent(OnButtonClickedShop);
        
    }

    void GameEvent_OnPurchaseRequest()
    {
        Get<TMP_Text>((int)Texts.Text_Gold).text = GameSystem.Instance.Gold.ToString();
        
        Get<TMP_Text>((int)Texts.Text_HappyPoint).text = string.Format("{0} / {1}", GameSystem.Instance.HappyPoint, 100);
        Get<TMP_Text>((int)Texts.Text_FoodPoint).text = string.Format("{0} / {1}", GameSystem.Instance.FoodPoint, 100);

        Get<Slider>((int)Sliders.Slider_Happy).value = GameSystem.Instance.HappyPoint;
        Get<Slider>((int)Sliders.Slider_Food).value = GameSystem.Instance.FoodPoint;
    }

    void SetInit()
    {
        // TODO : init 할 때 푸드포인트 혹은 해피포인트가 위험하면 팝업 띄우는게 나을듯.
        Get<TMP_Text>((int)Texts.Text_Time).text = string.Format("Day {0}", GameSystem.Instance.Day);
        Get<TMP_Text>((int)Texts.Text_Gold).text = GameSystem.Instance.Gold.ToString();

        Get<TMP_Text>((int)Texts.Text_HappyPoint).text = string.Format("{0} / {1}", GameSystem.Instance.HappyPoint, 100);
        Get<TMP_Text>((int)Texts.Text_FoodPoint).text = string.Format("{0} / {1}", GameSystem.Instance.FoodPoint, 100);

        Get<Slider>((int)Sliders.Slider_Happy).value = GameSystem.Instance.HappyPoint;
        Get<Slider>((int)Sliders.Slider_Food).value = GameSystem.Instance.FoodPoint;
    }

    void OnButtonClickedStart(PointerEventData evt)
    {
        Debug.Log("Start");
        UI_Manager.Instance.ShowPopupUI<UI_LevelPopup>();
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
