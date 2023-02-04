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
            nodeEventChannel.OnNodeSpawn += FocusCamera;
        }
        protected void Start() {
            currentFocus = targetGroup.m_Targets[0].target;
        }
        protected void OnDisable() {
            nodeEventChannel.OnNodeSpawn -= FocusCamera;
        }

        void FocusCamera(GameObject gameObject) {
            targetGroup.RemoveMember(currentFocus);
            targetGroup.AddMember(gameObject.transform, weight, radius);
            currentFocus = gameObject.transform;
        }
    }
}