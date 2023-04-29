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
            transform.localScale = new Vector3(6, 7, 1);
            Vector3 newPosition = transform.localPosition;
            newPosition.y = -1.3f; 
            transform.localPosition = newPosition;
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
