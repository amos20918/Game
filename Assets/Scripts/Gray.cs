using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gray : MonoBehaviour
{
    public Collider redHoleCollider;
    public Collider blueHoleCollider;
    public Collider purpleHoleCollider;
    public LayerMask RedHole;
    public LayerMask BlueHole;
    public LayerMask PurpleHole;


    private bool isOverlapping = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isOverlapping = Physics.CheckBox(transform.position, redHoleCollider.bounds.extents, Quaternion.identity, RedHole) ||
                        Physics.CheckBox(transform.position, blueHoleCollider.bounds.extents, Quaternion.identity, BlueHole) ||
                        Physics.CheckBox(transform.position, purpleHoleCollider.bounds.extents, Quaternion.identity, PurpleHole);

        if (isOverlapping)
        {

            if (Physics.CheckBox(transform.position, redHoleCollider.bounds.extents, Quaternion.identity, RedHole))
            {
                SetHoleColor(redHoleCollider, Color.gray);
            }
            if (Physics.CheckBox(transform.position, blueHoleCollider.bounds.extents, Quaternion.identity, BlueHole))
            {
                SetHoleColor(blueHoleCollider, Color.gray);
            }
            if (Physics.CheckBox(transform.position, purpleHoleCollider.bounds.extents, Quaternion.identity, PurpleHole))
            {
                SetHoleColor(purpleHoleCollider, Color.gray);
            }
        }
        if(transform.localScale.x == 1.2f)
        {
            //Debug.Log("into" + gameObject);
            if (redHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray &&
                !Physics.CheckBox(transform.position, redHoleCollider.bounds.extents, Quaternion.identity, RedHole))
            {
                SetHoleColor(redHoleCollider, Color.black);
            }
            if (blueHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray &&
                !Physics.CheckBox(transform.position, redHoleCollider.bounds.extents, Quaternion.identity, BlueHole))
            {
                SetHoleColor(blueHoleCollider, Color.black);
            }
            if (purpleHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray &&
                !Physics.CheckBox(transform.position, redHoleCollider.bounds.extents, Quaternion.identity, PurpleHole))
            {
                SetHoleColor(purpleHoleCollider, Color.black);
            }
        }
    }

    private void SetHoleColor(Collider holeCollider, Color color)
    {
        SpriteRenderer sr = holeCollider.gameObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = color;
        }
        //Debug.Log(sr.color);
    }

    private void ResetHoleColors()
    {
        SetHoleColor(redHoleCollider, Color.black);
        SetHoleColor(blueHoleCollider, Color.black);
        SetHoleColor(purpleHoleCollider, Color.black);
    }
}
