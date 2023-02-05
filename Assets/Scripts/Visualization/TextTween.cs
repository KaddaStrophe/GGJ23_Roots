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
        
        bool finishedAnimation = false;
        LTDescr savedTween;
        string origText;

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
            finishedAnimation = false;
            // TODO: Delay abhängig vom Abstand des Kästchens
            origText = contentText.text;
            int charCount = origText.Length;
            float time = charCount / typingSpeed;
            contentText.text = "";
            savedTween = LeanTween.value(gameObject, 0, origText.Length, time).setEase(easeType).setOnUpdate((float val) => {
                contentText.text = origText.Substring(0, Mathf.RoundToInt(val));
            }).setDelay(delay).setOnComplete(SetAnimationFinished);
        }

        void SetAnimationFinished() {
            finishedAnimation = true;
        }

        public void SetDelay(float animationDelay) {
            delay = animationDelay;
        }

        public void GreyOut() {
            contentText.alpha = greyScale;
        }

        public bool FinishAnimation() {
            // If already finished return false
            if(finishedAnimation) {
                return false;
            }
            // If animation has to be finished right now, do that and then return true
            LeanTween.cancel(savedTween.id);
            contentText.text = origText;
            SetAnimationFinished();
            return true;
        }

        public bool GetIsFinished() {
            return finishedAnimation;
        }
    }
}