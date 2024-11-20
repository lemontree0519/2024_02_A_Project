using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;                   //������ ���� (��: ũ����Ż, �Ĺ�, ��Ǯ, ����)
    public string itemName;                     //������ �̸�
    public float respawnTime = 30.0f;           //������ �ð�(�������� �ٽ� ���� �� �� ������ ��� �ð�)
    public bool canCollect = true;              //���� ���� ����(���� �� �� �ִ��� ���θ� ��Ÿ��)

    //�������� �����ϴ� �޼��� PlayerInventory�� ���� �κ��丮�� �߰�
    public void CollectItem(PlayerInventory inventory)
    {
        //���� ���� ���θ� üũ
        if (!canCollect) return;
        
        inventory.AddItem(itemType);        //�������� �κ��丮�� �߰�

        if (FloatingTextManager.instance != null)
        {
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;          //������ ��ġ���� �ణ ���� �ؽ�Ʈ ����
            FloatingTextManager.instance.Show($" + {itemName}", textPosition);
        }

        Debug.Log($"{itemName} ���� �Ϸ�"); //������ ���� �Ϸ� �޼��� ���
        StartCoroutine(RespawnRoutine());       //������ ������ �ڷ�ƾ ����
    }

    //������ �������� ó���ϴ� �ڷ�ƾ
    private IEnumerator RespawnRoutine()
    {
        canCollect = false;                                 //���� �ư��� ���·� ����
        GetComponent<MeshRenderer>().enabled = false;       //�������� MeshRenderer�� ���� ������ �ʰ� ��
        GetComponent<MeshCollider>().enabled = false;

        yield return new WaitForSeconds(respawnTime);       //������ ������ �ð� ��ŭ ���

        GetComponent<MeshRenderer>().enabled = true;        //�������� �ٽ� ���̰� ��
        GetComponent<MeshCollider>().enabled = true;
        canCollect = true;                                  //���� ���� ���·� ����
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
