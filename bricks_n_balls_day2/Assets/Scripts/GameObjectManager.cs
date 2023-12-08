using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{

    [SerializeField] BallManager ballManager = null;
    [SerializeField] BlockManager blockManager = null;

    public void Initialize()
    {
        ballManager.Initialize();
        blockManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ballManager.GetBallList().Count; i++)
        {
            if (!ballManager.GetBallList()[i].GetIsMoving()) continue;

            for (int j = 0; j < blockManager.GetBlockList().Count; j++)
            {
                if (!blockManager.GetBlockList()[j].GetIsActive()) continue;

                if (blockManager.GetBlockList()[j].CheckBallHit(ballManager.GetBallList()[i]))
                {
                    ballManager.GetBallList()[i].HitBounds(blockManager.GetBlockList()[j].gameObject.transform.position, blockManager.GetBlockList()[j].GetSideHalf());
                    blockManager.GetBlockList()[j].Hit();
                }
            }
        }

    }




}
