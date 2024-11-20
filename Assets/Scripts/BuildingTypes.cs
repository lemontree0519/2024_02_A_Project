using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    CraftingTable,      //���۴�
    Funace,             //�뱤��
    Kitchen,            //�ֹ�
    Stroage             //â��
}

[System.Serializable]
public class CraftingRecipe
{
    public string itemName;     //������ ������ �̸�
    public ItemType resultItem; //�����
    public int resultAmount = 1;    //����� ����
    public ItemType[] requiredItems;    //�ʿ��� ����
    public int[] requiredAmounts;       //�ʿ��� ��� ����
}