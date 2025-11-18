using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{

    public class ElevatorDoor:MonoBehaviour
    {
        public Animator animator;
        private void OnEnable()
        {
            ElevatorSceneSquencer.OnElevatorDoorOpen += ElevatorSceneSquencer_OnElevatorDoorOpen;
            ElevatorSceneSquencer.OnElevatorDoorClose += ElevatorSceneSquencer_OnElevatorDoorClose;
        }

        private void ElevatorSceneSquencer_OnElevatorDoorClose(object sender, EventArgs e)
        {
                      animator.SetTrigger("Close");
        }

        private void ElevatorSceneSquencer_OnElevatorDoorOpen(object sender, EventArgs e)
        {
            AudioManager.Instance.Play("elevator_door_open");
            animator.SetTrigger("Open");
        }
    }
}
