using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets._Scripts
{
    public class PlayerMovement:Movement
    {
   
        Quaternion headInitialRotation;
        private bool wasMoving = false;

        public override void Start()
        {
            base.Start();
            headInitialRotation = headLook.transform.rotation;
        }
        public override void Update()
        {
            base.Update();
            // Check if we just stopped moving
            if (wasMoving && !isMoving)
            {

                // RESET ROTATION INPUT after movement completes
                keyBoardNormal = 0;
                PlayerAnimatorController.Instance.Idle();
            }
            if (isMoving)
            {
                // Character is moving - play movement animation
                if (keyboardInput.x == 1)
                {
                    AudioManager.Instance.Play("walk");

                    PlayerAnimatorController.Instance.MoveRight();
                }
                else if (keyboardInput.x == -1)
                {
                    AudioManager.Instance.Play("walk");

                    PlayerAnimatorController.Instance.MoveLeft();
                }
                else if (keyboardInput.y == 1 || keyboardInput.y == -1)
                {
                    headLook.transform.rotation = headInitialRotation;
                    AudioManager.Instance.Play("walk");
                    PlayerAnimatorController.Instance.Walk();
                }
            }
            else
            {
                // Not moving - handle rotation only if there's rotation input
                if (keyBoardNormal != 0)
                {
                    Rotate();
                    AudioManager.Instance.Play("rotate");

                }
            }
            // Update wasMoving for next frame
            wasMoving = isMoving;
        }
        void Rotate()
        {
            if (keyBoardNormal == 1)
            {
                headLook.transform.rotation = Quaternion.Euler(0, 0,-90f);
                PlayerAnimatorController.Instance.RotateRight();
            }
            else if (keyBoardNormal == -1)
            {
                headLook.transform.rotation = Quaternion.Euler(0, 0, 90f);
                PlayerAnimatorController.Instance.RotateLeft();
                
            }
            else
            {
                headLook.transform.rotation = headInitialRotation;
                // No rotation input - go to idle
                PlayerAnimatorController.Instance.Idle();
            }
            
        }
        void Move()
        {
           
            if (isMoving)
            {
                if (keyboardInput.x == 1)
                {
                    PlayerAnimatorController.Instance.MoveRight();
                }
                else if (keyboardInput.x == -1)
                {
                    PlayerAnimatorController.Instance.MoveLeft();
                }
                else if (keyboardInput.y == 1 || keyboardInput.y == -1)
                {
                    PlayerAnimatorController.Instance.Walk();
                    
                }
            }
            else
            {
               AudioManager.Instance.Stop();
                // When not moving, allow rotation animations
                Rotate();
                AudioManager.Instance.Play("rotate");

            }
        }


    }
    

}
