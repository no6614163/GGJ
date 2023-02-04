using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
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
        Image_FoodOutline,
        Image_Deco,
        Image_DecoOutline,
        Image_Animals,
        Image_AnimalsOutline,
    }

    [SerializeField]
    private Transform m_ShopItemParent;

    private List<UI_ShopItem> m_ShopItems;

    public override void Init()
    {
        // TODO : 처음 세팅은 food
        base.Init();
        m_ShopItems = new List<UI_ShopItem>();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Image>((int)Images.Image_Back).gameObject.BindEvent(OnButtonClickedBack);
        Get<Image>((int)Images.Image_Food).gameObject.BindEvent(OnButtonClickedFood);
        Get<Image>((int)Images.Image_Deco).gameObject.BindEvent(OnButtonClickedDeco);
        SetOffAllOutline();

        Get<Image>((int)Images.Image_Animals).gameObject.BindEvent(OnButtonClickedAnimals);
        SetFoodItems();
        Get<Image>((int)Images.Image_FoodOutline).gameObject.SetActive(true);
    }

    void SetOffAllOutline()
    {
        Get<Image>((int)Images.Image_FoodOutline).gameObject.SetActive(false);
        Get<Image>((int)Images.Image_DecoOutline).gameObject.SetActive(false);
        Get<Image>((int)Images.Image_AnimalsOutline).gameObject.SetActive(false);
    }

    void OnButtonClickedBack(PointerEventData evt)
    {
        Debug.Log("Back");
        UI_Manager.Instance.ClosePopupUI();
    }
    void OnButtonClickedFood(PointerEventData evt)
    {
        Debug.Log("Food");
        SetOffAllOutline();
        Get<Image>((int)Images.Image_FoodOutline).gameObject.SetActive(true);
        RemoveItems();
        SetFoodItems();
    }
    void OnButtonClickedDeco(PointerEventData evt)
    {
        Debug.Log("Deco");
        SetOffAllOutline();
        Get<Image>((int)Images.Image_DecoOutline).gameObject.SetActive(true);
        RemoveItems();
        SetDecoItems();
    }
    void OnButtonClickedAnimals(PointerEventData evt)
    {
        Debug.Log("Animals");
        SetOffAllOutline();
        Get<Image>((int)Images.Image_AnimalsOutline).gameObject.SetActive(true);
        RemoveItems();
        SetAnimalItems();
    }

    void SetFoodItems()
    {
        List<ShopItem> foods = GameSystem.Instance.ShopItems.FindAll((x) =>  x.ItemType == ItemType.Food );
        for (int i = 0; i < foods.Count; i++)
        {
            var shopItem = Instantiate(Resources.Load<UI_ShopItem>("Prefabs/UI/Popup/ShopItem"), m_ShopItemParent);
            shopItem.SetData(foods[i]);
            m_ShopItems.Add(shopItem);
        }
    }

    void SetDecoItems()
    {
        List<ShopItem> decos = GameSystem.Instance.ShopItems.FindAll((x) => x.ItemType == ItemType.Deco);
        for (int i = 0; i < decos.Count; i++)
        {
            var shopItem = Instantiate(Resources.Load<UI_ShopItem>("Prefabs/UI/Popup/ShopItem"), m_ShopItemParent);
            shopItem.SetData(decos[i]);
            m_ShopItems.Add(shopItem);
        }
    }

    void SetAnimalItems()
    {
        List<ShopItem> animals = GameSystem.Instance.ShopItems.FindAll((x) => x.ItemType == ItemType.Animal);
        for (int i = 0; i < animals.Count; i++)
        {
            var shopItem = Instantiate(Resources.Load<UI_ShopItem>("Prefabs/UI/Popup/ShopItem"), m_ShopItemParent);
            shopItem.SetData(animals[i]);
            m_ShopItems.Add(shopItem);
        }
    }

    void RemoveItems()
    {
        int count = m_ShopItems.Count;
        for (int i = count-1; i >= 0; i--)
        {
            Destroy(m_ShopItems[i].gameObject);
        }
        m_ShopItems.Clear();

    }
}
