using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinOrLoss : MonoBehaviour
{
    public TMP_Text point_text;
    public GameObject winWindow;
    public GameObject lossWindow;

    // Start is called before the first frame update
    void Start()
    {
        winWindow.SetActive(false);
        lossWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float point = float.Parse(point_text.text.Replace("Point:", "").Replace("/30", ""));
        if (point >= 30)
        {
            winWindow.SetActive(true);
        }
        else if(point <= 0)
        {
            lossWindow.SetActive(true);
        }
        else
        {
            lossWindow.SetActive(false);
            winWindow.SetActive(false);
        }
    }
    
}
