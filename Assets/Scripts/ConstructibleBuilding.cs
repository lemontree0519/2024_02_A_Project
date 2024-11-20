using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Settings")]
    public BuildingType buildingType;           //�ǹ� Ÿ�� ����
    public string buildingName;                 //�ǹ� �̸�
    public int requiredTree = 5;                //�ǹ� �Ǽ��� �ʿ��� ���� ����
    public float constructionTime = 2.0f;       //�ǹ� �Ǽ� �ð�

    public bool canBuild = true;                //�Ǽ� ���� ����
    public bool isConstructed = false;          //�Ǽ� �Ϸ� ����

    private Material buildingMaterial;          //�ǹ��� ���׸��� ����

    void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;
        //�ʱ� ���� ���� (������)
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    private IEnumerator ConstructionRoutine()
    {
        canBuild = false;
        float timer = 0;

        Color color = buildingMaterial.color;

        while (timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer / constructionTime);
            buildingMaterial.color = color;
            yield return null;
        }
        isConstructed = true;

        if (FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"{buildingName} �Ǽ� �Ϸ� !", transform.position + Vector3.up);
        }
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed) return;     //�Ǽ� ����, �Ϸ� ���� üũ�ؼ� ���� ��Ų��.

        if (inventory.treeCount >= requiredTree)    //�Ǽ��� �ʿ��� ���� ���� Ȯ�� ��
        {
            inventory.RemoveItem(ItemType.Tree, requiredTree);      //�ش� ���� ���� ��ŭ ����
            if (FloatingTextManager.instance == null)
            {
                FloatingTextManager.instance.Show($"{buildingName} �Ǽ� ���� !", transform.position + Vector3.up);
            }

            StartCoroutine(ConstructionRoutine());
        }
        else
        {
            if (FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"������ �����մϴ�! ({inventory.treeCount} / {requiredTree})", transform.position + Vector3.up);
            }
        }
    }

    void Update()
    {
        
    }
}
