using Assets.__Game.Scripts.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.__Game.Scripts.Character.Player {
    public sealed class PlayerHandler : Character {

        [Header("Item holder param's")]
        [SerializeField] private Transform itemHolder;
        [SerializeField] private int maxCapacity = 6;
        [SerializeField] private List<ItemResource> pickedItems = new();

        //Private
        private Vector3 stackPos;

        private void Start() {
            UpdateState(CharacterState.Moving);
        }

        private void OnTriggerEnter(Collider other) {
            PickItem(other);
        }

        private void OnEnable() {
            ItemResource.OnItemDrop += DropItem;
        }

        private void PickItem(Collider other) {
            if (pickedItems.Count >= maxCapacity) return;

            if (other.TryGetComponent(out ItemResource item)) {
                if (!pickedItems.Contains(item)) {

                    //Set parent and trans, rot
                    item.transform.SetParent(itemHolder);
                    item.transform.SetLocalPositionAndRotation(stackPos + item.ItemResourceSO.CarryingPos, Quaternion.identity);
                    item.transform.localRotation = Quaternion.Euler(itemHolder.transform.localRotation.x,
                        itemHolder.transform.localRotation.y, item.ItemResourceSO.CarryingRot.z);

                    //Add item to list
                    pickedItems.Add(item);

                    //Up stack height
                    stackPos.y += item.ModelHeight;
                }
            }

            CheckPickedItems();
        }

        private void DropItem(ItemResource item) {
            if (pickedItems.Count == 0) return;

            stackPos.y -= item.ModelHeight;

            pickedItems.Remove(item);

            foreach (var i in pickedItems) {
                i.transform.localPosition = new Vector3(i.transform.localPosition.x,
                    i.transform.localPosition.y - stackPos.y,
                    i.transform.localPosition.z);
            }

            CheckPickedItems();
        }

        private void CheckPickedItems() {
            if (pickedItems.Count > 0) {
                UpdateState(CharacterState.MovingCarry);
            } else {
                UpdateState(CharacterState.Moving);
            }
        }
    }
}