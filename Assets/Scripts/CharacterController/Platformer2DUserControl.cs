using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public enum ControllerState { playerControl, menuControl, noControl };
    ControllerState controlState;
    private PlatformerCharacter2D m_Character;
    
    private bool m_Jump;
    private bool m_JumpHeld;
    private bool interact;

    public ControllerState State
    {
        get { return controlState; }
        set { controlState = value; }
    }

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
        controlState = ControllerState.playerControl;
    }


    private void Update()
    {
        if (controlState == ControllerState.playerControl)
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_JumpHeld)
            {
                m_JumpHeld = CrossPlatformInputManager.GetButton("Jump");
            }
        }
    }

    private void FixedUpdate()
    {
        if (controlState == ControllerState.playerControl)
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool attack = Input.GetButtonDown("Fire1");

            interact = Input.GetKeyDown(KeyCode.E);
            if (interact)
            {
                m_Character.Interact();
            }

			//TODO move this logic in to the Character itself
            if (attack)
            {
				m_Character.StartAttacking();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                m_Character.StopAttacking();
            }

            m_Character.Move(h, v, crouch, m_Jump, m_JumpHeld);
            m_Jump = false;
            m_JumpHeld = false;
        }
    }
}
