using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimationFactor : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimator(bool activeAnimator){
        if(activeAnimator){
            animator.enabled = true;
        }else if(!activeAnimator)
        {
            animator.enabled = false;
        }
    }
}
