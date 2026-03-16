using UnityEngine;

namespace EasyUIFrame.Frame.UI
{
    public static class UIHelper
    {
        public static T AddOrGetComponent<T>(Transform target, bool autoAdd = false) where T : Component
        {
            if (target == null)
            {
                Debug.LogError($"AddOrGetComponent<{typeof(T).Name}> failed because target is null.");
                return null;
            }

            if (target.TryGetComponent<T>(out var component))
            {
                return component;
            }

            if (!autoAdd)
            {
                Debug.LogError($"Component '{typeof(T).Name}' missing on '{target.name}'.");
                return null;
            }

            Debug.LogWarning($"Component '{typeof(T).Name}' missing on '{target.name}', auto-added.");
            return target.gameObject.AddComponent<T>();
        }

        public static T AddOrGetComponentInChild<T>(Transform root, string childName, bool autoAdd = false) where T : Component
        {
            if (root == null)
            {
                Debug.LogError($"AddOrGetComponentInChild<{typeof(T).Name}> failed because root is null.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(childName))
            {
                Debug.LogError($"AddOrGetComponentInChild<{typeof(T).Name}> failed because child name is empty.");
                return null;
            }

            var transforms = root.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i].name == childName)
                {
                    return AddOrGetComponent<T>(transforms[i], autoAdd);
                }
            }

            Debug.LogWarning($"Child '{childName}' not found under '{root.name}'.");
            return null;
        }
    }
}
