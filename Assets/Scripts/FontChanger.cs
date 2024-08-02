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
        // ������������Ҧ�TextMeshProUGUI�ե�
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>();

        // �M���Ҧ�TextMeshProUGUI�ե�ó]�m�r��겣
        foreach (TMP_Text text in texts)
        {
            text.font = newFont;
            text.fontSize += add;
        }
    }
}
