using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;
using static UnityEngine.Rendering.DebugUI;
public class Grid
{
   public  int width;
    public int height;
 public   float cellSize;
    int[,] gridArray;
    Vector3 originPosition;
    TextMeshPro[,] DebugArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new int[width, height];
        DebugArray=new TextMeshPro[width, height];
        for (int x = 0;x<gridArray.GetLength(0);x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            { 
             //DebugArray[x,y]= UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y)+new Vector3(cellSize,cellSize)*0.5F, 5, Color.yellow, TextAnchor.MiddleCenter);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
               // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,0 ), GetWorldPosition(width, height), Color.white, 100f);
    }
    public Vector3 GetWorldPosition(int x, int y, int z = 0)
    {
        return new Vector3(x, y, z)*cellSize+originPosition;
    }
    public Vector3 GetWorldPositionCenter(int x, int y, int z = 0)
    {
        return new Vector3(x, y, z) * cellSize;
    }
    public void SetValue(int x ,int y,int value)
    {
        if (x>=0 && y>=0 && x<width && y<height )
        gridArray[x,y] = value;
     //   DebugArray[x,y].text=value.ToString();
    }

    public void GetXY(Vector3 worldPositon,out int x,out  int y, int z=0)
    {
        x=Mathf.FloorToInt((worldPositon.x-originPosition.x)/cellSize);
        y=Mathf.FloorToInt((worldPositon.y - originPosition.y )/ cellSize);
    }
    public void SetValue(Vector3 worldPosition,int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    public int GetValue(int x,int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
            return gridArray[x, y];
        else return 0;
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }       

}
