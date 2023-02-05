using System;
using System.Collections.Generic;
using Assets.Scripts.GraphSystem.Errors;
using Assets.Scripts.GraphSystem.Model.OutcomeByUserHandler;
using Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GraphSystem {
    public class GraphImportUtility : MonoBehaviour {
        static int EXPECTED_CSV_ROW_LENGTH = 10;
        static char CSV_DELIMITER = '|';

        protected void Start() {
            Debug.Log("Running GraphImportUtility Test...");
            ImportGraphFromCSV(@"D:\TRB-3.csv");
        }

        public static void ImportGraphFromCSV(string filePath) {

            var graph = ScriptableObject.CreateInstance<Graph>();

            var outcomeByEndHandler = ScriptableObject.CreateInstance<OutcomeByEndHandler>();
            var outcomeByUserHandler = ScriptableObject.CreateInstance<OutcomeByUserHandler>();
            var outcomeByRandHandler = ScriptableObject.CreateInstance<OutcomeByRandomisationHandler>();

            Dictionary<int, Node> nodeDict = new();
            List<Node> nodes = new();

            // var dataset = Resources.Load<TextAsset>(filePath);
            // string[] dataLines = dataset.text.Split('\n');

            string[] dataLines = System.IO.File.ReadAllLines(filePath);

            // first loop: consctruct all nodes

            for (int i = 1; i < dataLines.Length; i++) { // skip first line!
                string[] data = dataLines[i].Split(CSV_DELIMITER);

                if (data.Length < EXPECTED_CSV_ROW_LENGTH) {
                    continue;
                    // throw new CSVRowHasWrongLength(EXPECTED_CSV_ROW_LENGTH, data.Length);
                }

                if (data[0] == "") { // skip lines with no ID set
                    continue;
                }

                var node = ScriptableObject.CreateInstance<Node>();

                node.content = data[1];
                node.speaker = ParseSpeaker(data[2]);
                node.depth = ParseDepth(data[3]);
                node.isStartOfScene = ParseBool(data[4], "isStartOfScene");
                node.backgroundFlavorContent = ParseString(data[5]);

                if (node.isStartOfScene) {
                    graph.startNode = node;
                }

                int id = ParseInt(data[0], "id");
                nodeDict.Add(id, node);
                nodes.Add(node);
            }

            // second loop: add outcomes with references to other nodes

            for (int i = 1; i < dataLines.Length; i++) { // skip first line
                string[] data = dataLines[i].Split(CSV_DELIMITER);

                if (data.Length < EXPECTED_CSV_ROW_LENGTH) {
                    continue;
                    // throw new CSVRowHasWrongLength(EXPECTED_CSV_ROW_LENGTH, data.Length);
                }

                if (data[0] == "") { // skip lines with no ID set
                    continue;
                }


                int id = ParseInt(data[0], "id");
                var node = nodeDict[id];

                node.outcomesNames = new();
                node.outcomesNodes = new();
                node.outcomes = new();

                string answer1 = ParseString(data[6]);
                string answer2 = ParseString(data[7]);

                // add outcomes

                int nextNode1_id = -1;
                int nextNode2_id = -1;

                try {
                    nextNode1_id = ParseInt(data[8], "nextNode1_id");

                    node.outcomeDecisionHandler = outcomeByRandHandler;

                    var outcome1 = ScriptableObject.CreateInstance<Outcome>();
                    outcome1.answer = answer1;
                    outcome1.nextNode = nodeDict[nextNode1_id];
                    node.outcomes.Add(outcome1);

                    node.outcomesNames.Add(outcome1.answer);
                    node.outcomesNodes.Add(outcome1.nextNode);
                } catch (CSVPropertyCouldNotBeParsedError) { }


                try {
                    nextNode2_id = ParseInt(data[9], "nextNode2_id");

                    node.outcomeDecisionHandler = outcomeByRandHandler;
                    var outcome2 = ScriptableObject.CreateInstance<Outcome>();
                    outcome2.answer = answer2;
                    outcome2.nextNode = nodeDict[nextNode2_id];
                    node.outcomes.Add(outcome2);

                    node.outcomesNames.Add(outcome2.answer);
                    node.outcomesNodes.Add(outcome2.nextNode);
                } catch (CSVPropertyCouldNotBeParsedError) { }

                // add decision handlers

                if (nextNode1_id == -1 && nextNode2_id == -1) {
                    node.outcomeDecisionHandler = outcomeByEndHandler;
                }

                if (nextNode1_id == -1 == (nextNode2_id != -1)) {
                    node.outcomeDecisionHandler = outcomeByRandHandler;
                }

                if (nextNode1_id != -1 && nextNode2_id != -1) {
                    node.outcomeDecisionHandler = outcomeByUserHandler;
                }


            }

            // save everything

            string graphDirPath = "Assets/Ressources";
            string subDirPath = "ImportCSVTest";
            string fullDirPath = graphDirPath + "/" + subDirPath + "/";

            AssetDatabase.CreateFolder(graphDirPath, subDirPath);

            AssetDatabase.CreateAsset(graph, fullDirPath + "graph" + ".asset");

            AssetDatabase.CreateAsset(outcomeByEndHandler,  fullDirPath + "outcomeByEndHandler"  + ".asset");
            AssetDatabase.CreateAsset(outcomeByUserHandler, fullDirPath + "outcomeByUserHandler" + ".asset");
            AssetDatabase.CreateAsset(outcomeByRandHandler, fullDirPath + "outcomeByRandHandler" + ".asset");

            Debug.Log("nodes loaded: " + nodes.Count);

            for(int i=0; i<nodes.Count; i++) {
                AssetDatabase.CreateAsset(nodes[i], fullDirPath + "node" + i + ".asset");
            }
        }

        static int ParseInt(string data, string propertyName) {
            try {
                return int.Parse(data);
            } catch (FormatException) {
                throw new CSVPropertyCouldNotBeParsedError(propertyName, data, "int");
            }
        }

        static string ParseString(string data) {
            return data;
        }

        static Speaker ParseSpeaker(string data) {
            try {
                return (Speaker)Enum.Parse(typeof(Speaker), data);
            } catch (ArgumentException) {
                throw new CSVPropertyCouldNotBeParsedError("Speaker", data, "SPEAKER");
            }
        }

        static Depth ParseDepth(string data) {
            try {
                return (Depth)Enum.Parse(typeof(Depth), data);
            } catch (ArgumentException) {
                throw new CSVPropertyCouldNotBeParsedError("Depth", data, "DEPTH");
            }
        }

        static bool ParseBool(string data, string propertyName) {
            try {
                if (data == "0") {
                    return false;
                }
                if (data == "1") {
                    return true;
                }
                return bool.Parse(data);
            } catch (FormatException) {
                throw new CSVPropertyCouldNotBeParsedError(propertyName, data, "bool");
            }
        }
    }
}
