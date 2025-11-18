using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Assets._Scripts
{
    public class NpcNormal : NpcBase
    {
        public bool limitLeft;
        public bool limitRight;
        public float leftRotValue;
        public float rightRotValue;
        public Vector3 initialRotation;
        bool routineStarted;
        int Xcoordinate;
        int Ycoordinate;
        public enum HeadLimits
        {
            None,
            Left, Right,
        }
        public HeadLimits headLimits;
  

        protected override void Start()
        {
            base.Start();
            initialRotation= HeadLook.transform.localEulerAngles;
           // Debug.Log("NPC Moving");
        }
        public override void Move(int targetX, int targetY)
        {
            base.Move(targetX, targetY);
            if(!routineStarted)
            {
                routineStarted = true;
                StartCoroutine(HeadRotationRoutine());
            }
            // StartCoroutine(HeadRotationRoutine());
           // Debug.Log("NPC Moving");
          //  npcAnimatorBase?.Walk(AnimationNames.Walk, true);
           
        }
        public override void RotateLeft(float value)
        {
            if (HeadLook != null)
            {
             //   Debug.Log("Rotating Left");
                HeadLook.Rotate(value);
            }
        }

        public override void RotateRight(float value)
        {
            if (HeadLook != null)
            {
                HeadLook.Rotate(value);
            }
        }
        void walk()
        {
            npcAnimatorBase?.Reset();
            npcAnimatorBase?.SetBool(AnimationNames.Walk, true);
           

        }
        void idle()
        {
            npcAnimatorBase?.Reset();
            npcAnimatorBase?.SetBool(AnimationNames.Idle, true);
          

        }
        void lookLeft()
        {
            npcAnimatorBase?.Reset();
            npcAnimatorBase?.SetBool(AnimationNames.LookLeft, true);
       

        }

        void lookRight()
        {
            npcAnimatorBase?.Reset();
            npcAnimatorBase?.SetBool(AnimationNames.LookRight, true);
            
        }
      
        IEnumerator HeadRotationRoutine()
        {
        //    Debug.Log("Starting Head Rotation Routine");
            yield return new WaitForSeconds(5);
            switch(headLimits)
            {
                    case HeadLimits.Left:
                    RotateRight(rightRotValue);
                    lookRight();
                    yield return new WaitForSeconds(7);
                    RotateRight(initialRotation.z);
                    idle();
                    break;
                    case HeadLimits.Right:
                    lookRight();
                    RotateLeft(leftRotValue);
                    yield return new WaitForSeconds(7);
                    RotateLeft(initialRotation.z);
                    idle();  
                    break;
                    case HeadLimits.None:
                    RotateLeft(leftRotValue);
                    Debug.Log("Rotating Left");
                    yield return new WaitForSeconds(7);
                    Debug.Log("Rotating to Initial");
                    RotateLeft(initialRotation.z);
                    idle();
                    yield return new WaitForSeconds(7);
                    Debug.Log("Rotating Right");
                    RotateRight(rightRotValue);
                    lookRight();
                  
                    break;

            }
            StartCoroutine(HeadRotationRoutine());

        }
    }
}