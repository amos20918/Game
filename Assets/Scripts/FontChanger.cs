using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChanger : MonoBehaviour
{
    public TMP_FontAsset newFont;
    public float add;
    void Start()
    {
        // 獲取場景中的所有TextMeshProUGUI組件
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>();

        // 遍歷所有TextMeshProUGUI組件並設置字體資產
        foreach (TMP_Text text in texts)
        {
            text.font = newFont;
            text.fontSize += add;
        }
    }
}
