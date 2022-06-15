using UnityEngine;

// THIS PLAYER MOVE CLASS WILL ALLOW THE GAMEOBJECT TO MOVE BASED ON CHARACTERCONTROLLER

// NOTE - It is VITAL that ground surfaces be assigned to a layer that the 'Ground Mask' this script uses or there will be serious issues with moving and jumping.

public class FPSMovement : MonoBehaviour
{
    // VARS
    public UnityEngine.CharacterController m_charController;
    public float m_movementSpeed = 10f;
    public float m_runSpeed = 1.5f;
    private float m_finalSpeed = 0;

    // jumping & air movement

    public float m_gravity = -9.81f;
    public float m_jumpHeight = 1.5f;
    private Vector3 m_velocity;

    public Transform m_groundCheckPoint;
    public float m_groundDistance = 0.3f;
    public LayerMask m_groundMask;
    private bool m_isGrounded;
    public float m_horizontalAirScaling = 0.25f;

    // key bindings

    public KeyCode m_forward;
    public KeyCode m_back;
    public KeyCode m_left;
    public KeyCode m_right;
    public KeyCode m_sprint;
    public KeyCode m_jump;
    public KeyCode m_crouch;

    // crouching vars

    public bool crouching;
    public GameObject m_camera;
    public GameObject m_cameraStandPoint;
    public GameObject m_cameraCrouchPoint;
    public float lerpRate;
    public float headRoom;
    private bool crouchSwitched;

    void Awake()
    {
        m_finalSpeed = m_movementSpeed;
        m_charController.height = 1.8f;
        
    }

    void Update()
    {
        m_isGrounded = HitGroundCheck(); // CHecks touching the ground every frame
        MoveInputCheck();

    }

    // Check if a button is pressed
    void MoveInputCheck()
    {
        float x = Input.GetAxis("Horizontal"); // Gets the x input value for the Gameobject vector
        float z = Input.GetAxis("Vertical"); // Gets the z input value for the Gameobject vector

        Vector3 move = Vector3.zero;

        if (m_isGrounded) // If the player is on a ground surface, move at normal speed.
        {
            if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right))
            {
                move = transform.right * x + transform.forward * z; // calculate the move vector (direction)          
            }
        }

        if (m_isGrounded == false)
        {
            if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right))
            {
                move = (transform.right * x * m_horizontalAirScaling) + (transform.forward * z); // calculate the move vector (direction)          
            }
        }
        


        MovePlayer(move); // Run the MovePlayer function with the vector3 value move
        RunCheck(); // Checks the input for run
        JumpCheck(); // Checks if we can jump

        if (Input.GetKeyDown(m_crouch))
        {
            if (crouching)
            {
                crouching = false;
                crouchSwitched = true;
                RaycastHit hit;
                if (Physics.Raycast(m_camera.transform.position, Vector3.up * headRoom, out hit, headRoom))
                {
                    string name = hit.collider.gameObject.name;
                    if (hit.collider.tag != null)
                    {
                        Debug.Log("Object = " + name + " above, cannot stand.");
                        crouching = true;
                        crouchSwitched = false;
                    }                 
                }                    
            }
            else
            {
                crouching = true;
                crouchSwitched = true;
            }

            CrouchCheck();
        }
    }

    // MovePlayer
    void MovePlayer(Vector3 move)
    {
        m_charController.Move(move * m_finalSpeed * Time.deltaTime); // Moves the Gameobject using the Character Controller

        m_velocity.y += m_gravity * Time.deltaTime; // Gravity affects the jump velocity
        m_charController.Move(m_velocity * Time.deltaTime); //Actually move the player up
    }

    void CrouchCheck()
    {
        if (crouchSwitched == true)
        {
            if (crouching)
            {
                m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_cameraCrouchPoint.transform.position, Time.deltaTime * lerpRate);
                m_charController.height = 0.75f; // If you want to change the crouching height, adjust this. Be careful doing so - Kept this non-exposed as it can lead to issues.
                crouchSwitched = false;
            }
            else
            {
                m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_cameraStandPoint.transform.position, Time.deltaTime * lerpRate);
                m_charController.height = 1.8f;
                crouchSwitched = false;
            }
        }  
    }

    // Player run
    void RunCheck()
    {
        if (Input.GetKeyDown(m_sprint)) // if key is down, sprint
        {
            m_finalSpeed = m_movementSpeed * m_runSpeed;
        } 
        else if (Input.GetKeyUp(m_sprint)) // if key is uo, don't sprint
        {
            m_finalSpeed = m_movementSpeed;
        }
    }

    // Ground check
    bool HitGroundCheck()
    {
        bool isGrounded = Physics.CheckSphere(m_groundCheckPoint.position, m_groundDistance, m_groundMask);

        //Gravity
        if (isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        return isGrounded;
    }

    // Jump Check
    void JumpCheck()
    {
        if (Input.GetKeyDown(m_jump))
        {
            if (m_isGrounded == true)
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            }
        }

    }
}
