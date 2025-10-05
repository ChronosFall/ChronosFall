using UnityEngine;

public class TestAnimationScript : MonoBehaviour
{
    Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetFloat("Speed", Input.GetAxis("Vertical"));
    }
}

