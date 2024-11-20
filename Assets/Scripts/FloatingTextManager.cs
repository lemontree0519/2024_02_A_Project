using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;         //싱글톤
    public GameObject textPrefabs;                      //UI 텍스트 프리팹

    private void Awake()
    {
        instance = this;                                //싱글톤 등록
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(worldPos);       //월드 좌료를 스크린 좌표로 변환

        GameObject textObj = Instantiate(textPrefabs, transform);       //UI텍스트 생성

        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

        if (temp != null)
        {
            temp.text = text;

            StartCoroutine(AnimateText(textObj));
        }
    }

    private IEnumerator AnimateText(GameObject textobj)
    {
        float duration = 1f;                    //동작 시간
        float timer = 0;                        //사용할 타이머

        Vector3 startPos = textobj.transform.position;
        TextMeshProUGUI temp = textobj.GetComponent<TextMeshProUGUI>();     //받아온 오브젝트에서 TMP 폰트 참조

        while (timer < duration)                        //타이머 1초 전 까지
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textobj.transform.position = startPos + Vector3.up * (progress * 50f);      //폰트를 위로 올라가는 효과를 준다

            if (temp != null)
            {
                temp.alpha = 1 - progress;
            }
            yield return null;
        }

        Destroy(textobj);
    }
}
