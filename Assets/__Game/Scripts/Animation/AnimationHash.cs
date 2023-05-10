using UnityEngine;

namespace Assets.__Game.Scripts.Animation {
    public sealed class AnimationHash : MonoBehaviour {

        public readonly int movementAnimBlend = Animator.StringToHash("Movement_Blend");
        public readonly int movementCarryAnimBlend = Animator.StringToHash("Movement_Carry_Blend");
    }
}