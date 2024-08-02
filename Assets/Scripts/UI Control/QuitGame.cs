using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // �b�s�边�������Ҧ�
        #else
                Application.Quit(); // �b�c�ئn���C�����h�X
        #endif
    }
}
