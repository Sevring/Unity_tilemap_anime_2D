using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int imagewidth = 511; // ���� �� �ȼ�
    private int imageHeight = 256; // ���� �� �ȼ� 

    //�������� : ������ 
    private int colCnt = 8; //���� ��
    private int rowCnt = 4; //���� ��

    private float tileWidth = 0f; // Ÿ�� ��ĭ�� ���� ���� 
    private float tileHeight = 0f; // Ÿ�� ��ĭ�� ���� ���� 
    private float tileU = 0f; // Ÿ����ĭ�� ���� ���̸� ��ü�� ���̷� ���� -> ���� ������ ����
    private float tileV = 0f; // Ÿ�� ��ĭ�� ���� ���̸� ��ü�� ���� ���̷� ���� -> ���� ������ ����

    private MeshFilter mf = null; // �Ž� ���� �Ž����� ���� 

    private void Awake()
    {
        //Ÿ���� �� ���ϱ�
        tileWidth = (float)imagewidth / colCnt; 
        tileHeight = (float)imageHeight / rowCnt;
        tileU = tileWidth / imagewidth;
        tileV = tileHeight / imageHeight;

        mf = GetComponent<MeshFilter>();

        //SetTileWithIndex(8);

    }

    public void SetTileWithIndex (int _idx)
    {
        int col = _idx % colCnt; // Ÿ�ϸ� ���� ���� �ε��� ��ġ 
        int row = _idx / colCnt; // Ÿ�ϸ� ���� ���� �ε��� ��ġ

        float startU = col * tileU; // 
        float startV = row * tileV;

        mf.mesh.uv = new Vector2[]
        {

            //1,2,3,4 ��° �� 
            //7 9 1 3 ������ ����(Z)���
            new Vector2(startU, startV),
            new Vector2(startU+tileU, row*tileV),
            new Vector2(startU,startV +tileV),
            new Vector2(startU+tileU, startV + tileV),
        };

    }



}
