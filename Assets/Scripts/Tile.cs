using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int imagewidth = 511; // 가로 행 픽셀
    private int imageHeight = 256; // 세로 열 픽셀 

    //임의지정 : 조각수 
    private int colCnt = 8; //가로 행
    private int rowCnt = 4; //세로 열

    private float tileWidth = 0f; // 타일 한칸의 가로 길이 
    private float tileHeight = 0f; // 타일 한칸의 세로 길이 
    private float tileU = 0f; // 타일한칸의 가로 길이를 전체의 길이로 나눔 -> 가로 비율이 나옴
    private float tileV = 0f; // 타일 한칸의 세로 길이를 전체의 세로 길이로 나눔 -> 세로 비율이 나옴

    private MeshFilter mf = null; // 매쉬 받을 매쉬필터 변수 

    private void Awake()
    {
        //타일의 폭 구하기
        tileWidth = (float)imagewidth / colCnt; 
        tileHeight = (float)imageHeight / rowCnt;
        tileU = tileWidth / imagewidth;
        tileV = tileHeight / imageHeight;

        mf = GetComponent<MeshFilter>();

        //SetTileWithIndex(8);

    }

    public void SetTileWithIndex (int _idx)
    {
        int col = _idx % colCnt; // 타일맵 기준 가로 인덱스 위치 
        int row = _idx / colCnt; // 타일맵 기준 세로 인덱스 위치

        float startU = col * tileU; // 
        float startV = row * tileV;

        mf.mesh.uv = new Vector2[]
        {

            //1,2,3,4 번째 점 
            //7 9 1 3 순으로 진행(Z)모양
            new Vector2(startU, startV),
            new Vector2(startU+tileU, row*tileV),
            new Vector2(startU,startV +tileV),
            new Vector2(startU+tileU, startV + tileV),
        };

    }



}
