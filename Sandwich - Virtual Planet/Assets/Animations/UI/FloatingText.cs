using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private const string FLOATING_TEXT_ANIMATION_KEY = "float";

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Animator animator;

    public string Text { set { text.text = value; } }

    public void DoAnimation()
    {
        animator.SetTrigger(FLOATING_TEXT_ANIMATION_KEY);
    }
}
