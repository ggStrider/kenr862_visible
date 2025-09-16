using UnityEngine;
using UnityEngine.Video;

namespace Definitions.Cassettes
{
    [CreateAssetMenu(fileName = "New Cassette", menuName = 
        StaticKeys.PROJECT_NAME + "/Cassette")]
    public class CassetteSO : ScriptableObject
    {
        [field: SerializeField] public string CassetteName { get; private set; } = "Fish";
        [field: SerializeField] public VideoClip VideoClip { get; private set; }
    }
}