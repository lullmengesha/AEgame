using Assets._Scripts;
using CodeMonkey;
using CodeMonkey.Utils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
   protected HeadLook headLook;
  public  Grid grid;
    public float moveSpeed = 5f;
    public bool isMoving;
    Vector3 targetPosition;
    public bool moveFreely;
    float inputThreshold = 0.3f;

    // Hold functionality variables
    private float holdTimer = 0f;
    private Vector2 heldInput;
    private bool isHolding = false;
    public float holdTimeRequired = 0.3f; // Time in seconds to hold before moving
    public Vector2 keyboardInput;
    public InputAction moveAction;
    public InputAction RotateAction;

    private void OnEnable()
    {   moveAction.Enable();
        RotateAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        RotateAction.Disable();
    }

    private void Awake()
    {
        moveAction.performed += MoveAction_performed;
        moveAction.canceled += MoveAction_canceled; // Added for hold functionality
        RotateAction.performed += RotateAction_performed;
        RotateAction.canceled += RotateAction_Canceled
            ;
    }
  public  virtual  void Start()
    {
        grid = GridCreator.grid;
        if (grid == null)
        {
            Debug.Log("it is  nul;");

        }
        InitialPosition();
    }
    private void RotateAction_Canceled(InputAction.CallbackContext context)
    {
    
       
       // PlayerAnimatorController.Instance.Idle();
    }

    private void MoveAction_canceled(InputAction.CallbackContext ctx)
    {
        // Reset hold when input is released
 
        isHolding = false;
        holdTimer = 0f;
        heldInput = Vector2.zero;
        if (moveAction.IsPressed())
            return;
        PlayerAnimatorController.Instance.Idle();
    }
    public float keyBoardNormal;
    private void RotateAction_performed(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        keyBoardNormal = input;
     //   Debug.Log(input);  
    }

    private void MoveAction_performed(InputAction.CallbackContext ctx)
    {
        if (isMoving) return;
        Vector2 input = ctx.ReadValue<Vector2>();
        // Only start tracking hold if there's significant input
        if (input.magnitude > 0.1f)
        {
            heldInput = input;
            isHolding = true;
            holdTimer = 0f;
          //  Debug.Log("Started holding input...");
        }
    }

   

    // Update is called once per frame
  public  virtual void Update()
    {
        // Handle hold timing
        if (isHolding && !isMoving)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTimeRequired)
            {
                ExecuteHeldMovement();
                isHolding = false;
                holdTimer = 0f;
            }
        }
        if (!moveFreely)
            Move(moveFreely);
        else
            Move();

        if (isMoving)
        {
            MoveCharacter(this.transform, targetPosition);
        }
    }

    private void ExecuteHeldMovement()
    {
        if (isMoving) return;

        Vector2 input = heldInput;
        keyboardInput = input;

        // Prevent diagonal
        if (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.y) == 1)
            return;

        grid.GetXY(this.transform.position, out int currentX, out int currentY);
        int targetX = currentX + Mathf.RoundToInt(input.x);
        int targetY = currentY + Mathf.RoundToInt(input.y);

        // Check bounds
        if (targetX < 0 || targetY < 0 || targetX >= grid.width || targetY >= grid.height)
            return;

        // Set rotation based on input
        if (input.x == 1)
        {
            keyBoardNormal = 1; // Right rotation
        }
        else if (input.x == -1)
        {
            keyBoardNormal = -1; // Left rotation
        }
        else
        {
            keyBoardNormal = 0; // No rotation for vertical movement
        }

        // Only move if it's a movement input (not just rotation)
        if (input.y != 0 || input.x != 0)
        {
            targetPosition = grid.GetWorldPosition(targetX, targetY) + new Vector3(grid.cellSize, grid.cellSize) * 0.5f;
            isMoving = true;
           // Debug.Log($"Moved to ({targetX}, {targetY}) after {holdTimeRequired}s hold");
        }
    }

    void Move()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 10);
        }
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
            Vector3 worldPos = UtilsClass.GetMouseWorldPosition();
                int x, y;
                grid.GetXY(worldPos, out x, out y);

            if (x >= 0 && y >= 0 && x < grid.width && y < grid.height)
            {
                if (!isMoving)
                    isMoving = true;
                targetPosition = grid.GetWorldPosition(x, y) + new Vector3(grid.cellSize, grid.cellSize) * 0.5f;
            }
            else
            {
                Debug.Log(" Clicked outside grid boundaries");
            }
        }
    }

    Vector2 currentPosition;

    void MoveWithKeyboard(bool limitMovement)
    {
        if (isMoving) return;

        // --- 1️⃣ Get input axes ---
        float horizontal = Keyboard.current != null
            ? (Keyboard.current.dKey.isPressed ? 1 : Keyboard.current.aKey.isPressed ? -1 : 0)
            : 0;
        float vertical = Keyboard.current != null
            ? (Keyboard.current.wKey.isPressed ? 1 : Keyboard.current.sKey.isPressed ? -1 : 0)
            : 0;

        Vector2 input = new Vector2(horizontal, vertical);

        // --- 2️⃣ Apply threshold ---
        if (input.magnitude < inputThreshold)
            return; // too small — ignore movement

        // --- 3️⃣ Normalize and convert to grid offset ---
        input.Normalize();

        int moveX = Mathf.RoundToInt(input.x);
        int moveY = Mathf.RoundToInt(input.y);

        // Prevent diagonal
        if (Mathf.Abs(moveX) == 1 && Mathf.Abs(moveY) == 1)
            return;

        // --- 4️⃣ Determine current and target grid positions ---
        int currentX, currentY;
        grid.GetXY(this.transform.position, out currentX, out currentY);
        currentPosition = new Vector2Int(currentX, currentY);

        int targetX = currentX + moveX;
        int targetY = currentY + moveY;

        // --- 5️⃣ Check grid bounds ---
        if (targetX < 0 || targetY < 0 || targetX >= grid.width || targetY >= grid.height)
        {
            Debug.Log("Can't move — outside grid boundaries.");
            return;
        }

        // --- 6️⃣ Move ---
        isMoving = true;
        targetPosition = grid.GetWorldPosition(targetX, targetY) + new Vector3(grid.cellSize, grid.cellSize) * 0.5f;

        Debug.Log($"Moving to: ({targetX},{targetY})");
    }

    void Move(bool limitMovement)
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            grid?.SetValue(UtilsClass.GetMouseWorldPosition(), 10);
        }

        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 worldPos = UtilsClass.GetMouseWorldPosition();

            // Get target cell (mouse click)
            int targetX, targetY;
            grid.GetXY(worldPos, out targetX, out targetY);

            // Get current character grid cell
            int currentX, currentY;
            grid.GetXY(this.transform.position, out currentX, out currentY);
            currentPosition = new Vector2Int(currentX, currentY);
            
            // Calculate difference
            int dx = targetX - currentX;
            int dy = targetY - currentY;

            Debug.Log($"Current: ({currentX},{currentY}) → Target: ({targetX},{targetY}) | Diff: {dx},{dy}");

            if (targetX >= 0 && targetY >= 0 && targetX < grid.width && targetY < grid.height)
            {
                if (!((Mathf.Abs(dx) == 1 && dy == 0) || (Mathf.Abs(dy) == 1 && dx == 0)))
                {
                    Debug.Log("Invalid move: only one block allowed in cardinal directions.");
                    return;
                }

                if (!isMoving)
                {
                    isMoving = true;
                    targetPosition = grid.GetWorldPosition(targetX, targetY) + new Vector3(grid.cellSize, grid.cellSize) * 0.5f;
                }
            }
            else
            {
                Debug.Log("Clicked outside grid boundaries");
            }
        }
    }

    void InitialPosition()
    {
        isMoving = true;
        targetPosition = grid.GetWorldPosition(1, 1) + new Vector3(grid.cellSize, grid.cellSize) * 0.5f;
    }

    void MoveCharacter(Transform ch, Vector2 targetPosition)
    {
        ch.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, targetPosition) < 0.01f)
        {
            this.transform.position = targetPosition; // Snap to final spot
            isMoving = false;
        }
    }
}