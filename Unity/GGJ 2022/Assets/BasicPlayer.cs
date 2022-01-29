using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BasicPlayer : MonoBehaviour
{
    private Transform _transform;
    private BoxCollider2D _hitbox;
    private Rigidbody2D _rigidbody;

    public Transform spriteTransform;
    public LayerMask groundCheckMask;

    public Transform leftSensorTransform;
    public Transform rightSensorTransform;
    public Transform groundSensorTransform;

    private bool isHitR;
    private bool isHitL;
    private bool isHitG;
    private bool isHitForward;

    private Vector2 hitPointR = Vector2.zero;
    private Vector2 hitPointL = Vector2.zero;
    private Vector2 hitPointG = Vector2.zero;
    private Vector2 hitPointForward = Vector2.zero;

    private void Awake () {
        _transform = transform;
        _hitbox = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawRays();

        //UpdateMovement();

        //SetStates();
    }

    void DrawRays () {
        //---------------------------------------------------------------------
        // Use raycasting to find where the ground is in relation to the player
        // and what angle the ground they are on is.
        //---------------------------------------------------------------------

        // Set the direction and distance of the ground check
        Vector2 downDirection = new Vector2(0, (_transform.localScale.y * -1f));// -myTransform.up;
        Vector2 forwardDirection = new Vector2(spriteTransform.localScale.x, 0);
        float distanceSide = 0.25f;
        float distanceGround = 0.25f;
        float distanceFront = (_hitbox.size.x / 2f) + 0.05f;

        // Get the start positions for the two forward checks
        Vector3 l_forwardStartT = _transform.position;
        l_forwardStartT.y += (_hitbox.size.y - 0.1f) * _transform.localScale.y;
        Vector3 l_forwardStartB = _transform.position;
        l_forwardStartB.y += 0.1f * _transform.localScale.y;

        // Create debug visualizations of the rays being used
        Debug.DrawRay(rightSensorTransform.position, downDirection * distanceSide, Color.green);
        Debug.DrawRay(leftSensorTransform.position, downDirection * distanceSide, Color.green);
        Debug.DrawRay(groundSensorTransform.position, downDirection * distanceGround, Color.red);
        Debug.DrawRay(l_forwardStartT, forwardDirection * distanceFront, Color.blue);
        Debug.DrawRay(l_forwardStartB, forwardDirection * distanceFront, Color.blue);

        //		Debug.DrawRay (topSensorTransform.position, forwardDirection * distanceFront, Color.yellow);
        //		Debug.DrawRay (bottomSensorTransform.position, forwardDirection * distanceFront, Color.yellow);

        // Create a raycast for each of the 3 sensor points
        RaycastHit2D hitR = Physics2D.Raycast(rightSensorTransform.position, downDirection, distanceSide, groundCheckMask);
        RaycastHit2D hitL = Physics2D.Raycast(leftSensorTransform.position, downDirection, distanceSide, groundCheckMask);
        RaycastHit2D hitG = Physics2D.Raycast(groundSensorTransform.position, downDirection, distanceGround, groundCheckMask);
        RaycastHit2D hitFT = Physics2D.Raycast(l_forwardStartT, forwardDirection, distanceFront, groundCheckMask);
        RaycastHit2D hitFB = Physics2D.Raycast(l_forwardStartB, forwardDirection, distanceFront, groundCheckMask);
        //		RaycastHit2D hitT = Physics2D.Raycast(topSensorTransform.position, forwardDirection, distanceFront, groundCheckMask);
        //		RaycastHit2D hitB = Physics2D.Raycast(bottomSensorTransform.position, forwardDirection, distanceFront, groundCheckMask);

        isHitR = false;
        isHitL = false;
        isHitG = false;
        isHitForward = false;

        hitPointR = Vector2.zero;
        hitPointL = Vector2.zero;
        hitPointG = Vector2.zero;
        hitPointForward = Vector2.zero;

        // The right sensor hit something
        if (hitR != null && hitR.collider != null) {
            isHitR = true;
            hitPointR = hitR.point;
        }

        // The left sensor hit something
        if (hitL != null && hitL.collider != null) {
            isHitL = true;
            hitPointL = hitL.point;
        }

        // The middle sensor hit something
        if (hitG != null && hitG.collider != null) {
            isHitG = true;
            hitPointG = hitG.point;
        }

        // The front top sensor hit something
        if (hitFT != null && hitFT.collider != null) {
            isHitForward = true;
            hitPointForward = hitFT.point;
        }

        // The front bottom sensor hit something
        if (hitFB != null && hitFB.collider != null) {
            isHitForward = true;
            hitPointForward = hitFB.point;
        }
    }

     /*private void UpdateMovement () {

        // Get current velocity
        Vector3 newVelocity = _rigidbody.velocity;

        // Listen for button presses
        //if (AbilityManager.instance.HasAbility (AbilityManager.Ability.Vector)) 
        //{
        if (InputManager.instance.GetJump()) {
            if (myStateManager.currentGroundState == Enums.PlayerGroundState.OnGround) {
                Jump(Constants.JUMP_FORCE);
            }

            if (myStateManager.currentGroundState == Enums.PlayerGroundState.Stuck) {
                Jump(Constants.JUMP_FORCE);
                newVelocity = myRigidbody.velocity;
                newVelocity.x = Constants.RUN_SPEED * spriteTransform.localScale.x;
                myRigidbody.gravityScale = Constants.PLAYER_GRAVITY * myTransform.localScale.y;

                myRigidbody.velocity = newVelocity;

                myStateManager.currentGroundState = Enums.PlayerGroundState.Rising;

                _currentLaunchDuration = Constants.LAUNCH_TIMER;
            }
        }
        //}

        if (_currentLaunchDuration > 0) {
            _currentLaunchDuration -= Time.deltaTime;
            return;
        }

        if (myStateManager.currentGroundState == Enums.PlayerGroundState.Stuck) {
            myRigidbody.velocity = Vector3.zero;
            return;
        }

        // Apply horizontal movement
        newVelocity = myRigidbody.velocity;
        newVelocity.x = InputManager.instance.GetMovement() * Constants.RUN_SPEED;

        // If our front sensor hit something
        if (isHitForward) {
            // If we're trying to move toward the point we hit cancel movement
            if (Mathf.Sign(newVelocity.x) == Mathf.Sign(spriteTransform.localScale.x)) {
                newVelocity.x = 0;
            }

            if (myStateManager.currentGroundState != Enums.PlayerGroundState.OnGround) {
                return;
                myStateManager.currentGroundState = Enums.PlayerGroundState.Stuck;
                myRigidbody.gravityScale = 0f;
                newVelocity = Vector2.zero;
                //Player.instance.PlayAnimation ("stuck");

                // Flip our current direction
                if (spriteTransform.localScale.x > 0) // facing right
                {
                    SetDirection(Enums.Direction.Left);
                }
                else {
                    SetDirection(Enums.Direction.Right);
                }

                Vector3 l_newPosition = myTransform.position;
                l_newPosition.x = hitPointForward.x + (0.1f * spriteTransform.localScale.x);
                myTransform.position = l_newPosition;

                myRigidbody.velocity = newVelocity;
                return;
            }
        }
        myRigidbody.velocity = newVelocity;

        // Make sure the sprite is facing the right way
        if (spriteTransform.localScale.x > 0) // facing right
        {
            if (InputManager.instance.GetMovement() < 0) // Moving left
            {
                SetDirection(Enums.Direction.Left);
            }
        }

        if (spriteTransform.localScale.x < 0) // facing left
        {
            if (InputManager.instance.GetMovement() > 0) // Moving right
            {
                SetDirection(Enums.Direction.Right);
            }
        }
    }

    private void SetStates () {
        // Ignore if we're stuck to a wall
        if (myStateManager.currentGroundState == Enums.PlayerGroundState.Stuck) {
            return;
        }

        // Set in air states

        if (!isHitG) {
            if (myRigidbody.velocity.y > 0.5f) {
                if (myStateManager.currentGroundState != Enums.PlayerGroundState.Rising) {
                    myStateManager.currentGroundState = Enums.PlayerGroundState.Rising;
                    _isCharging = false;
                }
            }
            else if (myRigidbody.velocity.y < -0.5f) {
                if (myStateManager.currentGroundState != Enums.PlayerGroundState.Falling) {
                    myStateManager.currentGroundState = Enums.PlayerGroundState.Falling;
                    _isCharging = false;
                }
            }
        }
        else {
            if (myStateManager.currentGroundState != Enums.PlayerGroundState.OnGround) {
                myStateManager.currentGroundState = Enums.PlayerGroundState.OnGround;
            }
        }

        // Set running state
        if (Mathf.Abs(myRigidbody.velocity.x) > 0.5f && !myStateManager.isMoving) {
            myStateManager.isMoving = true;
        }
        else if (Mathf.Abs(myRigidbody.velocity.x) <= 0.5f && myStateManager.isMoving) {
            myStateManager.isMoving = false;
        }
    }

    private void SetControl (bool p_control) {
        _rigidbody.simulated = p_control;
        _rigidbody.velocity = (p_control) ? Vector2.zero : _rigidbody.velocity;
        _hitbox.enabled = p_control;
    }*/
}
