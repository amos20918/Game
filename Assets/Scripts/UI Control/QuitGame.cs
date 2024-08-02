using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // 在編輯器中停止播放模式
        #else
                Application.Quit(); // 在構建好的遊戲中退出
        #endif
    }
}
