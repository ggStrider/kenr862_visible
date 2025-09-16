using Definitions.Cassettes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Gameplay.UI
{
    public class UCassetteContainer : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI Label { get; private set; }

        [field: SerializeField] public Button Button { get; private set; }
        // [field: SerializeField] public Image Image { get; private set; }
        [SerializeField] public CassetteSO CassetteSO;
    }
}