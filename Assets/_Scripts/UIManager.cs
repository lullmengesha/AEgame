using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class UIManager : MonoBehaviour
    {
        [Header("Player")]
        public Image AwkwardBar;
        public GameObject[] panels;
        public  TextMeshProUGUI FloorNoText;
        public static UIManager instance;
        public static UIManager Instance
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
        private void OnEnable()
        {
            HeadLook.OnEyeContactLocal += HeadLook_OnEyeContactLocal;
            HeadLook.OnEyeContactLocalLost += OnEyeCOntactLost;
        }

        private void OnEyeCOntactLost()
        {
            AwkwardBar.fillAmount -= 0.03f * Time.deltaTime;
        }

        private void HeadLook_OnEyeContactLocal()
        {
           Debug.Log("Eye Contact Made - Show Awkward Bar");
           AwkwardBar.fillAmount+= 0.06f*Time.deltaTime;
        }

        private void OnDisable()
        {
            HeadLook.OnEyeContactLocal += HeadLook_OnEyeContactLocal;

        }
        public void TogglePanel(string panelName)
        {
            foreach (GameObject panel in panels)
            {
                if (panel.name == panelName)
                {
                    panel.SetActive(!panel.activeSelf);
                }
               
            }
        }
        private void Start()
        {


        }

        
    }
}