using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab = null;
    private List<Ball> ballList = new List<Ball>();
    private bool isShot = false;
    private int shotCount = 0;
    private float shotTimer = 0.0f;
    private float SHOT_DELAY = 0.05f;
    private float BALL_POOL_MAX = 100;

    public void Initialize()
    {
        for (int i = 0; i < BALL_POOL_MAX; i++)
        {
            GameObject ballObject = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
            ballObject.transform.parent = this.transform;
            Ball ball = ballObject.GetComponent<Ball>();
            ball.Initialize();
            ballList.Add(ball);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShotStart();
        }

        if (!isShot) return;
        shotTimer += Time.deltaTime;
        if (shotTimer < SHOT_DELAY) return;
        shotTimer = 0.0f;
        if (shotCount >= ballList.Count)
        {
            isShot = false;
            shotCount = 0;
            return;
        }
        ShotBall();
    }

    private void ShotStart()
    {
        isShot = true;
    }

    private void ShotBall()
    {
        ballList[shotCount].SetVelocity(new Vector2(1.0f, 1.0f));
        ballList[shotCount].SetIsMoving(true);
        shotCount++;
    }

    public List<Ball> GetBallList()
    {
        return ballList;
    }
}
