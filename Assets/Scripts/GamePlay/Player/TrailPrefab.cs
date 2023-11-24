using UnityEngine;

public class TrailPrefab : MonoBehaviour
{
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    [field: SerializeField] public MeshFilter MeshFilter { get; private set; }
}
