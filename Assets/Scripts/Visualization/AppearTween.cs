using System;
using TMPro;
using UnityEngine;

namespace TheRuinsBeneath.Visualization {
    public class AppearTween : MonoBehaviour, ITween {
        [SerializeField]
        NodeBox nodeBox = default;
        [SerializeField, Range(0f, 100f)]
        float time = 6.0f;
        [SerializeField, Range(0f, 10f)]
        float delay = 2.0f;
        [SerializeField]
        RectTransform rectTransform = default;
        [SerializeField]
        TextMeshProUGUI textToAppear = default;
        [SerializeField]
        LeanTweenType easeType = LeanTweenType.linear;

        protected void OnValidate() {
            if (!rectTransform) {
                TryGetComponent(out rectTransform);
            }
            if (!textToAppear) {
                textToAppear = GetComponentInChildren<TextMeshProUGUI>();
            }
            if (!nodeBox) {
                GetComponentInParent<NodeBox>();
            }
        }

        protected void Start() {
            //delay = nodeBox.animationDelay;
            Appear();
        }

        void Appear() {
            // TODO: Delay abhängig vom Textfortschritt
            //LeanTween.alpha(textToAppear.rectTransform, 0f, 0.1f);
            LeanTween.alpha(rectTransform, 0f, 0.1f);

            //LeanTween.alpha(textToAppear.rectTransform, 1f, time).setFrom(0f).setDelay(delay).setEase(easeType);
            LeanTween.alpha(rectTransform, 1f, time).setFrom(0f).setDelay(delay).setEase(easeType);
        }

        public void SetDelay(float animationDelay) {
            delay = animationDelay;
        }
    }
}