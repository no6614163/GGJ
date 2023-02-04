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
        // TODO : �ִϸ��̼� ������ �����ؾߵ�.
        // TODO : Image_Background Ȱ���ؼ� alpha �� Ȱ���� �ִϸ��̼�
        // TODO : add typing animation
        Get<TMP_Text>((int)Texts.Text_TotalGold).text = "";
        Get<TMP_Text>((int)Texts.Text_TotalAnimals).text = "";
        Get<TMP_Text>((int)Texts.Text_TotalDays).text = "";
        Get<TMP_Text>((int)Texts.Text_Reason).text = "";
    }

    void OnButtonClickedOut(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        // TODO : �ִϸ��̼ǵ� �־��ָ� ����.
        Debug.Log("Out");
        GameSystem.Instance.LoadScene((int)SceneType.Start);
    }





}
