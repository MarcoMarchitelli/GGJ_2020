namespace Deirin.Utilities {
    using System;
    using UnityEngine.Events;
    using UnityEngine;

    public static class CustomUnityEvents {}

    [Serializable]
    public class UnityFloatEvent : UnityEvent<float> { }

    [Serializable]
    public class UnityComponentEvent : UnityEvent<Component> { }

    [Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
}