using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public static Fader Instance;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void FadeIn()
    {
        animator.Play("Fade in");
    }

    public void FadeOut()
    {
        animator.Play("Fade out");
    }
}
