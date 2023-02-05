using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_SpeakerTextStyle_New", menuName = "TextStyle/SpeakerTextStyle")]
public class SpeakerTextStyle : ScriptableObject {
    [SerializeField]
    public FontStyles style = default;
    [SerializeField]
    public Color color = default;
}
