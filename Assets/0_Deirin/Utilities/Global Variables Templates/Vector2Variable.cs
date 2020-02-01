using UnityEngine;

[CreateAssetMenu]
public class Vector2Variable : ScriptableObject {
    [SerializeField] Vector2 value;

    public Vector2 Value { get => value; set { this.value = value; OnValueChanged?.Invoke( Value ); } }
    public System.Action<Vector2> OnValueChanged;

    private void OnValidate () {
        OnValueChanged?.Invoke( Value );
    }
}