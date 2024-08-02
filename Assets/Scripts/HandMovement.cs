using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] bool useHand = true;

    private ReceiveMqttMessage mqttMessage;
    private float rightX;
    private float leftX;
    private float x_mapped;
    private float y_mapped;

    public float smoothFactor = 0.1f;

    void Start()
    {
        mqttMessage = GetComponent<ReceiveMqttMessage>();

        Camera cam = Camera.main;
        Vector3 screenLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, cam.nearClipPlane));  
        leftX = screenLeft.x;
        rightX = screenRight.x;
    }

    void Update()
    {
        
        if (useHand)
        {
            Vector3 mapped = MappedCoordinate();
            transform.position = Vector3.Lerp(transform.position, mapped, smoothFactor);
        }
    }
    public float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }
    public Vector3 MappedCoordinate()
    {
        float x = mqttMessage.x_float;
        float y = mqttMessage.y_float;
        x_mapped = Map(x, 0f, 200f, rightX, leftX);
        y_mapped = Map(y, 0f, 200f, rightX, leftX);
        //Debug.Log("x: " + x_mapped);
        //Debug.Log("y: " + y_mapped);
        return new Vector3(x_mapped, y_mapped, transform.position.z);
    }
}
