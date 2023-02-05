using DG.Tweening;
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
        Slider_Timer,
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));

        Get<Slider>((int)Sliders.Slider_Happy).maxValue = 100;
        Get<Slider>((int)Sliders.Slider_Food).maxValue = 100;

        SetInit();
        
        Get<Image>((int)Images.Image_Setting).gameObject.BindEvent(OnButtonClickedSetting);

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

    void OnButtonClickedSetting(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Settings");
        Time.timeScale = 0;
        UI_Manager.Instance.ShowPopupUI<UI_SettingsPopup>();
        //var popup = UI_Manager.Instance.ShowPopupUI<UI_SettingsPopup>();
        //var rect = popup.transform.GetChild(0).GetComponent<RectTransform>();
        //rect.localScale = Vector3.zero;
        //rect.DOScale(1, 0.5f);
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
