using Assets.__Game.Scripts.Item;
using Assets.__Game.Scripts.Machines.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.__Game.Scripts.Machines {
    public sealed class Factory : MonoBehaviour {

        [Header("Param's")]
        [SerializeField] private float produceInterval = 5f;
        [SerializeField] private ItemResourceSO itemToReceive;
        [SerializeField] private ItemResourceSO itemToProduce;
        [SerializeField] private List<Transform> receivedItemPoints = new();
        [SerializeField] private List<Transform> producedItemPoints = new();

        //Private
        private List<GameObject> receivedItems = new();
        private List<GameObject> producedItems = new();
        private float tick;

        private IFactory factory;

        private void Awake() {
            factory = new FactoryRoot(itemToProduce.Prefab);
        }

        private void Update() {
            ProduceTimer();
        }

        public void ReceiveItem(ItemResource item) {
            if (item.ItemResourceSO.Name == itemToReceive.Name) {
                foreach (var i in receivedItemPoints) {
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

        #region Production
        private void ProduceTimer() {
            if (receivedItems.Count > 0) {
                tick += Time.deltaTime;

                if (tick >= produceInterval) {
                    ProduceItem();

                    tick = 0;
                }
            }
        }

        private void ProduceItem() {
            try {
                foreach (var unused in receivedItems) {
                    foreach (var j in producedItemPoints) {
                        if (j.childCount == 0) {

                            //Produce
                            var producedItem = factory.Produce();
                            producedItem.transform.SetParent(j);
                            producedItem.transform.position = j.position;
                            producedItems.Add(producedItem);

                            RemoveLastReceivedItem();

                            break;
                        }
                    }
                }
            } catch (System.InvalidOperationException) {
            }
        }
        #endregion

        private void RemoveLastReceivedItem() {
            var item = receivedItems.Last();
            receivedItems.Remove(item);
            item.transform.SetParent(null);
            Destroy(item);
        }
    }
}