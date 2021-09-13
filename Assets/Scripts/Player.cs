using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Edir { Left, Right, Up, Down }

    [SerializeField] private Tilemap tilemap = null;
    //현재 맵상의 인덱스 
    private int curIndex = -1; 

    private void Start()
    {
        Vector2 startPos = tilemap.GetStartPosition(); // 타일맵의 겟스타트 포지션값 가져오기 
        transform.position = startPos; //타일맵에서 받은 포지션값을 대입 

        curIndex = tilemap.GetStartPositionIndex(); //현재 플레이어의 인덱스를 저장함.

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveWithDir(Edir.Left);
        else if (Input.GetKeyDown (KeyCode.RightArrow))
            MoveWithDir(Edir.Right);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveWithDir(Edir.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveWithDir(Edir.Down);

    }
    private void MoveWithDir(Edir  _dir)
    {
        if(tilemap.CheckMovable(curIndex,_dir))
        {
            tilemap.MoveToDir(ref curIndex, _dir);
            transform.position = tilemap.GetPositionFromIndex(curIndex);
        }
    }


}
