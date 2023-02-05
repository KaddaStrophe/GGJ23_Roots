using System;
using TMPro;
using UnityEngine;

namespace TheRuinsBeneath.Visualization {
    public class TextTween : MonoBehaviour, ITween {
        [SerializeField]
        NodeBox nodeBox = default;
        [SerializeField, Range(0f, 100f)]
        float typingSpeed = 6.0f;
        [SerializeField, Range(0f, 10f)]
        float delay = 2.0f;
        [SerializeField, Range(0f, 1f)]
        float greyScale = 0.2f;
        [SerializeField]
        TextMeshProUGUI contentText = default;
        [SerializeField]
        LeanTweenType easeType = LeanTweenType.easeOutQuad;

        protected void OnValidate() {
            if (!contentText) {
                TryGetComponent(out contentText);
            }
            if(!nodeBox) {
                GetComponentInParent<NodeBox>();
            }
        }

        protected void Start() {
            delay = nodeBox.animationDelay;
            StartTyping();
        }

        public void StartTyping() {
            // TODO: Delay abhängig vom Abstand des Kästchens
            string origText = contentText.text;
            int charCount = origText.Length;
            float time = charCount / typingSpeed;
            contentText.text = "";
            LeanTween.value(gameObject, 0, origText.Length, time).setEase(easeType).setOnUpdate((float val) => {
                contentText.text = origText.Substring(0, Mathf.RoundToInt(val));
            }).setDelay(delay);
        }

        public void SetDelay(float animationDelay) {
            delay = animationDelay;
        }

        public void GreyOut() {
            contentText.alpha = greyScale;
        }
    }
}