using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("MyGame/TitleScreen")]
public class TitleScreen : MonoBehaviour
{
    void OnGUI()
    {
        GameGui.Apply();

        GUI.skin.label.fontSize = 48;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(0, 30, Screen.width, 100), "太空大战");

        if (GUI.Button(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.7f, 200, 30), "开始游戏"))
        {
            SceneManager.LoadScene("level1");
        }
    }
}
