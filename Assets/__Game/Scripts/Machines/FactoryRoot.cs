using Assets.__Game.Scripts.Machines.Interface;
using UnityEngine;

namespace Assets.__Game.Scripts.Machines {
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