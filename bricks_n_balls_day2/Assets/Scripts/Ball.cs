using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 velocity;
    private float radius = 0.0f;
    private float speed = 0.025f;
    private float screenWidth = 0.0f;
    private float screenHeight = 0.0f;


    // Start is called before the first frame update
    void Start()
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
}
