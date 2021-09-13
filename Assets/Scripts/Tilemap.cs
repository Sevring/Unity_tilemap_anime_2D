using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab = null;
    private int[] map = new int[]
    {
        24, 25, 25, 25, 26,
        16, 17, 17, 17, 18,
        16, 17, 17, 17, 18,
        8,  9,  9,  9,  10

    };
    // public enum EEvent { W = 1}

    private int[] eventmap = new int[]
    {   //Layer
        //0 : road 1 : wall, 2 : startpoint 
        1,0,1,1,1,
        1,0,0,0,1,
        0,0,2,0,0,
        1,0,1,1,1
    }; 

    private int mapWidth = 5;
    private int mapHeight = 4;
    private int tileW = 1;
    private int tileH = 1;

    private List<GameObject> tileList = new List<GameObject>();
    private int _dir;

    private void Awake()
    {
        Buildmap();

    }

    private void Buildmap()
    {
        float startX = -((tileW * mapWidth) * 0.5f);
        float startY = (tileH * mapHeight) * 0.5f;
        float tileWHalf = tileW * 0.5f;
        float tileHHalf = tileH * 0.5f;
    
        for(int i = 0; i< map.Length; ++i)
        {
            GameObject tileGo =
            Instantiate(tilePrefab, 
            new Vector3(
            startX + (tileW * (i % mapWidth)) + tileWHalf,
            startY - (tileH * (i / mapWidth)) - tileWHalf,
            0f), 
            Quaternion.identity);

            tileGo.name = "Tile : " + i;
            tileGo.transform.parent = transform;

            Tile tile = tileGo.GetComponent<Tile>();
            tile.SetTileWithIndex(map[i]);
            tileList.Add(tileGo);

        }
    }

    public Vector3 GetStartPosition()
    {
        //시작점에 있는 인덱스 찾기 2가 나올때 까지 
     int startIndex = GetStartPositionIndex();
      
        if (startIndex == -1) 
        {
            Debug.LogError("sf");
        }
        return tileList[startIndex].transform.position; // 해당 인덱스의 포지션을 반환
    }
    public int GetStartPositionIndex()
    {
        for(int i = 0; i < eventmap.Length; ++i)
        {
            if (eventmap[i]==2)
            {
                return i;
            }
        }
        return -1;
    }

    public bool CheckMovable(int _curIdx, Player.Edir _dir)
    {
        int checkIdx = _curIdx;
        switch(_dir)
        {
            case Player.Edir.Left:
                if(checkIdx % mapWidth !=0)
                    checkIdx -= 1;
                break;
            case Player.Edir.Right:
                if (checkIdx % mapWidth != mapWidth - 1)
                    checkIdx += 1;
                break;
            case Player.Edir.Up:
                if (checkIdx > mapWidth-1)
                    checkIdx -= mapWidth;
                break;
            case Player.Edir.Down:
                if (checkIdx < eventmap.Length - mapWidth)
                    checkIdx += mapWidth;
                break;
        }
        if (checkIdx == _curIdx) return false;

        return eventmap[checkIdx] != 1;
    }
    // out , ref
    public void MoveToDir(ref int _curIdx, Player.Edir _dir)
    {
        switch (_dir)
        {
            case Player.Edir.Left:
                if (_curIdx % mapWidth != 0)
                    _curIdx -= 1;
                break;
            case Player.Edir.Right:
                if (_curIdx % mapWidth != mapWidth - 1)
                    _curIdx += 1;
                break;
            case Player.Edir.Up:
                if (_curIdx > mapWidth - 1)
                    _curIdx -= mapWidth;
                break;
            case Player.Edir.Down:
                if (_curIdx < eventmap.Length - mapWidth)
                    _curIdx += mapWidth;
                break;
        }
    }
    public Vector3 GetPositionFromIndex(int _idx)
    {
        return tileList[_idx].transform.position;
    }



}
