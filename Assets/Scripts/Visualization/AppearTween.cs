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
        [SerializeField, Range(0f, 1f)]
        float greyScale = 0.2f;
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
            StartAppearing();
        }

        public void StartAppearing() {
            // TODO: Delay abh�ngig vom Textfortschritt
            //LeanTween.alpha(textToAppear.rectTransform, 0f, 0.1f);
            LeanTween.alpha(rectTransform, 0f, 0f);

            //LeanTween.alpha(textToAppear.rectTransform, 1f, time).setFrom(0f).setDelay(delay).setEase(easeType);
            LeanTween.alpha(rectTransform, 1f, time).setFrom(0f).setDelay(delay).setEase(easeType);
        }

        public void SetDelay(float animationDelay) {
            delay = animationDelay;
        }

        public void GreyOut() {
            LeanTween.alpha(rectTransform, greyScale, 0.2f);
            LeanTween.alpha(rectTransform, greyScale, 0.2f).setFrom(1f).setEase(LeanTweenType.easeInBack);
        }
    }
}