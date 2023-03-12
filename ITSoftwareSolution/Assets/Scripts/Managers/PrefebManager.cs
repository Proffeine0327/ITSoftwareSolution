using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrefebManager : MonoBehaviour, ISerializationCallbackReceiver
{
    private static PrefebManager manager;

    public static GameObject GetObject(string key) => manager.table[key];

    [SerializeField] private List<string> keys = new List<string>();
    [SerializeField] private List<GameObject> values = new List<GameObject>();
    private Dictionary<string, GameObject> table = new Dictionary<string, GameObject>();

    private void Awake() 
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void OnAfterDeserialize()
    {
        table = new Dictionary<string, GameObject>();
        
        for(int i = 0; i < Mathf.Min(keys.Count, values.Count); i++) table.Add(keys[i], values[i]);
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        
        foreach(var kvp in table)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PrefebManager))]
public class PrefebManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Add"))
        {
            serializedObject.FindProperty("keys").arraySize++;
            serializedObject.FindProperty("values").arraySize++;
            
            serializedObject.FindProperty("keys").GetArrayElementAtIndex(serializedObject.FindProperty("keys").arraySize - 1).stringValue = " ";
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
