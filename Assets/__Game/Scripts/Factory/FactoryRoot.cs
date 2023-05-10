using Assets.__Game.Scripts.Factory.Interface;
using UnityEngine;

namespace Assets.__Game.Scripts.Factory {
    public sealed class FactoryRoot : IFactory {

        //Private
        private readonly GameObject prefab;

        public FactoryRoot(GameObject prefab) {
            this.prefab = prefab;
        }

        public GameObject Produce() {
            return Object.Instantiate(prefab);
        }
    }
}