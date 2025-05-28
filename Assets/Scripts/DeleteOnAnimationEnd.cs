using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnAnimationEnd : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo animStateInfo;
    public string specifcStateName = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (specifcStateName == null || specifcStateName == "") {
            if (animStateInfo.normalizedTime >= 1.0f)
            {
                Destroy(gameObject);
            }
        } else
        {
            if (animStateInfo.IsName(specifcStateName))
            {
                if (animStateInfo.normalizedTime >= 1.0f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
