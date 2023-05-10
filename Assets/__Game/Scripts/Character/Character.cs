using UnityEngine;

namespace Assets.__Game.Scripts.Character {
    public class Character : MonoBehaviour {

        public CharacterStateController characterStateController;

        private void Awake() {
            characterStateController = new();
            characterStateController.UpdateState(CharacterState.Moving);
        }
    }
}