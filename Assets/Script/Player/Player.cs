using JetBrains.Annotations;
using UnityEngine;

public class Player : SingletonMonobehavior<Player>
{
    // Movement parameters
    private float xInput;
    private float yInput;
    private bool isCarrying = false;
    private bool isIdle;
    private bool isLiftingToolDown;
    private bool isLiftingToolLeft;
    private bool isLiftingToolRight;
    private bool isLiftingToolUp;
    private bool isRunning;
    private bool isUsingToolDown;
    private bool isUsingToolLeft;
    private bool isUsingToolRight;
    private bool isUsingToolUp;
    private bool isSWingingToolDown;
    private bool isSwingingToolLeft;
    private bool isSwingingToolRight;
    private bool isSwingingToolUp;
    private bool isWalking;
    private bool isPickingUp;
    private bool isPickingDown;
    private bool isPickingLeft;
    private bool isPickingRight;
    private ToolEffect toolEffect = ToolEffect.none;

    private Rigidbody2D rigidBody2D;
    private Direction playerDirection;
    private float movementSpeed;

    private bool _playerInputIsDisabled = false;
    public bool PlayerInputIsDisabled
    {
        get => _playerInputIsDisabled;
        set => _playerInputIsDisabled = value;
    }

    protected override void Awake()
    {
        base.Awake();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        #region Player Input
        ResetAnimationTriggers();

        PlayerMovementInput();

        PlayerWalkInput();

        #endregion
    }

    private void ResetAnimationTriggers()
    {
        isPickingRight = false;
        isPickingLeft = false;
        isPickingUp = false;
        isPickingDown = false;
        isUsingToolRight = false;
        isUsingToolLeft = false;
        isUsingToolUp = false;
        isUsingToolDown = false;
        isLiftingToolRight = false;
        isLiftingToolLeft = false;
        isLiftingToolUp = false;
        isLiftingToolDown = false;
        isSwingingToolRight = false;
        isSwingingToolLeft = false;
        isSwingingToolUp = false;
        isSWingingToolDown = false;
        toolEffect = ToolEffect.none;
    }

    private void PlayerMovementInput() {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // khi bấm cả 2 nút thì nhân với 0.71 để không bị nhanh hơn khi đi chéo
        if (yInput != 0 && xInput != 0) { 
            xInput = xInput * 0.71f;
            yInput = yInput * 0.71f;
        }
        // Running
        if (yInput != 0 || xInput != 0) {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Setting.runningSpeed;

            if(xInput < 0) {
                playerDirection = Direction.left;
            }else if(xInput > 0) {
                playerDirection = Direction.right;
            }else if(yInput < 0) {
                playerDirection = Direction.down;
            } else {
                playerDirection = Direction.up;
            }

        }else if(xInput == 0 && yInput == 0){ //Đứng yên
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }
    }

    private void PlayerWalkInput() {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            isRunning = false;
            isWalking = true;
            isIdle = false;
            movementSpeed = Setting.walkingSpeed;
        } else {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Setting.runningSpeed;
        }
    }
}
