using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private enum ControllerState { playerControl, menuControl, noControl };
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_JumpHeld;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
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


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis ("Vertical");
            //if (!m_Jump)
            //{
            //    // Read the jump input in Update so button presses aren't missed.
            //    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            //}

            //if (!m_JumpHeld)
            //{
            //    m_JumpHeld = CrossPlatformInputManager.GetButton("Jump");
            //}
            // Pass all parameters to the character control script.
            m_Character.Move(h, v, crouch, m_Jump, m_JumpHeld);
            m_Jump = false;
            m_JumpHeld = false;
        }
    }
}
