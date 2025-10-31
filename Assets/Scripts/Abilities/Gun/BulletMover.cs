using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Move(Enemy enemy)
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, enemy.transform.position, _speed * Time.deltaTime);
    }
}
