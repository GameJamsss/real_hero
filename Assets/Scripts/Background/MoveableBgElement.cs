using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveableBgElement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;

    private Vector2 _min; //bottom-left point of the screen
    private Vector2 _max; //top-rigth point of the screen

    private void Awake()
    {
        _min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        _max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        var spriteLength = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        //Add the object sprite half length to max.x
        _max.x += spriteLength;
        //Substract the object sprite half length to min.x
        _min.x -= spriteLength;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 pos = transform.position;
        pos = new Vector2(pos.x + UnityEngine.Random.Range(_minSpeed, _maxSpeed), pos.y);

        transform.position = pos;

        if (transform.position.x > _max.x)
            ResetPosition();
    }

    private void ResetPosition()
    {
        var randY = UnityEngine.Random.Range(_min.y, _max.y);
        transform.position = new Vector2(_min.x, randY);
        Debug.Log("_min.x" + _min.x);
        Debug.Log("randY" + randY);
    }
}
