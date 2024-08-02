using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int sceneNumber;
    public void LoadScene_()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
