using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlay
{
    public class AbilityHelpElement : MonoBehaviour
    {
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TextMesh { get; private set; }
    }
}