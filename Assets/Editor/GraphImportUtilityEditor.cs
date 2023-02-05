using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GraphSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphImportUtilityEditor : EditorWindow {

    [MenuItem("Tools/Graph Import Utility Editor")]
    public static void ShowMyEditor() {

        EditorWindow wnd = GetWindow<GraphImportUtilityEditor>();
        wnd.titleContent = new GUIContent("Graph Import Utility Editor");

        wnd.minSize = new Vector2(250, 100);
        wnd.maxSize = new Vector2(250, 100);
    }

    protected void CreateGUI() {

        var btn = new Button();
        btn.text = "Import CSV";
        btn.clicked += OnImportCSV;

        rootVisualElement.Add(btn);
    }

    void OnImportCSV() {
        GraphImportUtility.ImportGraphFromCSV();
    }
}
