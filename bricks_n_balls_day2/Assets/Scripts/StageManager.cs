using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] BlockManager blockManager = null;

    private int BLOCK_COUNT_X = 6;
    private int BLOCK_COUNT_Y = 5;
    private float STAGE_WIDTH = 6.0f;
    private float STAGE_HEIGHT = 9.0f;
    private float BLOCK_DISTANCE = 1.0f;

    public void Initialize()
    {
        // ブロックを配置
        for (int y = 0; y < BLOCK_COUNT_Y; y++)
        {
            for (int x = 0; x < BLOCK_COUNT_X; x++)
            {
                int index = BLOCK_COUNT_X * y + x;
                Vector3 position = new Vector3(
                    -STAGE_WIDTH / 2 + BLOCK_DISTANCE / 2 + x * BLOCK_DISTANCE,
                    STAGE_HEIGHT / 2 - BLOCK_DISTANCE / 2 - y * BLOCK_DISTANCE,
                    0.0f
                );
                blockManager.GetBlockList()[index].gameObject.transform.position = position;
                blockManager.GetBlockList()[index].SetHitPointTextPosition(position);
                blockManager.GetBlockList()[index].SetIsActive(true);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
