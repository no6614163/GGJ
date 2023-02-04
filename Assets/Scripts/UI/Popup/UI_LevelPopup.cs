using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LevelPopup : UI_Popup
{

    enum Images
    {
        Image_VeryEasy,
        Image_Easy,
        Image_Normal,
        Image_Hard,
        Image_VeryHard,
        Image_Back,
        Image_Start,
    }

    enum Texts
    {
        Text_Description,
    }

    private int m_Level = -1;

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<TMP_Text>((int)Texts.Text_Description).text = "";

        Get<Image>((int)Images.Image_VeryEasy).gameObject.BindEvent(OnButtonClickedVeryEasy);
        Get<Image>((int)Images.Image_Easy).gameObject.BindEvent(OnButtonClickedEasy);
        Get<Image>((int)Images.Image_Normal).gameObject.BindEvent(OnButtonClickedNormal);
        Get<Image>((int)Images.Image_Hard).gameObject.BindEvent(OnButtonClickedHard);
        Get<Image>((int)Images.Image_VeryHard).gameObject.BindEvent(OnButtonClickedVeryHard);

        Get<Image>((int)Images.Image_Back).gameObject.BindEvent(OnButtonClickedBack);
        Get<Image>((int)Images.Image_Start).gameObject.BindEvent(OnButtonClickedStart);
        PlayAnimation();
    }
    void PlayAnimation()
    {
        // TODO : 애니메이션 순차로 실행해야됨.
        // TODO : Image_Background 활용해서 alpha 값 활용한 애니메이션
        // TODO : add typing animation

    }

    void OnButtonClickedBack(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Out");
        //UI_Manager.Instance.ClosePopupUI();
        
        var rect = transform.GetChild(0).GetComponent<RectTransform>();
        if (rect.transform.localScale.x < 1)
            return;
        rect.DOScale(0, 0.5f).OnComplete(() => UI_Manager.Instance.ClosePopupUI());

    }

    void OnButtonClickedStart(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Out");
        if(m_Level == -1)
        {
            var popup = UI_Manager.Instance.ShowPopupUI<UI_WarningPopup>();
            popup.SetText("Please select level");
            // 레벨이 선택이 안된 경우 레벨 선택하라는 팝업 표시
        }
        else
        {
            GameSystem.Instance.SetNextDay();
            int rand = Random.Range(2, 8);
            GameSystem.Instance.LoadScene(rand);
        }
    }

    void OnButtonClickedVeryEasy(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        m_Level = 0;
        Get<TMP_Text>((int)Texts.Text_Description).text = "Selected Level : Very Easy";
        GameSystem.Instance.SetGameLevel(0);
    }

    void OnButtonClickedEasy(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        m_Level = 1;
        Get<TMP_Text>((int)Texts.Text_Description).text = "Selected Level : Easy";
        GameSystem.Instance.SetGameLevel(1);
    }

    void OnButtonClickedNormal(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        m_Level = 2;
        Get<TMP_Text>((int)Texts.Text_Description).text = "Selected Level : Normal";
        GameSystem.Instance.SetGameLevel(2);
    }

    void OnButtonClickedHard(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        m_Level = 3;
        Get<TMP_Text>((int)Texts.Text_Description).text = "Selected Level : Hard";
        GameSystem.Instance.SetGameLevel(3);
    }

    void OnButtonClickedVeryHard(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        m_Level = 4;
        Get<TMP_Text>((int)Texts.Text_Description).text = "Selected Level : Very Hard";
        GameSystem.Instance.SetGameLevel(4);
    }





}
