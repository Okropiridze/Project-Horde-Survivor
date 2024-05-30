using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public Sprite sprite;

    public float maxMoveSpeed = 800f;
    public float acceleration = 20f;

    public float attackRange = 0f;
    public float health = 100f;
}
