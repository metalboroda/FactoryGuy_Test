using UnityEngine;

namespace Assets.__Game.Scripts.Character.Player {
    public class PlayerMovement : CharacterMovement {

        //Private
        private CharacterController characterController;
        private Joystick joystick;

        private Character character;
        private CharacterAnimation characterAnimation;

        private void Awake() {
            characterController = GetComponent<CharacterController>();
            joystick = GetComponentInChildren<Joystick>();

            character = GetComponent<Character>();
            characterAnimation = GetComponentInChildren<CharacterAnimation>();
        }

        private void Update() {
            Movement();
        }

        private void Movement() {
            if (character.characterStateController.CharacterState != CharacterState.Moving) return;

            //Move character controller
            Vector3 moveDir = new(joystick.Horizontal, 0, joystick.Vertical);
            characterController.Move(movementSpeed * Time.deltaTime * moveDir);

            //Rotate 
            transform.forward = Vector3.Slerp(transform.forward,
                moveDir, Time.deltaTime * rotationSpeed);

            characterAnimation.MovementAnim(moveDir.magnitude);
        }
    }
}