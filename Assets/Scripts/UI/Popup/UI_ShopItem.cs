using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_ShopItem : UI_Popup
{
    enum Texts
    {
        Text_Gold,
        Text_Name,
    }

    enum Images
    {
        Image_Item,
        Image_Gold,
        Image_Background,
    }

    private bool m_Init = false;
    private ShopItem m_Item;

    public override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        m_Init = true;
    }

    public void SetData(ShopItem data)
    {
        if (!m_Init)
            Init();

        m_Item = data;

        Get<Image>((int)Images.Image_Item).sprite = Resources.Load<Sprite>("Sprites/" + m_Item.ItemName);
        Get<TMP_Text>((int)Texts.Text_Name).text = m_Item.ItemName;
        Get<TMP_Text>((int)Texts.Text_Gold).text = m_Item.Price.ToString();

        Get<Image>((int)Images.Image_Background).gameObject.BindEvent(OnButtonClickedItem);
    }

    void OnButtonClickedItem(PointerEventData evt)
    {
        Debug.Log("Item");




    }

}
