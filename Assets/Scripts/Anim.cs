using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public struct SClip
    {
        public int imageW; //원본 이미지의 가로 사이즈
        public int imageH; // 원본이미지의 세로 사이즈
        public int frameW; // 타일 한조각의 가로사이즈
        public int frameH; // 타일 한조각의 세로 사이즈 
        public int colCnt; // 나뉘는 타일 기준 가로 순서
        public int rowCnt; // 나뉘는 타일 기준  세로 순서 
        public int totalCnt; //나뉘는 총 타일의 수 

        public SClip(int _imgW, int _imgH, int _frameW, int _frameH, int _colCnt, int _rowCnt, int _totalCnt)
        {
            //각 인수 바로 입력 받을 수 있도록 생성자 
            imageW = _imgW;
            imageH = _imgH;
            frameW = _frameW;
            frameH = _frameH;
            colCnt = _colCnt;
            rowCnt = _rowCnt;
            totalCnt = _totalCnt;
        }




    }

    //각 인수 초기화 
    private SClip run = new SClip(520, 347, 74, 86, 7, 4, 27);
    private MeshFilter mf = null; // 매쉬필터 변수 선언

    private void Awake()
    {
        mf = GetComponent<MeshFilter>(); //자체 매쉬필터 가저오기

        //SetFrameWithIndex(0);//0번째 프레임 호출
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
            new Vector2(startU, startV), //영점 
            new Vector2(startU + frameSizeW, startV), // x축 ++
            new Vector2(startU, startV + frameSizeH), // y축 ++
            new Vector2(startU+frameSizeW, startV+frameSizeH)// x,y축 ++,
        };

    }


}
