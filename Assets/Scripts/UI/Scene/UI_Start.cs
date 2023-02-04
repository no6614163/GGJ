using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Start : UI_Scene
{
    enum Texts
    {
        Text_Title,
        Text_Start,
        Text_Settings,
        Text_Credits,
        Text_Quit,
    }

    enum Images
    {
        Image_Back,
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        //Bind<Image>(typeof(Images));

        Get<TMP_Text>((int)Texts.Text_Start).gameObject.BindEvent(OnButtonClickedStart);
        Get<TMP_Text>((int)Texts.Text_Settings).gameObject.BindEvent(OnButtonClickedSettings);
        Get<TMP_Text>((int)Texts.Text_Credits).gameObject.BindEvent(OnButtonClickedCredits);
        Get<TMP_Text>((int)Texts.Text_Quit).gameObject.BindEvent(OnButtonClickedQuit);
    }

    void OnButtonClickedStart(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Start");
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

    void OnButtonClickedSettings(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Settings");
        //UI_Manager.Instance.ShowPopupUI<UI_SettingsPopup>();
        var popup = UI_Manager.Instance.ShowPopupUI<UI_SettingsPopup>();
        var rect = popup.transform.GetChild(0).GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;
        rect.DOScale(1, 0.5f);

    }

    void OnButtonClickedCredits(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Credits");
        var popup = UI_Manager.Instance.ShowPopupUI<UI_CreditsPopup>();
        var rect = popup.transform.GetChild(0).GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;
        rect.DOScale(1, 0.5f);
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





}
