using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    [SerializeField] private DamageZone _damageZone;
    private Animator _animator;

    //delete
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //delete
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _animator.SetTrigger("Attack");
    }

    //Called from Attack animation as Event
    private void Hit()
    {
        //Sent damageZone.GetDamage() to Player
        Debug.Log("Hit!");
    }
}
