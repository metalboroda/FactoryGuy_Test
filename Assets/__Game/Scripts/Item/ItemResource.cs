using Assets.__Game.Scripts.Machines;
using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Item {
    public sealed class ItemResource : MonoBehaviour {

        //Events
        public static event Action<ItemResource> OnItemDrop;

        [field: Header("Param's")]
        [field: SerializeField] public ItemResourceSO ItemResourceSO { get; private set; }
        public float ModelHeight { get; private set; }

        //Hidden
        [HideInInspector] public Collider coll;

        private void Awake() {
            coll = GetComponent<Collider>();
        }

        private void Start() {
            ModelHeight = GetItemWidth();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out Factory factory)) {
                factory.ReceiveItem(this);
            }

            if (other.TryGetComponent(out Recycler recycler)) {
                recycler.ReceiveItem(this);
            }

            if (other.TryGetComponent(out Table table)) {
                table.ReceiveItem(this);
            }
        }

        public void ItemDropped() {
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