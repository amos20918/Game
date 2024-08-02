using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewBall : MonoBehaviour
{
    public GameObject[] objectsToDuplicate; 
    public Transform[] spawnPoint; 
    public LayerMask ballLayer; 
    public float detectionRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DuplicateRandomObject()
    {
        
        if (objectsToDuplicate.Length > 0)
        {
            int randomIndex = Random.Range(0, objectsToDuplicate.Length);
            int randomIndex_1 = Random.Range(0, spawnPoint.Length);
            bool hasBall = CheckForBallLayerAtPosition(spawnPoint[randomIndex_1].position);
            while (hasBall)
            {
                randomIndex_1 = Random.Range(0, spawnPoint.Length);
                hasBall = CheckForBallLayerAtPosition(spawnPoint[randomIndex_1].position);
            }
            GameObject selectedObject = objectsToDuplicate[randomIndex];
            GameObject newObject = Instantiate(selectedObject, spawnPoint[randomIndex_1].position, spawnPoint[randomIndex_1].rotation, selectedObject.transform.parent);
        }
    }
    bool CheckForBallLayerAtPosition(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, detectionRadius, ballLayer);
        return hitColliders.Length > 0;
    }
}
