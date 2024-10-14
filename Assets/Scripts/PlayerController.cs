using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public float jumpForce;
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right
    public float laneDistance = 4; // distance between two lanes

    public float gravity = -20;

    public Animator animator;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        direction.z = forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerManager.isGameStarted) return;
       
        animator.SetBool("isGameStarted", true);
        bool isGrounded = transform.position.y < 4.0f;
        // debug log isGrounded transform.position.y, originY, Mathf.Abs(transform.position.y - originY)
        animator.SetBool("isGrounded", isGrounded);
        checkJump();
        checkChangeLane();
        checkSlide();
     
    }

    private void checkChangeLane()
    {


        if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        forwardSpeed+= 0.1f * Time.deltaTime;
        forwardSpeed = Mathf.Min(forwardSpeed, maxSpeed);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        if (transform.position == targetPosition) return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 *  Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    private void checkJump()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) ||SwipeManager.swipeUp)
            {
                direction.y = jumpForce;
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
    private void checkSlide()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || SwipeManager.swipeDown)
        {
            StartCoroutine(Sliding());
        }
    }

   private IEnumerator Sliding()
    {
        if(animator.GetBool("isSliding")) yield break;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("isSliding", false);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
    }
}




