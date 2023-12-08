using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab = null;
    [SerializeField] TextMeshProUGUI ballCountText = null;
    private List<Ball> ballList = new List<Ball>();
    private Vector2 velocity = Vector2.zero;
    private bool isShot = false;
    private bool isAllStop = true;
    private int shotCount = 0;
    private float shotTimer = 0.0f;
    private float SHOT_DELAY = 0.05f;
    private float BALL_POOL_MAX = 100;
    private Vector2 firstStopPosition = Vector2.zero;
    private Vector2 START_POSITION = new Vector2(0.0f, -4.5f);
    private Vector2 TEXT_POSITION_OFFSET = new Vector2(0.0f, 0.25f);

    public void Initialize()
    {
        for (int i = 0; i < BALL_POOL_MAX; i++)
        {
            GameObject ballObject = Instantiate(ballPrefab, START_POSITION, Quaternion.identity);
            ballObject.transform.parent = this.transform;
            Ball ball = ballObject.GetComponent<Ball>();
            ball.Initialize();
            ballList.Add(ball);
        }

        firstStopPosition = START_POSITION;

        ballCountText.text = "×" + ballList.Count.ToString();
        ballCountText.transform.position = firstStopPosition + TEXT_POSITION_OFFSET;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAllStop) CheckStop();

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

    public void ShotStart(Vector2 velocity)
    {
        this.velocity = velocity;
        isShot = true;
        isAllStop = false;
        foreach (Ball ball in ballList)
        {
            ball.SetIsMoving(true);
            ball.SetIsGather(false);
        }
    }

    private void ShotBall()
    {
        ballList[shotCount].SetVelocity(velocity);
        shotCount++;
    }

    public List<Ball> GetBallList()
    {
        return ballList;
    }

    public bool GetIsAllStop()
    {
        return isAllStop;
    }

    public Vector2 GetFirstStopPosition()
    {
        return firstStopPosition;
    }

    public void CheckStop()
    {
        List<int> stopIndexList = new List<int>();

        for (int i = 0; i < ballList.Count; i++)
        {
            if (ballList[i].GetIsMoving()) continue;

            stopIndexList.Add(i);
            ballCountText.text = "×" + stopIndexList.Count.ToString();
        }

        if (stopIndexList.Count == 1)
        {
            firstStopPosition = ballList[stopIndexList[0]].transform.position;
            foreach (Ball ball in ballList)
            {
                ball.SetFirstStopPosition(firstStopPosition);
                ballCountText.transform.position = firstStopPosition + TEXT_POSITION_OFFSET;
            }
            return;
        }

        if (stopIndexList.Count != ballList.Count) return;
        isAllStop = true;
        foreach (Ball ball in ballList)
        {
            ball.SetFirstStopPosition(firstStopPosition);
            ball.SetIsGather(true);
        }
    }
}
