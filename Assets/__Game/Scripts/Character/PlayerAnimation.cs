using Assets.__Game.Scripts.Animation;
using Assets.__Game.Scripts.Character.Player;
using UnityEngine;

namespace Assets.__Game.Scripts.Character {
    public sealed class PlayerAnimation : MonoBehaviour {

        [SerializeField] private float crossfadeDuration = 0.15f;
        [SerializeField] private float dampTime = 0.05f;

        //Private
        private Animator anim;

        private AnimationHash animHash;
        private PlayerHandler playerHandler;

        private void Awake() {
            anim = GetComponent<Animator>();

            animHash = new();
            playerHandler = GetComponentInParent<PlayerHandler>();
        }

        private void OnEnable() {
            playerHandler.OnUpdateState += UpdateAnimation;
            PlayerMovement.OnAnimationValue += MovementAnimValues;
            PlayerMovement.OnAnimationValue += MovementCarryAnimValue;
        }

        private void UpdateAnimation(CharacterState state) {
            switch (state) {
                case CharacterState.None:
                    break;
                case CharacterState.Moving:
                    MovementAnim();
                    break;
                case CharacterState.MovingCarry:
                    MovementCarryAnim();
                    break;
            }
        }

        public void MovementAnim() {
            anim.CrossFade(animHash.movementAnimBlend, crossfadeDuration);
        }

        public void MovementCarryAnim() {
            anim.CrossFade(animHash.movementCarryAnimBlend, crossfadeDuration);
        }

        public void MovementAnimValues(float value) {
            anim.SetFloat("Movement_Value", value, dampTime, Time.deltaTime);
        }

        public void MovementCarryAnimValue(float value) {
            anim.SetFloat("Movement_Value", value, dampTime, Time.deltaTime);
        }
    }
}