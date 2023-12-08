using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private BallManager ballManager = null;
    [SerializeField] private GameObject dotPoint = null;
    [SerializeField] private GameObject pointMarker = null;
    [SerializeField] private GameObject dotPointParent = null;
    [SerializeField] private GameObject pointMarkerParent = null;

    private Vector2 velocity = Vector2.zero;
    private Vector2 stageSize = Vector2.zero;
    private List<GameObject> dotList = new List<GameObject>();
    private float DOT_DISTANCE = 0.3f;
    private int REFLECTION_DOT_COUNT = 5;
    private int useDotCount = 0;
    private bool isFinish = false;
    private GameObject pointMarkerObject = null;
    private float POINT_MARKER_RADIUS = 0.1f;

    void Start()
    {
        stageSize.x = Camera.main.ViewportToWorldPoint(Vector2.one).x;
        stageSize.y = Camera.main.ViewportToWorldPoint(Vector2.one).y;

        pointMarkerObject = Instantiate(pointMarker, Vector3.zero, Quaternion.identity);
        pointMarkerObject.transform.parent = pointMarkerParent.transform;
        pointMarkerObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector2(mousePosition.x - (stageSize.x * 0.5f), mousePosition.y - (stageSize.y * 0.5f));
            Vector2 direction = ((Vector2)this.transform.position - mousePosition).normalized;
            Debug.Log(direction);
            velocity = direction;

            useDotCount = 0;
            isFinish = false;

            // ドットの線を表示
            do
            {
                Vector2 dotPosition = (Vector2)this.transform.position + direction * DOT_DISTANCE * useDotCount;
                if (dotPosition.x > stageSize.x || dotPosition.x < -stageSize.x || dotPosition.y > stageSize.y || dotPosition.y < -stageSize.y)
                {
                    pointMarkerObject.SetActive(true);
                    float directionRatio = 0.0f;
                    Vector2 crossPoint = Vector2.zero;
                    Vector2 reflectionDirection = Vector2.zero;
                    Vector2 pointMarkerOffset = Vector2.zero;

                    if (dotPosition.x > stageSize.x)
                    {
                        reflectionDirection = Vector2.Reflect(direction, Vector2.left);
                        directionRatio = (stageSize.x - dotPosition.x) / direction.x;
                        pointMarkerOffset.x = -POINT_MARKER_RADIUS;
                    }
                    else if (dotPosition.x < -stageSize.x)
                    {
                        reflectionDirection = Vector2.Reflect(direction, Vector2.right);
                        directionRatio = (-stageSize.x - dotPosition.x) / direction.x;
                        pointMarkerOffset.x = POINT_MARKER_RADIUS;
                    }
                    else if (dotPosition.y > stageSize.y)
                    {
                        reflectionDirection = Vector2.Reflect(direction, Vector2.down);
                        directionRatio = (stageSize.y - dotPosition.y) / direction.y;
                        pointMarkerOffset.y = -POINT_MARKER_RADIUS;
                    }
                    else if (dotPosition.y < -stageSize.y)
                    {
                        reflectionDirection = Vector2.Reflect(direction, Vector2.up);
                        directionRatio = (-stageSize.y - dotPosition.y) / direction.y;
                        pointMarkerOffset.y = POINT_MARKER_RADIUS;
                    }

                    crossPoint = dotPosition + direction * directionRatio;
                    pointMarkerObject.transform.position = crossPoint + pointMarkerOffset;

                    for (int j = 0; j < REFLECTION_DOT_COUNT; j++)
                    {
                        Vector2 reflectionPosition = crossPoint + reflectionDirection * DOT_DISTANCE * j;
                        if (useDotCount < dotList.Count)
                        {
                            dotList[useDotCount].SetActive(true);
                            dotList[useDotCount].transform.position = reflectionPosition;
                        }
                        else
                        {
                            GameObject tempDot = Instantiate(dotPoint, reflectionPosition, Quaternion.identity);
                            tempDot.transform.parent = dotPointParent.transform;
                            dotList.Add(tempDot);
                        }
                        useDotCount++;
                    }
                    isFinish = true;
                    break;
                }

                if (useDotCount < dotList.Count)
                {
                    dotList[useDotCount].SetActive(true);
                    dotList[useDotCount].transform.position = dotPosition;
                }
                else
                {
                    GameObject tempDot = Instantiate(dotPoint, dotPosition, Quaternion.identity);
                    tempDot.transform.parent = dotPointParent.transform;
                    dotList.Add(tempDot);
                }

                useDotCount++;
            } while (!isFinish);

            if (useDotCount < dotList.Count)
            {
                for (int i = useDotCount; i < dotList.Count; i++)
                {
                    dotList[i].SetActive(false);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ballManager.ShotStart(velocity);
            pointMarkerObject.SetActive(false);
            foreach (GameObject dot in dotList)
            {
                dot.SetActive(false);
            }
        }

        if (!ballManager.GetIsAllStop()) return;
        transform.position = ballManager.GetFirstStopPosition();
    }
}
