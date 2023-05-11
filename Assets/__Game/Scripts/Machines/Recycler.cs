using Assets.__Game.Scripts.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.__Game.Scripts.Machines {
    public sealed class Recycler : MonoBehaviour {

        //Private
        private List<GameObject> receivedItems = new();

        public void ReceiveItem(ItemResource item) {

            item.transform.SetParent(transform);

            //Rot & pos
            item.transform.position = transform.position;
            item.transform.localRotation = Quaternion.Euler(transform.localRotation.x,
                transform.localRotation.y, transform.localRotation.z);

            //Disable item collider
            item.coll.enabled = false;

            //Add item to list
            receivedItems.Add(item.gameObject);

            item.ItemDropped();

            //Destroy item
            receivedItems.Remove(item.gameObject);
            Destroy(item.gameObject);
        }
    }
}