using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    CraftingTable,      //제작대
    Funace,             //용광로
    Kitchen,            //주방
    Stroage             //창고
}

[System.Serializable]
public class CraftingRecipe
{
    public string itemName;     //제작할 아이템 이름
    public ItemType resultItem; //결과물
    public int resultAmount = 1;    //결과물 개수
    public ItemType[] requiredItems;    //필요한 재료들
    public int[] requiredAmounts;       //필요한 재료 개수
}