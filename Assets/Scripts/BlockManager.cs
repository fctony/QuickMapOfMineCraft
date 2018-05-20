using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;

public class BlockManager : MonoBehaviour {


    public int range = 10;

    private ushort blockID = 0;//默认摆放物体为空

    public Transform selectBlockEffect;//定义选择的外边框效果


	void Start () {
        selectBlockEffect = GameObject.Find("selected block graphics").transform;
        selectBlockEffect.gameObject.SetActive(false);
		
	}
	

	void Update () {

        SelectBlockID();


        VoxelInfo info = Engine.VoxelRaycast(Camera.main.transform.position,Camera.main.transform.forward,range,false);
        if (info!=null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Voxel.DestroyBlock(info);
            }

            if (Input.GetMouseButtonDown(1))
            {
                VoxelInfo newInfo = new VoxelInfo(info.adjacentIndex, info.chunk);
                Voxel.PlaceBlock(newInfo, blockID);
            }
        }

        
        UpdateSelectBlockEffect(info);
	}
    /// <summary>
    /// 选择不同的block物体
    /// </summary>
    private void SelectBlockID()
    {
        for (ushort i=1;i<10;i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                blockID = i;
            }
        }
    }

    private void UpdateSelectBlockEffect(VoxelInfo info)
    {
        if (info!=null)
        {
            selectBlockEffect.gameObject.SetActive(true);
            selectBlockEffect.position = info.chunk.VoxelIndexToPosition(info.index);
        }
        else
        {
            selectBlockEffect.gameObject.SetActive(false);
        }
    }
}
