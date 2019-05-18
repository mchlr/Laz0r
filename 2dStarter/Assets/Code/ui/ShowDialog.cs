using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialog : MonoBehaviour
{
    public void showIngameMessage(string txt)
    {
        UnityEditor.EditorUtility.DisplayDialog("Hello Ingame!", txt, "oki");
    }
}
