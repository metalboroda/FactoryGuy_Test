using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Item {
    public class ItemResource : MonoBehaviour {

        //Events
        public static event Action<ItemResource> OnItemDrop;

        [field: Header("Param's")]
        [field: SerializeField] public ItemResourceSO ItemResourceSO { get; private set; }
        [field: SerializeField] public float ModelHeight { get; private set; }

        //Private
        private Collider coll;

        private void Awake() {
            coll = GetComponent<Collider>();
        }

        private void Start() {
            ModelHeight = GetItemWidth();
        }

        public void MoveToFactory() {
            coll.enabled = false;
            OnItemDrop?.Invoke(this);
        }

        #region GetItemHeight
        private float GetItemWidth() {
            Bounds bounds = GetModelBounds();
            float height = bounds.size.x;

            return height;
        }

        private Bounds GetModelBounds() {
            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

            return meshRenderer.bounds;
        }
        #endregion
    }
}