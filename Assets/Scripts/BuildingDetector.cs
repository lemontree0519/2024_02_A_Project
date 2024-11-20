using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;            //������ ���� ����
    public Vector3 lastPosition;                 //�÷��̾��� ������ ��ġ (�÷��̾� �̵��� ���� �� ��� �ֺ��� ã�� ���� ����)
    public float moveThreshold = 0.1f;          //�̵� ���� �Ӱ谪
    public ConstructibleBuilding currentNearbyBuilding;   //���� ������ �ִ� ���� ������ ������
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;  //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForBuilding();                    //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)     //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        {
            CheckForBuilding();                                            //�̵��� ������ üũ
            lastPosition = transform.position;                          //���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        //����� �������� �ְ� EŰ�� ������ �� ������ ����
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());     //�÷��̾� �κ��丮�� �����Ͽ� ������ ����
        }
    }
    //�ֺ��� ���� ������ �������� �����ϴ� �Լ�

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);       //���� ���� ���� ��� �ݶ��̴��� ã�ƿ�

        float closestDistance = float.MaxValue;                     //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;                         //���� ����� �������� �ʱⰪ

        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();        //�������� ����
            if (building != null && building.canBuild)              //�������� �ְ� ���� �������� Ȯ��
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);     //�Ÿ� ���
                if (distance < closestDistance)                                                     //�� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    closestBuilding = building;
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)        //���� ����� �������� ����Ǿ��� �� �޼��� ǥ��
        {
            currentNearbyBuilding = closestBuilding;
            if (currentNearbyBuilding != null)          //���� ����� ������ ������Ʈ
            {
                if (FloatingTextManager.instance != null)
                {
                    Vector3 textPostion = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show(
                        $"[F] Ű�� {currentNearbyBuilding.buildingName} �Ǽ� (���� {currentNearbyBuilding.requiredTree} �� �ʿ�)"
                        , currentNearbyBuilding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
