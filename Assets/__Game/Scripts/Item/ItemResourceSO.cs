using UnityEngine;

namespace Assets.__Game.Scripts.Item {
    [CreateAssetMenu(menuName = "Items/ItemResource")]
    public class ItemResourceSO : ScriptableObject {

        [field: Header("Param's")]
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        [field: Header("Carrying param's")]
        [field: SerializeField] public Vector3 CarryingPos { get; private set; }
        [field: SerializeField] public Vector3 CarryingRot { get; private set; }
    }
}