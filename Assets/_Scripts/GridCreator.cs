using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Scripts
{
    [DefaultExecutionOrder(-1)]
    public class GridCreator : MonoBehaviour
    {
        public static Grid grid;
        public Transform gridOrigin;
        public int width;
        public int height;
        public float cellSize;
        private void Awake()
        {
          
            grid = new Grid(width, width, cellSize, gridOrigin.position);
        }
       
    }
}
