using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSkinChange : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController basicMagician;

    [SerializeField] private RuntimeAnimatorController advancedMagician;

    private Animator animator;

    void Start()
    {
        ChooseMagicianSkin();
    }

    public void ChangeMagicianSkin()
    {
        StaticVariables.hasUnlockedBetterMagician = true;
        ChooseMagicianSkin();
    }

    public void ChooseMagicianSkin()
    {
        animator = GetComponent<Animator>();

        if (StaticVariables.hasUnlockedBetterMagician)
        {
            animator.runtimeAnimatorController = advancedMagician;
            Vector3 newPosition = transform.localPosition;
            Debug.Log("Curr position" + newPosition);
            newPosition.y = 0.65f;
            transform.localPosition = newPosition;
        }
        else
        {
            animator.runtimeAnimatorController = basicMagician;
        }

    }
}
