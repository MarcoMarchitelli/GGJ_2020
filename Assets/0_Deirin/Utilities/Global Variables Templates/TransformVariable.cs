using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TransformVariable : ScriptableObject {
    [SerializeField] Transform value;

    public Transform Value { get => value; set { this.value = value; OnValueChanged?.Invoke( Value ); } }
    public System.Action<Transform> OnValueChanged;

    private void OnValidate () {
        OnValueChanged?.Invoke( Value );
    }
}