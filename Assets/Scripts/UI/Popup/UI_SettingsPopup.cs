using DG.Tweening;
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
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Back");
        Time.timeScale = 1;
        var rect = transform.GetChild(0).GetComponent<RectTransform>();
        rect.DOScale(0, 0.5f).OnComplete(() => UI_Manager.Instance.ClosePopupUI());
    }

    void OnButtonClickedQuit(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void SetValues()
    {
        // TODO : ���� �Ŵ������� �� �޾Ƽ� �־��ֱ�
        Get<Slider>((int)Sliders.Slider_Effect).value = SoundManager.Instance._sfxSource.volume;
        Get<Slider>((int)Sliders.Slider_Background).value = SoundManager.Instance._bgmSource.volume;
    }

    void OnValueChangedEffect(float value)
    {
        // TODO : ����Ŵ����� ���ؼ� ���� ����
        SoundManager.Instance._sfxSource.volume = value;

    }

    void OnValueChangedBackground(float value)
    {
        // TODO : ����Ŵ����� ���ؼ� ���� ����
        SoundManager.Instance._bgmSource.volume = value;
        SoundManager.Instance._ambienceSource.volume = value;
    }


}
