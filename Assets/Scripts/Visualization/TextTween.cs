using System;
using TMPro;
using UnityEngine;

namespace TheRuinsBeneath.Visualization {
    public class TextTween : MonoBehaviour {
        [SerializeField, Range(0f, 100f)]
        float typingSpeed = 6.0f;
        [SerializeField, Range(0f, 10f)]
        float delay = 2.0f;
        [SerializeField]
        TextMeshProUGUI contentText = default;
        [SerializeField]
        LeanTweenType easeType = LeanTweenType.easeOutQuad;

        protected void OnValidate() {
            if (!contentText) {
                TryGetComponent(out contentText);
            }
        }

        protected void Start() {
            StartTyping();
        }

        void StartTyping() {
            // TODO: Delay abhängig vom Abstand des Kästchens
            string origText = contentText.text;
            int charCount = origText.Length;
            float time = charCount / typingSpeed;
            Debug.Log("Count: " + charCount + ", time: " + time);
            contentText.text = "";
            LeanTween.value(gameObject, 0, origText.Length, time).setEase(easeType).setOnUpdate((float val) => {
                contentText.text = origText.Substring(0, Mathf.RoundToInt(val));
            }).setDelay(delay);
        }
    }
}