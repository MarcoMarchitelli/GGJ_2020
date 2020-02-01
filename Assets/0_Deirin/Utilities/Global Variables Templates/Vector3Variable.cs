using UnityEngine;

[CreateAssetMenu]
public class Vector3Variable : ScriptableObject {
    [SerializeField] Vector3 value;

    public Vector3 Value { get => value; set { this.value = value; OnValueChanged?.Invoke( Value ); } }
    public System.Action<Vector3> OnValueChanged;

    private void OnValidate () {
        OnValueChanged?.Invoke( Value );
    }
}