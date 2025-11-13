using UnityEngine;
using UnityEditor;

public class TagsChildren : MonoBehaviour
{
    [MenuItem("Tools/Tag Children As Obstacle")]
    static void TagAllChildren()
    {
        GameObject parent = Selection.activeGameObject;
        if (parent == null)
        {
            Debug.LogError("Select a parent object first.");
            return;
        }

        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.tag = "Obstacle";
        }

        Debug.Log("All children tagged as Obstacle.");
    }
}
