using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingTheBall : MonoBehaviour
{
    private ReceiveHandMessage mqttMessage;

    private Vector3 originalScale;
    private bool hand = false;

    public Transform crosshair;
    public LayerMask moveableLayer;
    void Start()
    {
        mqttMessage = GetComponent<ReceiveHandMessage>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (mqttMessage != null)
        {
            hand = mqttMessage.hand_close_bool;
        }
        Collider[] hitColliders = Physics.OverlapBox(crosshair.position, transform.GetComponent<Collider>().bounds.extents, Quaternion.identity, moveableLayer);

        bool isOverlapping = false;
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == gameObject)
            {
                isOverlapping = true;
                break;
            }
        }
        if (isOverlapping && hand && ( NoBallTaken() || SelfBallTaken() ) )
        {
            transform.position = crosshair.position;
            transform.localScale = originalScale * 1.2f;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
        }
    }
    bool NoBallTaken()
    {
        bool ball = true;
        Collider[] hitColliders = Physics.OverlapBox(crosshair.position, transform.GetComponent<Collider>().bounds.extents, Quaternion.identity, moveableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.transform.localScale.x == 1.2f)
            {
                ball = false;
                return ball;
            }
        }
        return ball;
    }

    bool SelfBallTaken()
    {
        bool ball = false;
        Collider[] hitColliders = Physics.OverlapBox(crosshair.position, transform.GetComponent<Collider>().bounds.extents, Quaternion.identity, moveableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.transform.localScale.x == 1.2f && hitCollider.gameObject.name == gameObject.name)
            {
                ball = true;
                return ball;
            }
        }
        return ball;
    }
}
