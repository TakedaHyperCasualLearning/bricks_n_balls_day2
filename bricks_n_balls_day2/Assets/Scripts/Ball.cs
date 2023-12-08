using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 velocity;
    private float radius = 0.0f;
    private float speed = 0.025f;
    private float screenWidth = 0.0f;
    private float screenHeight = 0.0f;
    private bool isMoving = false;

    public void Initialize()
    {
        velocity = new Vector2(0.0f, 0.0f);
        radius = transform.localScale.x / 2.0f;

        // 画面の幅と高さを取得
        screenWidth = Camera.main.ViewportToWorldPoint(Vector2.one).x * 2.0f;
        screenHeight = Camera.main.ViewportToWorldPoint(Vector2.one).y * 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(velocity * speed);

        CheckBoundsOuterWall();
    }

    public void HitBounds(Vector2 position, Vector2 halfScale)
    {
        Vector2 ballPosition = transform.position;

        float leftDistanceLength = (new Vector2(position.x - halfScale.x, position.y) - ballPosition).magnitude;
        float rightDistanceLength = (new Vector2(position.x + halfScale.x, position.y) - ballPosition).magnitude;
        float topDistanceLength = (new Vector2(position.x, position.y + halfScale.y) - ballPosition).magnitude;
        float bottomDistanceLength = (new Vector2(position.x, position.y - halfScale.y) - ballPosition).magnitude;

        if (leftDistanceLength < rightDistanceLength && leftDistanceLength < topDistanceLength && leftDistanceLength < bottomDistanceLength)
        {
            velocity.x = -Mathf.Abs(velocity.x);
        }
        if (rightDistanceLength < leftDistanceLength && rightDistanceLength < topDistanceLength && rightDistanceLength < bottomDistanceLength)
        {
            velocity.x = Mathf.Abs(velocity.x);
        }
        if (topDistanceLength < leftDistanceLength && topDistanceLength < rightDistanceLength && topDistanceLength < bottomDistanceLength)
        {
            velocity.y = Mathf.Abs(velocity.y);
        }
        if (bottomDistanceLength < leftDistanceLength && bottomDistanceLength < rightDistanceLength && bottomDistanceLength < topDistanceLength)
        {
            velocity.y = -Mathf.Abs(velocity.y);
        }
    }

    private void CheckBoundsOuterWall()
    {
        if (transform.position.x - radius < screenWidth * -0.5f)
        {
            velocity.x = Mathf.Abs(velocity.x);
        }
        else if (transform.position.x + radius > screenWidth * 0.5f)
        {
            velocity.x = -Mathf.Abs(velocity.x);
        }

        if (transform.position.y - radius < screenHeight * -0.5f)
        {
            velocity.y = Mathf.Abs(velocity.y);
        }
        else if (transform.position.y + radius > screenHeight * 0.5f)
        {
            velocity.y = -Mathf.Abs(velocity.y);
        }
    }

    public void SetVelocity(Vector2 newVelocity)
    {
        velocity = newVelocity;
    }

    public float GetRadius()
    {
        return radius;
    }

    public void SetIsMoving(bool newIsMoving)
    {
        isMoving = newIsMoving;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }


}
