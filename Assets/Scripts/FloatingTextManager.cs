using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;         //�̱���
    public GameObject textPrefabs;                      //UI �ؽ�Ʈ ������

    private void Awake()
    {
        instance = this;                                //�̱��� ���
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(worldPos);       //���� �·Ḧ ��ũ�� ��ǥ�� ��ȯ

        GameObject textObj = Instantiate(textPrefabs, transform);       //UI�ؽ�Ʈ ����

        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

        if (temp != null)
        {
            temp.text = text;

            StartCoroutine(AnimateText(textObj));
        }
    }

    private IEnumerator AnimateText(GameObject textobj)
    {
        float duration = 1f;                    //���� �ð�
        float timer = 0;                        //����� Ÿ�̸�

        Vector3 startPos = textobj.transform.position;
        TextMeshProUGUI temp = textobj.GetComponent<TextMeshProUGUI>();     //�޾ƿ� ������Ʈ���� TMP ��Ʈ ����

        while (timer < duration)                        //Ÿ�̸� 1�� �� ����
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textobj.transform.position = startPos + Vector3.up * (progress * 50f);      //��Ʈ�� ���� �ö󰡴� ȿ���� �ش�

            if (temp != null)
            {
                temp.alpha = 1 - progress;
            }
            yield return null;
        }

        Destroy(textobj);
    }
}
