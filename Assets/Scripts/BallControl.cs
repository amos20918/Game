using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Vector3 originalScale;
    public Vector3 enlargedScale = new Vector3(2f, 2f, 2f);
    private bool isBeingDragged = false;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // �˴��ƹ�����O�_���U
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            // �p�G�ƹ��I����y��
            if (collider != null && collider.gameObject == gameObject)
            {
                transform.localScale = enlargedScale;
                isBeingDragged = true;
            }
        }
        if (Input.GetMouseButton(0) && isBeingDragged)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // �T�O Z �b�O���� 0
            transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isBeingDragged)
        {
            transform.localScale = originalScale;
            isBeingDragged = false;
        }
    }
}
