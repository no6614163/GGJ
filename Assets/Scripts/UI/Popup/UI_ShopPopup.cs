using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopPopup : UI_Popup
{
    enum Texts
    {
        Text_Title,
        Text_Settings,
        Text_Controls,
        Text_Languages,
    }

    enum Images
    {
        Image_Back,
        Image_Food,
        Image_Deco,
        Image_Animals,
    }

    public override void Init()
    {
        // TODO : 처음 세팅은 food
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Image>((int)Images.Image_Back).gameObject.BindEvent(OnButtonClickedBack);
        Get<Image>((int)Images.Image_Food).gameObject.BindEvent(OnButtonClickedFood);
        Get<Image>((int)Images.Image_Deco).gameObject.BindEvent(OnButtonClickedDeco);
        Get<Image>((int)Images.Image_Animals).gameObject.BindEvent(OnButtonClickedAnimals);

    }

    void OnButtonClickedBack(PointerEventData evt)
    {
        Debug.Log("Back");
        UI_Manager.Instance.ClosePopupUI();
    }
    void OnButtonClickedFood(PointerEventData evt)
    {
        Debug.Log("Food");
    }
    void OnButtonClickedDeco(PointerEventData evt)
    {
        Debug.Log("Deco");
    }
    void OnButtonClickedAnimals(PointerEventData evt)
    {
        Debug.Log("Animals");
    }

    void SetFoodItems()
    {

    }

    void SetDecoItems()
    {

    }

    void SetAnimalItems()
    {

    }



}
