using UnityEngine;

namespace Assets.__Game.Scripts.Item {
    public class ItemResource : MonoBehaviour {

        [field: SerializeField] public ItemResourceSO ItemResourceSO { get; private set; }
    }
}