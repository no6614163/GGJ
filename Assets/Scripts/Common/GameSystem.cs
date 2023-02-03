using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : Singleton<GameSystem>
{
    public int Gold { get; private set; }
    public int HappyPoint { get; private set; }
    public int FoodPoint { get; private set; }





    public void SetGold(int gold)
    {
        Gold += gold;
    }

    public void SetHappyPoint(int happyPoint)
    {
        HappyPoint += happyPoint;

    }
    public void SetFoodPoint(int foodPoint)
    {
        FoodPoint += foodPoint;
    }





}
