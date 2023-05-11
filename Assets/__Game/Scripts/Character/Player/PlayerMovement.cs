using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Character.Player {
    public sealed class PlayerMovement : CharacterMovement {

        //Events
        public static event Action<float> OnAnimationValue;

        //Private
        private Vector3 moveDir;

        private CharacterController characterController;
        private Joystick joystick;

        private PlayerHandler player;
        private PlayerAnimation characterAnimation;

        private void Awake() {
            characterController = GetComponent<CharacterController>();
            joystick = GetComponentInChildren<Joystick>();

            player = GetComponent<PlayerHandler>();
            characterAnimation = GetComponentInChildren<PlayerAnimation>();
        }

        private void Update() {
            Movement();
        }

        private void Movement() {
            if (player.CharacterState == CharacterState.Moving ||
               player.CharacterState == CharacterState.MovingCarry) {

                //Move character controller
                moveDir = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
                characterController.Move(movementSpeed * Time.deltaTime * moveDir);

                //Rotate 
                transform.forward = Vector3.Slerp(transform.forward,
                    moveDir, Time.deltaTime * rotationSpeed);

                OnAnimationValue?.Invoke(moveDir.magnitude);
            }
        }
    }
}