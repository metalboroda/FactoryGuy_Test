using Assets.__Game.Scripts.Factory.Interface;
using Assets.__Game.Scripts.Item;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.__Game.Scripts.Factory {
    public class Factory : MonoBehaviour {

        [SerializeField] private ItemResourceSO itemToReceive;
        [SerializeField] private ItemResourceSO itemToProduce;
        [SerializeField] private List<Transform> receivedItemPoints = new();
        [SerializeField] private List<Transform> producedItemPoints = new();
        [SerializeField] private List<GameObject> receivedItems = new();
        [SerializeField] private List<GameObject> producedItems = new();

        //Private
        private float tick;
        private float produceInterval = 5f;

        private IFactory factory;

        private void Awake() {
            factory = new FactoryRoot(itemToProduce.Prefab);
        }

        private void Update() {
            ProduceTimer();
        }

        private void OnTriggerEnter(Collider other) {
            ReceiveItem(other);
        }

        private void ReceiveItem(Collider other) {
            if (other.TryGetComponent(out ItemResource item)) {
                if (item.ItemResourceSO.Name == itemToReceive.Name) {
                    foreach (var i in receivedItemPoints) {
                        if (i.childCount > 0) continue;

                        item.transform.SetParent(i);
                        item.transform.position = i.position;
                        receivedItems.Add(item.gameObject);
                        break;
                    }
                }
            }
        }

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
                foreach (var i in receivedItems) {
                    foreach (var j in producedItemPoints) {
                        if (j.childCount == 0) {
                            var producedItem = factory.Produce();
                            producedItem.transform.SetParent(j);
                            producedItem.transform.position = j.position;

                            RemoveLastReceivedItem();
                            break;
                        }
                    }
                }
            } catch (System.InvalidOperationException) {
                //throw;
            }
        }

        private void RemoveLastReceivedItem() {
            var item = receivedItems.Last();
            receivedItems.Remove(item);
            item.transform.SetParent(null);
            Destroy(item);
        }
    }
}