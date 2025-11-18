using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Scripts
{
    public class ElevatorSceneSquencer : MonoBehaviour
    {
        public NpcBase[] elevatorNpc;
        public Action currentTask;
        ElevatorDoor elevatorDoor;
        public static event EventHandler OnElevatorDoorOpen;
        public static event EventHandler OnElevatorDoorClose;
        public float Npc1Enterance;
        public float Npc2Enterance;
        public float Npc3Enterance;
        public float Npc4Enterance;

        private void OnEnable()
        {
            GameManager.OnGameStarted += GameManager_OnGameStart;
        }

        private void GameManager_OnGameStart(object sender, EventArgs e)
        {
            StartCoroutine(ElevatorSequence());
        }

        private void Start()
        {
           
        }
        [ContextMenu("Start Elevator Scene")]
        public void StartElevatorScene()
        {
            elevatorNpc[0].Move(1, 2);

        }
      [  ContextMenu("Go to Floor 1")]
        public void Floor1()
        {
            elevatorNpc[1].Move(0, 1);
        }
        [ContextMenu("Go to Floor 2")]
        public void Floor2() { 
            elevatorNpc[2].Move(2, 1);
        }
        [ContextMenu("Go to Floor 3")]
        public void Floor3() {
            elevatorNpc[3].Move(1, 0);
        }

        IEnumerator ElevatorSequence()
        {
            yield return new WaitForSeconds(3f);
            OnElevatorDoorOpen?.Invoke(this, EventArgs.Empty);
        
            yield return new WaitForSeconds(1);
            StartElevatorScene();
            yield return new WaitForSeconds(Npc2Enterance);
            OnElevatorDoorClose?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(2f);
            OnElevatorDoorOpen?.Invoke(this, EventArgs.Empty);
            Floor1();
            UIManager.instance.FloorNoText.text = "Floor 1";

            yield return new WaitForSeconds(Npc3Enterance);
            OnElevatorDoorClose?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(2f);
            OnElevatorDoorOpen?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(2f);
            Floor2();
            UIManager.instance.FloorNoText.text = "Floor 2";

            yield return new WaitForSeconds(Npc4Enterance);
            OnElevatorDoorClose?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(2f);
            OnElevatorDoorOpen?.Invoke(this, EventArgs.Empty);
            UIManager.instance.FloorNoText.text = "Floor 3";

            yield return new WaitForSeconds(2f);
            Floor3();
            yield return new WaitForSeconds(10f);
            //OnElevatorDoorClose?.Invoke(this, EventArgs.Empty);
        }
    }

   
}
