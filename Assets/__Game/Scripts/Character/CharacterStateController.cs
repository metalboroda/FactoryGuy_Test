using System;

namespace Assets.__Game.Scripts.Character {
    public sealed class CharacterStateController {

        //Events
        public event Action<CharacterState> OnUpdateState;

        public CharacterState CharacterState { get; private set; } = CharacterState.None;

        public void UpdateState(CharacterState newState) {
            if (newState != CharacterState) {
                CharacterState = newState;
            }
        }
    }

    public enum CharacterState {
        None,
        Moving,
        MovingCarry
    }
}
