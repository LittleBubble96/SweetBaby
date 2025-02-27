﻿using System.Collections;
using System.Collections.Generic;
using BubbleFramework;
using UnityEngine;

public class SM_SceneManager : Bubble_MonoSingle<SM_SceneManager>
{
    [Bubble_Name("关卡")]
    public List<SM_LevelData> LevelDatas = new List<SM_LevelData>();

    [Bubble_Name("角色")]
    public List<CharacterControls> CharacterControlses =new List<CharacterControls>();

    [Bubble_Name("UI角色")] 
    public List<GameObject> CharacterUIs = new List<GameObject>();
    
    [Bubble_Name("相机")]
    public CameraManager Camera;

    [Bubble_Name("所有AI")] 
    public List<SM_CharacterAIBase> CharacterAIBases = new List<SM_CharacterAIBase>();
    
    [Bubble_Name("人物出生掉落高度")] 
    public float BirthHeight = 10f;

    [Bubble_Name("UI音乐")] 
    public AudioClip uiAudio;
    /// <summary>
    /// 当前通关数
    /// </summary>
    [HideInInspector] 
    public int CrossLevelCount;
    
    /// <summary>
    /// 当前关卡
    /// </summary>
    [HideInInspector]
    public SM_LevelData CurLevelData;

    /// <summary>
    /// 当前相机
    /// </summary>
    [HideInInspector] 
    public CameraManager CurCamera;

    /// <summary>
    /// 当前选择得人物下表
    /// </summary>
    [HideInInspector] 
    public int SelectCharacterIndex;
    
    public void Init()
    {
        CurCamera = Instantiate(Camera, transform);
    }

    public void DoUpdate(float dt)
    {
        CurLevelData.DoUpdate(dt);
    }

    /// <summary>
    /// 创建新得关卡
    /// </summary>
    /// <exception cref="GameException"></exception>
    public void CreateLevel()
    {
        ClearLevel();
        if (LevelDatas.Count <= 0)
        {
            throw new GameException("关卡数为0");
            return;
        }
        //随机下标
        int r =(CrossLevelCount % LevelDatas.Count);
        
        //创建新关卡
        SM_LevelData level = LevelDatas[r];
        CurLevelData = Instantiate(level, transform);
        CurLevelData.Init();
        
        
    }

    /// <summary>
    /// 清除当前关卡
    /// </summary>
    public void ClearLevel()
    {
        if (CurLevelData!=null)
        {
            DestroyImmediate(CurLevelData.gameObject);
        }
    }

    /// <summary>
    /// 检测是否全部通关
    /// </summary>
    /// <returns></returns>
    public bool CheckCrossAll()
    {
        return CrossLevelCount / LevelDatas.Count >= 1;

    }
}
