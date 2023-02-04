using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GraphSystem {
    public class GraphImportUtility : MonoBehaviour {

        public static Graph LoadGraphFromCSV(string filePath) {

            var dataset = Resources.Load<TextAsset>(filePath);
            string[] dataLines = dataset.text.Split('\n');

            var graphNodes = new List<Node>();

            for (int i = 0; i < dataLines.Length; i++) {
                string[] data = dataLines[i].Split(',');
                for (int d = 0; d < data.Length; d++) {
                    print(data[d]);
                }
            }

            return ScriptableObject.CreateInstance<Graph>();

        }

    }
}
