using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Character {
    public class Character : MonoBehaviour {

        //Events
        public event Action<CharacterState> OnUpdateState;

        public CharacterState CharacterState { get; private set; } = CharacterState.None;

        public void UpdateState(CharacterState newState) {
            if (newState != CharacterState) {
                CharacterState = newState;

                OnUpdateState?.Invoke(CharacterState);
            }
        }
    }

    public enum CharacterState {
        None,
        Moving,
        MovingCarry
    }
}