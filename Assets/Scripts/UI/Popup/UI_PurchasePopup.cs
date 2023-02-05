using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class UI_PurchasePopup : UI_Popup
{
    enum Texts
    {
        Text_Content,
    }

    enum Images
    {
        Image_Purchase,
        Image_Cancel,
    }

    private bool m_Init = false;
    private ShopItem m_Item;

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        m_Init = true;
    }

    public void SetData(ShopItem data)
    {
        if (!m_Init)
            Init();

        m_Item = data;

        Get<TMP_Text>((int)Texts.Text_Content).text = string.Format("Purchase a '{0}'?", m_Item.ItemName);

        Get<Image>((int)Images.Image_Purchase).gameObject.BindEvent(OnButtonClickedPurchase);
        Get<Image>((int)Images.Image_Cancel).gameObject.BindEvent(OnButtonClickedCancel);
    }

    void OnButtonClickedPurchase(PointerEventData evt)
    {
        // �� �ִ��� üũ�ؾߵ�. ���� ��� Ȯ�� �� �����˾� �����
        if(GameSystem.Instance.Gold >= m_Item.Price)
        {
            // ���Ű��� �� ���
            GameSystem.Instance.SetGold(-m_Item.Price);
            switch (m_Item.ItemType)
            {
                case ItemType.Food:
                    GameSystem.Instance.SetFoodPoint(m_Item.Point);
                    break;
                case ItemType.Deco:
                    GameSystem.Instance.SetHappyPoint(m_Item.Point);
                    break;
                case ItemType.Animal:
                    // ���� ���� instantiate �����Ǿ����. ��ġ�� ��������?
                    var values = Enum.GetValues(typeof(AnimalType));
                    foreach (var value in values)
                    {
                        if(value.ToString() == m_Item.ItemName)
                        {
                            GameSystem.Instance.AddAnimal((AnimalType)value);
                            break;
                        }
                    }
                    break;
            }
            SoundManager.Instance.PlaySFX("Cash", "UI");
            EventManager.Instance.GameEvent.InvokePurchaseItem(m_Item.Price);
            UI_Manager.Instance.ClosePopupUI();
        }
        else
        {
            // ���� �����ؼ� ���Ÿ� ���ϴ� ���
            var popup = UI_Manager.Instance.ShowPopupUI<UI_WarningPopup>();
            popup.SetText("Not enough gold");
        }

    }

    void OnButtonClickedCancel(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        // ���
        UI_Manager.Instance.ClosePopupUI();
    }


}
