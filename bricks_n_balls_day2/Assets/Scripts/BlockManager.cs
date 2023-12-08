using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab = null;
    [SerializeField] TextMeshProUGUI hitPointText = null;
    [SerializeField] Canvas uiCanvas = null;
    private List<Block> blockList = new List<Block>();
    private float BLOCK_POOL_MAX = 30;

    public void Initialize()
    {
        for (int i = 0; i < BLOCK_POOL_MAX; i++)
        {
            GameObject blockObject = Instantiate(blockPrefab, Vector3.zero, Quaternion.identity);
            blockObject.transform.parent = this.transform;
            Block block = blockObject.GetComponent<Block>();
            block.Initialize();
            block.SetHitPoint(Random.Range(40, 80));
            blockList.Add(block);
            TextMeshProUGUI tempText = Instantiate(hitPointText, Vector3.zero, Quaternion.identity);
            tempText.transform.SetParent(uiCanvas.transform);
            tempText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            block.SetTextMeshPro(tempText);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Block> GetBlockList()
    {
        return blockList;
    }
}
