using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActiveState : MonoBehaviour
{
    public GameObject objecToSwitch;
    public bool initialState;
    // Start is called before the first frame update
    void Start()
    {
        if (!initialState)
        {
            objecToSwitch.SetActive(false);
        }
    }
    public void SwitchStateActive()
    {
        if (objecToSwitch != null)
        {
            Debug.Log("Switch!" + objecToSwitch.name);
            objecToSwitch.SetActive(true); // ���� Palse ���󪺱ҥΪ��A
            Debug.Log(objecToSwitch.activeSelf, gameObject);
        }
    }
    public void SwitchStateInactive()
    {
        if (objecToSwitch != null)
        {
            Debug.Log("Switch!" + objecToSwitch.name);
            objecToSwitch.SetActive(false); // ���� Palse ���󪺱ҥΪ��A
            Debug.Log(objecToSwitch.activeSelf, gameObject);
        }
    }
}
