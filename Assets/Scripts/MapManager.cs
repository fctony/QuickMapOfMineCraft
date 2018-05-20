using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;

public class MapManager : MonoBehaviour {
    //判断是否生成
    //private bool isGenerate = false;
    public Transform playerTrans;
    //private Vector3 lastPosition;
    private Index lastIndex;

	void Start () {
        playerTrans = GameObject.Find("Player").transform;
        InvokeRepeating("InitMap", 1,0.02f);//此处如果想优化性能，可以适应提高帧数，默认1秒调用50次
		
	}
	
    /// <summary>
    /// 生成地图
    /// </summary>
    private void InitMap()
    {
        //如果地图还没生成直接返回
        if (Engine.Initialized == false || ChunkManager.Initialized == false)
            return;
        //此处是为了优化游戏性能
        Index currentIndex = Engine.PositionToChunkIndex(playerTrans.position);
        if (lastIndex != currentIndex)
        {
            ChunkManager.SpawnChunks(playerTrans.position);
            lastIndex = currentIndex;
        }
        

    }
}
