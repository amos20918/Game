using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using mqttConnection;
using TMPro;
//using TMPro;

public class ReceiveMqttMessage : MonoBehaviour
{
    private Rigidbody2D rb;
    public string output = "false";

    static string key_api_1 = "hand_sign";
    static string key_api_2 = "position_x";
    static string key_api_3 = "position_y";

    public TMP_Text X;
    public TMP_Text Y;
    public TMP_Text Hand;

    public float x_float;
    public float y_float;
    public bool hand_close_bool;




    private mqttConnect mqttScript; // Reference to your MQTT script

    public void ReceiveCommand(string command) // Make sure it's public
    {
        Debug.Log($"Received command: {command}");  // For debugging
        output = command;
    }

    void Start()
    {
        // Find the MQTT script in your scene (adjust the name if needed)
        mqttScript = FindObjectOfType<mqttConnect>();

        if (mqttScript != null)
        {
            mqttScript.MessageReceived += HandleMqttMessage;
        }
        else
        {
            Debug.LogError("mqttConnect script not found in the scene!");
        }
        rb = GetComponent<Rigidbody2D>();
    }

    private void HandleMqttMessage(string msg)
    {
        output = msg.Replace("\\", "").Replace("{", "").Replace("}", "").Replace(":", "").Replace("\"", "");
        if(output.Contains(key_api_1))
        {
            output = output.Replace(key_api_1, "");
            Hand.text = output;
            if(output == "power")
                hand_close_bool = true;
            else if(output == "stop")
                hand_close_bool = false;
            //Debug.Log(hand_close_bool);
        }
        else if (output.Contains(key_api_2))
        {
            output = output.Replace(key_api_2, "");
            X.text = output;
            //Debug.Log("x:" + output);
            if (X.text != "null")
                x_float = float.Parse(X.text);
        }
        else if (output.Contains(key_api_3))
        {
            output = output.Replace(key_api_3, "");
            Y.text = output;
            //Debug.Log("y:" + output);
            if (Y.text != "null")
                y_float = float.Parse(Y.text);
        }
    }
}
