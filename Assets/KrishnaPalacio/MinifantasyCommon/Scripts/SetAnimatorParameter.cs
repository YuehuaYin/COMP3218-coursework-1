using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minifantasy
{
    public class SetAnimatorParameter : MonoBehaviour
    {
        private Animator animator;

        public string parameterName = "Idle";
        private string prevAnim = "Idle";
        public float x = 1;
        public float y = 1;
        public float waitTime = 0f;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            Invoke("ToggleAnimatorParameter", waitTime);
            ToggleDirection();
        }

        public void ToggleAnimation(string anim)
        {
            parameterName = anim;
            animator.SetBool(prevAnim, false);
            animator.SetBool(parameterName, true);
            prevAnim = anim;
        }
        

        public void ToggleDirection()
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
        }

        public void ToggleXDirection(int x)
        {
            animator.SetFloat("X", x);
        }
        public void ToggleYDirection(int y)
        {
            animator.SetFloat("Y", y);
        }
    }
}