using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class NpcAnimatorBase:MonoBehaviour

    {
        public Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void  SetBool(string name, bool value)
        {
            animator.SetBool(name, value);
        }
        public void 
         Idle(string name, bool value)
        {
            animator.SetBool(name, value);
        }
        public void LookLeft(string name, bool value)
        {
            animator.SetBool(name, value);
        }
        public void LookRight(string name, bool value)
        {
            animator.SetBool(name, value);
        }
        public void Reset()
        {
            animator.SetBool(AnimationNames.Idle, false);
            animator.SetBool(AnimationNames.Walk, false);
            animator.SetBool(AnimationNames.LookLeft, false);
            animator.SetBool(AnimationNames.LookRight, false);
            //animator.SetBool(AnimationNames.MoveLeft, false);
            //animator.SetBool(AnimationNames.MoveRight, false);
        }
    }
}
