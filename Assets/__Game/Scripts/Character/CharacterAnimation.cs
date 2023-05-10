using Assets.__Game.Scripts.Animation;
using UnityEngine;

namespace Assets.__Game.Scripts.Character {
    public class CharacterAnimation : MonoBehaviour {

        [SerializeField] protected float crossfadeDuration = 0.15f;
        [SerializeField] protected float dampTime = 0.05f;

        //Private
        private Animator anim;

        private AnimationHash animHash;

        private void Awake() {
            anim = GetComponent<Animator>();

            animHash = new();
        }

        public void MovementAnim(float blendValue) {
            anim.CrossFade(animHash.movementAnimBlend, crossfadeDuration);
            anim.SetFloat("Movement_Value", blendValue, dampTime, Time.deltaTime);
        }
    }
}