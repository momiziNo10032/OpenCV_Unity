using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp.Demo;
using OpenCvSharp;
using OpenCvSharp.Aruco;

public class Face_get : MonoBehaviour
{
    public Texture2D texture;
    void Start()
    {
        // 画像読み込み
        Mat mat = OpenCvSharp.Unity.TextureToMat(this.texture);
        // グレースケール
        Mat gray = new Mat();
        Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);

        // カスケード分類器の準備
        CascadeClassifier haarCascade = new CascadeClassifier("Assets/OpenCV+Unity/Demo/Face_Detector/haarcascade_frontalface_default.xml");

        // 顔検出
        OpenCvSharp.Rect[] faces = haarCascade.DetectMultiScale(gray);

        // 顔の位置を描画
        foreach (OpenCvSharp.Rect face in faces)
        {
            Cv2.Rectangle(mat, face, new Scalar(0, 0, 255), 3);
        }

        // 書き出し
        Texture2D outTexture = new Texture2D(mat.Width, mat.Height, TextureFormat.ARGB32, false);
        OpenCvSharp.Unity.MatToTexture(mat, outTexture);

        // 表示
        GetComponent<RawImage>().texture = outTexture;
    }
}
