using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSInput : MonoBehaviour
{
    [SerializeField] bool _invertVert = false;
    //[SerializeField] GameObject levelManager;

    bool pauseGame;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    //public event Action JumpInput = delegate { };
    //public event Action SprintPressed = delegate { };
    //public event Action SprintReleased = delegate { };
    public event Action ShootInput = delegate { };

    private void Awake()
    {
        //pauseGame = levelManager.GetComponent<LevelManager>().Paused;
    }

    // Update is called once per frame
    void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        //DetectJumpInput();
        //DetectSprintInput();
        DetectShootInput();

        //pauseGame = levelManager.GetComponent<LevelManager>().Paused;
    }

    void DetectMoveInput()
    {
        // process input as 0 or 1 value
        float xIn = Input.GetAxisRaw("Horizontal");
        float yIn = Input.GetAxisRaw("Vertical");

        // if we have either horz or vert input
        if (xIn != 0 || yIn != 0)
        {
            // convert to local directions based on player orientation
            Vector3 _horzMovement = transform.right * xIn;
            Vector3 _forwardMovement = transform.forward * yIn;

            // combine movements into one vector
            Vector3 movement = (_horzMovement + _forwardMovement).normalized;

            // say we have moved
            MoveInput?.Invoke(movement);
        }
    }

    void DetectRotateInput()
    {
        // get inputs from input controller
        float xIn = Input.GetAxisRaw("Mouse X");
        float yIn = Input.GetAxisRaw("Mouse Y");

        if (xIn != 0 || yIn != 0)
        {
            // account for inverted camera movement if specified
            if (_invertVert == true)
            {
                yIn = -yIn;
            }

            // mouse left/right should be y axis, up/down x axis
            Vector3 rotation = new Vector3(yIn, xIn, 0);

            // say we have rotated
            RotateInput?.Invoke(rotation);
        }
    }

    /*void DetectJumpInput()
    {
        // spacebar pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput?.Invoke();
        }
    }*/

    void DetectShootInput()
    {
        // lmb pressed
        if (Input.GetKeyDown(KeyCode.Mouse0) && (pauseGame == false))
        {
            ShootInput?.Invoke();
        }
    }

    /*void DetectSprintInput()
    {
        // l shift pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SprintPressed?.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SprintReleased?.Invoke();
        }
    }*/
}
