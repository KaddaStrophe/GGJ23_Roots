using System;
using Cinemachine;
using TheRuinsBeneath.EventChannel;
using UnityEngine;

namespace TheRuinsBeneath.Visualization {
    public class CameraFocus : MonoBehaviour {
        [SerializeField]
        NodeEventChannel nodeEventChannel = default;
        [SerializeField]
        CinemachineTargetGroup targetGroup = default;
        [SerializeField, Range(0f, 5f)]
        float weight = 1f;
        [SerializeField, Range(0f, 200f)]
        float radius = 1f;

        [SerializeField]
        Transform currentFocus;

        protected void OnEnable() {
            if (!targetGroup) {
                targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
            }
            if (!currentFocus) {
                currentFocus = transform;
            }
            nodeEventChannel.OnNodeChange += FocusCamera;
        }
        protected void Start() {
            currentFocus = targetGroup.m_Targets[0].target;
        }
        protected void OnDisable() {
            nodeEventChannel.OnNodeChange -= FocusCamera;
        }

        void FocusCamera(Node node, GameObject gameObject) {
            if(!targetGroup.IsEmpty) {
                foreach(var target in targetGroup.m_Targets) {
                    targetGroup.RemoveMember(target.target);
                }
            }
            targetGroup.AddMember(gameObject.transform, weight, radius);
            currentFocus = gameObject.transform;
        }
    }
}