using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace Assets._Scripts
{
    
    public abstract class NpcBase : MonoBehaviour
    {
      public  NpcAnimatorBase npcAnimatorBase;
        public Vector3 initialPosiotion;
        public Vector3 targetPositon;
        public HeadLook HeadLook;
        public float speed = 2f;
        public Grid grid;
        private bool isMoving = false;
        private Coroutine movementCoroutine;

        protected virtual void Start()
        {
            grid = GridCreator.grid;
            if (grid == null)
            {
                Debug.LogWarning("Grid is null in NpcBase.Start()");
            }
        }

        public virtual void Move(int targetX, int targetY)
        {
            if (grid != null)
            {
                if (targetX < grid.width && targetY < grid.height && targetX >= 0 && targetY >= 0)
                {
                    // Get the world-space center of the target grid cell
                    Vector3 targetWorldPosition = grid.GetWorldPosition(targetX, targetY) + new Vector3(grid.cellSize, grid.cellSize) * 0.5f;

                    // Stop any existing movement
                    if (movementCoroutine != null)
                        StopCoroutine(movementCoroutine);

                    // Start smooth movement
                    movementCoroutine = StartCoroutine(MoveToPosition(targetWorldPosition));
                    targetPositon = new Vector3(targetX, targetY); // Store the grid coordinates
                }
                else
                {
                    Debug.LogWarning($"Target position ({targetX}, {targetY}) outside grid bounds");
                }
            }
            else
            {
                Debug.LogWarning("Grid is null in NpcBase.Move()");
            }
        }

        private IEnumerator MoveToPosition(Vector3 targetPosition)
        {
            isMoving = true;
            Vector3 startPosition = transform.position;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float duration = distance / speed;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                // Smooth lerp movement
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            // Snap to exact position at the end
            transform.position = targetPosition;
            isMoving = false;
            movementCoroutine = null;
        }

        public bool IsMoving() => isMoving;

        public void StopMovement()
        {
            if (movementCoroutine != null)
            {
                StopCoroutine(movementCoroutine);
                movementCoroutine = null;
            }
            isMoving = false;
        }

        public abstract void RotateLeft(float value);
        public abstract void RotateRight(float value);
    }
}