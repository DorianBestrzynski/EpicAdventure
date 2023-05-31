using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkinChange : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController basicKnight;

    [SerializeField] private RuntimeAnimatorController advancedKnight;

    private Animator animator;

    void Start()
    {
        ChooseKnightSkin();
    }

    public void ChangeKnightSkin() {
        StaticVariables.hasUnlockedBetterKnight = true;
        ChooseKnightSkin();
    }

    public void ChooseKnightSkin()
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
}
