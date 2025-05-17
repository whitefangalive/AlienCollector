using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class changeAnimationOffset : MonoBehaviour
{
    private Animator mAnimator;
    public float randMax = 1.5f;
    public float randMin = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAnimator.speed = Random.Range(randMax, randMin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
