﻿using UnityEngine;
using System.Collections;

namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;

        private Animator animator;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();

            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        }

        private void Start()
        {
            animator = GetComponent<PlatformerCharacter2D>().GetAnimator();
        }

        private void Update()
        {
            if (!jump)
                // Read the jump input in Update so button presses aren't missed.
                jump = Input.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            character.Move(h, crouch, jump);
            jump = false;
        }

        public void Blink(float hurtTime)
        {
            StartCoroutine(HurtBlinker(hurtTime));
        }

        IEnumerator HurtBlinker(float hurtTime)
        {
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
            GetComponent<PlatformerCharacter2D>().anim.SetLayerWeight(1, 1);
            yield return new WaitForSeconds(hurtTime);
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            GetComponent<PlatformerCharacter2D>().anim.SetLayerWeight(1, 0);
        }
    }
}