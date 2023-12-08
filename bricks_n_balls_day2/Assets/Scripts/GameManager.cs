using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StageManager stageManager = null;
    [SerializeField] GameObjectManager gameObjectManager = null;

    // Start is called before the first frame update
    void Start()
    {
        gameObjectManager.Initialize();
        stageManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
