using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkinChange : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController basicKnight;

    [SerializeField] private RuntimeAnimatorController advancedKnight;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
 
        if(StaticVariables.hasUnlockedBetterKnight)
        {
            animator.runtimeAnimatorController = advancedKnight;
            transform.localScale = new Vector3(6, 8, 1);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z); // adjust the position to align with previous Animator Controller
        }
        else
        {
            animator.runtimeAnimatorController = basicKnight;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
