using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameFailedPopup : UI_Popup
{

    enum Texts
    {
        Text_TotalGold,
        Text_TotalAnimals,
        Text_TotalDays,
        Text_Reason,
    }

    enum Images
    {
        Image_Background,
        Image_Out,
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));


        Get<Image>((int)Images.Image_Out).gameObject.BindEvent(OnButtonClickedOut);
        PlayAnimation();
    }
    void PlayAnimation()
    {
        // TODO : 애니메이션 순차로 실행해야됨.
        // TODO : Image_Background 활용해서 alpha 값 활용한 애니메이션
        // TODO : add typing animation
        Get<TMP_Text>((int)Texts.Text_TotalGold).text = "";
        Get<TMP_Text>((int)Texts.Text_TotalAnimals).text = "";
        Get<TMP_Text>((int)Texts.Text_TotalDays).text = "";
        Get<TMP_Text>((int)Texts.Text_Reason).text = "";
    }

    void OnButtonClickedOut(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Out");
        GameSystem.Instance.LoadScene((int)SceneType.Start);
    }





}
