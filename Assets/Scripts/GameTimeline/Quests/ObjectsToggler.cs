using UnityEngine;

namespace GameTimeline.Quests
{
    public static class ObjectsToggler
    {
        private const string INTERACT_LAYER_NAME = "Interact";
        private const string DEFAULT_LAYER_NAME = "Default";
        

        public static void EnableObject(GameObject targetObject)
        {
            if (targetObject != null)
                targetObject.SetActive(true);
        }

        public static void DisableObject(GameObject targetObject)
        {
            if (targetObject != null)
                targetObject.SetActive(false);
        }

        public static void SetLayerToInteract(GameObject targetObject)
        {
            if (targetObject != null)
            {
                int interactLayer = LayerMask.NameToLayer(INTERACT_LAYER_NAME);
                if (interactLayer != -1)
                    targetObject.layer = interactLayer;
            }
        }
        
        public static void SetLayerToDefault(GameObject targetObject)
        {
            if (targetObject != null)
            {
                int defaultLayer = LayerMask.NameToLayer(DEFAULT_LAYER_NAME);
                if (defaultLayer != -1)
                    targetObject.layer = defaultLayer;
            }
        }

        public static void SetLayer(GameObject targetObject, int layer)
        {
            if (targetObject != null)
                targetObject.layer = layer;
        }

        public static void ToggleDoorGameObject(GameObject doorToEnable, GameObject doorToDisable)
        {
            EnableObject(doorToEnable);
            DisableObject(doorToDisable);
        }
    }
}