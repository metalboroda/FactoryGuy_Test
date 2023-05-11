using Assets.__Game.Scripts.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.__Game.Scripts.Machines {
    public sealed class Table : MonoBehaviour {

        [Header("Param's")]
        [SerializeField] private ItemResourceSO itemToReceive;
        [SerializeField] private List<Transform> stackPoints = new();

        //Private
        private List<GameObject> receivedItems = new();

        public void ReceiveItem(ItemResource item) {
            if (item.ItemResourceSO.Name == itemToReceive.Name) {
                foreach (var i in stackPoints) {
                    if (i.childCount > 0) continue;

                    item.transform.SetParent(i);

                    //Rot & pos
                    item.transform.position = i.position;
                    item.transform.localRotation = Quaternion.Euler(i.transform.localRotation.x,
                        i.transform.localRotation.y, i.transform.localRotation.z);

                    //Disable item collider
                    item.coll.enabled = false;

                    //Add item to list
                    receivedItems.Add(item.gameObject);

                    item.ItemDropped();

                    break;
                }
            }
        }
    }
}