using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public struct SClip
    {
        public int imageW; //���� �̹����� ���� ������
        public int imageH; // �����̹����� ���� ������
        public int frameW; // Ÿ�� �������� ���λ�����
        public int frameH; // Ÿ�� �������� ���� ������ 
        public int colCnt; // ������ Ÿ�� ���� ���� ����
        public int rowCnt; // ������ Ÿ�� ����  ���� ���� 
        public int totalCnt; //������ �� Ÿ���� �� 

        public SClip(int _imgW, int _imgH, int _frameW, int _frameH, int _colCnt, int _rowCnt, int _totalCnt)
        {
            //�� �μ� �ٷ� �Է� ���� �� �ֵ��� ������ 
            imageW = _imgW;
            imageH = _imgH;
            frameW = _frameW;
            frameH = _frameH;
            colCnt = _colCnt;
            rowCnt = _rowCnt;
            totalCnt = _totalCnt;
        }




    }

    //�� �μ� �ʱ�ȭ 
    private SClip run = new SClip(520, 347, 74, 86, 7, 4, 27);
    private MeshFilter mf = null; // �Ž����� ���� ����

    private void Awake()
    {
        mf = GetComponent<MeshFilter>(); //��ü �Ž����� ��������

        //SetFrameWithIndex(0);//0��° ������ ȣ��
    }

    private void Start()
    {
        StartCoroutine(AnimationCoroutine());
    }
    private IEnumerator AnimationCoroutine()
    {
        float delay = 1f / run.totalCnt;
        int curFrame = 0;

        while (true)
        {
            SetFrameWithIndex(curFrame);
            ++curFrame;
            curFrame = curFrame % run.totalCnt;
            yield return new WaitForSeconds(delay);
        }
    }


    private void SetFrameWithIndex(int _idx)
    {
        float frameSizeW = (float)run.frameW / run.imageW; // 
        float frameSizeH = (float)run.frameH / run.imageH;
        float startU = (_idx % run.colCnt) * frameSizeW;
        float startV = ((run.rowCnt-1) - (_idx / run.colCnt)) * frameSizeH;


        mf.mesh.uv = new Vector2[]
        {
            //  0 - 1 
            //  |   |
            //  2 - 3
            new Vector2(startU, startV), //���� 
            new Vector2(startU + frameSizeW, startV), // x�� ++
            new Vector2(startU, startV + frameSizeH), // y�� ++
            new Vector2(startU+frameSizeW, startV+frameSizeH)// x,y�� ++,
        };

    }


}
