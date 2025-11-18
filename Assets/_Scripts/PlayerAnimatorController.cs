using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        public Animator animator;

        private static PlayerAnimatorController instance;
        public static PlayerAnimatorController Instance
        {
            get
            {
                return instance;
            }
            set
            {
                if (instance == null) { instance = value; }
            }


        }

        private void Awake()
        {
            Instance = this;
        }
        public void MoveRight()
        {
            ResetAllMovementBools();
            animator.SetBool(AnimationNames.MoveRight, true);
        }

        public void MoveLeft()
        {
            ResetAllMovementBools();
            animator.SetBool(AnimationNames.MoveLeft, true);
        }

        public void Walk()
        {
            ResetAllMovementBools();
            animator.SetBool(AnimationNames.Walk, true);
        }

        public void RotateLeft()
        {
            ResetAllMovementBools();
            animator.SetBool(AnimationNames.LookLeft, true);
        }

        public void RotateRight()
        {
            ResetAllMovementBools();
            animator.SetBool(AnimationNames.LookRight, true);
        }

        public void Idle()
        {
            ResetAllMovementBools();
            animator.SetBool(AnimationNames.Idle, true);
        }

        private void ResetAllMovementBools()
        {
            animator.SetBool(AnimationNames.Idle, false);
            animator.SetBool(AnimationNames.Walk, false);
            animator.SetBool(AnimationNames.LookLeft, false);
            animator.SetBool(AnimationNames.LookRight, false);
            animator.SetBool(AnimationNames.MoveLeft, false);
            animator.SetBool(AnimationNames.MoveRight, false);
        }

    }
}

