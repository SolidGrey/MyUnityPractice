using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy current;
    public float health = 3f;
    public int bounty = 1;
    public int damage = 1;
    public float speed = 2f;

    private void Awake()
    {
        current = this;
    }

    private void OnEnable()
    {
        
    }
}
