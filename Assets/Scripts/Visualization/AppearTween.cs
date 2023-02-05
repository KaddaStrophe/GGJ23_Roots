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

        bool finishedAnimation = false;
        LTDescr savedTween;

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
            finishedAnimation = false;
            // TODO: Delay abhängig vom Textfortschritt
            LeanTween.alpha(rectTransform, 0f, 0f);
            savedTween = LeanTween.alpha(rectTransform, 1f, time).setFrom(0f).setDelay(delay).setEase(easeType);
        }

        public void SetDelay(float animationDelay) {
            delay = animationDelay;
        }

        public void GreyOut() {
            LeanTween.alpha(rectTransform, greyScale, 0.2f);
            LeanTween.alpha(rectTransform, greyScale, 0.2f).setFrom(1f).setEase(LeanTweenType.easeInBack);
        }

        public bool FinishAnimation() {
            // If already finished return false
            if (finishedAnimation) {
                return false;
            }
            // If animation has to be finished right now, do that and then return true
            LeanTween.cancel(savedTween.id);
            LeanTween.alpha(rectTransform, 1f, 0.01f);
            return true;
        }

        public bool GetIsFinished() {
            return finishedAnimation;
        }
    }
}