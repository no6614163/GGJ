using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class UI_ShopPopup : UI_Popup
{
    enum Texts
    {
        Text_Title,
        Text_Settings,
        Text_Controls,
        Text_Languages,
        Text_Time,
        Text_Gold,
        Text_HappyPoint,
        Text_FoodPoint,
    }

    enum Images
    {
        Image_Back,
        Image_Food,
        Image_Setting,
        Image_FoodOutline,
        Image_Deco,
        Image_DecoOutline,
        Image_Animals,
        Image_AnimalsOutline,
    }

    enum Sliders
    {
        Slider_Happy,
        Slider_Food,
    }

    [SerializeField]
    private Transform m_ShopItemParent;

    private List<UI_ShopItem> m_ShopItems;

    public override void Init()
    {
        base.Init();
        m_ShopItems = new List<UI_ShopItem>();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        EventManager.Instance.GameEvent.OnPurchaseRequest += GameEvent_OnPurchaseRequest;

        Bind<Slider>(typeof(Sliders));

        Get<Image>((int)Images.Image_Back).gameObject.BindEvent(OnButtonClickedBack);
        Get<Image>((int)Images.Image_Food).gameObject.BindEvent(OnButtonClickedFood);
        Get<Image>((int)Images.Image_Deco).gameObject.BindEvent(OnButtonClickedDeco);
        Get<Image>((int)Images.Image_Setting).gameObject.BindEvent(OnButtonClickedSetting);
        SetOffAllOutline();
        SetInit();
        Get<Image>((int)Images.Image_Animals).gameObject.BindEvent(OnButtonClickedAnimals);
        SetFoodItems();
        Get<Image>((int)Images.Image_FoodOutline).gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        EventManager.Instance.GameEvent.OnPurchaseRequest -= GameEvent_OnPurchaseRequest;
    }
    void GameEvent_OnPurchaseRequest(int gold)
    {
        EffectManager.Instance.SpawnNumber(Get<TMP_Text>((int)Texts.Text_Gold).rectTransform, gold, Get<TMP_Text>((int)Texts.Text_Gold).rectTransform.anchoredPosition);

        Get<TMP_Text>((int)Texts.Text_Gold).text = GameSystem.Instance.Gold.ToString();
        Get<TMP_Text>((int)Texts.Text_HappyPoint).text = string.Format("{0} / {1}", GameSystem.Instance.HappyPoint, 100);
        Get<TMP_Text>((int)Texts.Text_FoodPoint).text = string.Format("{0} / {1}", GameSystem.Instance.FoodPoint, 100);

        Get<Slider>((int)Sliders.Slider_Happy).value = GameSystem.Instance.HappyPoint;
        Get<Slider>((int)Sliders.Slider_Food).value = GameSystem.Instance.FoodPoint;
    }

    void SetInit()
    {
        // TODO : init 할 때 푸드포인트 혹은 해피포인트가 위험하면 팝업 띄우는게 나을듯.
        Get<TMP_Text>((int)Texts.Text_Gold).text = GameSystem.Instance.Gold.ToString();

        Get<TMP_Text>((int)Texts.Text_HappyPoint).text = string.Format("{0} / {1}", GameSystem.Instance.HappyPoint, 100);
        Get<TMP_Text>((int)Texts.Text_FoodPoint).text = string.Format("{0} / {1}", GameSystem.Instance.FoodPoint, 100);

        Get<Slider>((int)Sliders.Slider_Happy).value = GameSystem.Instance.HappyPoint;
        Get<Slider>((int)Sliders.Slider_Food).value = GameSystem.Instance.FoodPoint;
    }

    void SetOffAllOutline()
    {
        Get<Image>((int)Images.Image_FoodOutline).gameObject.SetActive(false);
        Get<Image>((int)Images.Image_DecoOutline).gameObject.SetActive(false);
        Get<Image>((int)Images.Image_AnimalsOutline).gameObject.SetActive(false);
    }

    void OnButtonClickedBack(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Back");
        UI_Manager.Instance.ClosePopupUI();
    }

    void OnButtonClickedSetting(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Setting");
        var popup = UI_Manager.Instance.ShowPopupUI<UI_SettingsPopup>();
        var rect = popup.transform.GetChild(0).GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;
        rect.DOScale(1, 0.5f);
    }

    void OnButtonClickedFood(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Food");
        SetOffAllOutline();
        Get<Image>((int)Images.Image_FoodOutline).gameObject.SetActive(true);
        RemoveItems();
        SetFoodItems();
    }
    void OnButtonClickedDeco(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        Debug.Log("Deco");
        SetOffAllOutline();
        Get<Image>((int)Images.Image_DecoOutline).gameObject.SetActive(true);
        RemoveItems();
        SetDecoItems();
    }
    void OnButtonClickedAnimals(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
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
            Sprite sprite = Resources.Load<Sprite>("Images/ShopItemImages/" + foods[i].ItemName);
            shopItem.SetData(foods[i], sprite);
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

        // Load all sprites in atlas
        Sprite[] Sprites = Resources.LoadAll<Sprite>("Images/ShopItemImages/AllCharacters");
        for (int i = 0; i < animals.Count; i++)
        {
            var shopItem = Instantiate(Resources.Load<UI_ShopItem>("Prefabs/UI/Popup/ShopItem"), m_ShopItemParent);
            // Get specific sprite
            Sprite sprite = Sprites.Single(s => s.name == animals[i].ItemName);
            shopItem.SetData(animals[i], sprite);
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
