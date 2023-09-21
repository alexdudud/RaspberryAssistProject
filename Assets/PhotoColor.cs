using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PhotoColor : MonoBehaviour
{
    public GameObject PikedColorText;
   
    private PhotoCapture photoCaptureObject = null;
    private Dictionary<string,Color> colors = new Dictionary<string, Color>() {
        {"white",Color.white },
        {"black", Color.black},
        {"grey", new Color(192/255f,192/255f,192 / 255f,1f)},
        {"purple", Color.magenta},
        {"blue",Color.blue },
        {"green",new Color(51/255f,213/255f,125/255f,1f)},
        {"yellow", new Color(1f,1f,0f,1f)},
        {"orange", new Color(1f,0.5f,0f,1f) },
        {"red", Color.red},
        {"brown", new Color(128/255f,0f,0f,1f) }
    };
    //white(255,255,255)
    //grey (128,128,128)
    //purple (128,0,128)
    //blue (0,0,255)
    //green (51, 213, 125)
    //yellow (255,255,0)
    //orange (255,128,0)
    //red (255,0,0)
    //brown (128,0,0)
    
    void Start()
    {
        
        
    }
    void OnMouseDown()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            if (result.success)
            {
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            }
            else
            {
                Debug.LogError("Unable to start photo mode!");
            }
        }
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            // Create our Texture2D for use and set the correct resolution
            Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
            Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
            // Copy the raw image data into our target texture
            photoCaptureFrame.UploadImageDataToTexture(targetTexture);
            // Do as we wish with the texture such as apply it to a material, etc.
            GetComponent<Renderer>().material.SetTexture("MainTexture", targetTexture);
            GetComponent<Renderer>().material.mainTexture = targetTexture;
            Color middleColor = targetTexture.GetPixel((int)(targetTexture.width / 2), (int)(targetTexture.height / 2));

            var fillColorArray = targetTexture.GetPixels();

            for (var i = 0; i < fillColorArray.Length; ++i)
            {
                fillColorArray[i] = middleColor;
            }

            targetTexture.SetPixels(fillColorArray);

            //targetTexture.Apply();

            float red = middleColor.r ;
            float green = middleColor.g ;
            float blue = middleColor.b ;
            string definedColor = "none";
            float delta = 3;
            Debug.Log(red +" , "+ green + " , " + blue);
            PikedColorText.GetComponent<TextMeshPro>().text = "Picked Color : " + middleColor.ToString();
            foreach (var color in colors)
            {
                float tmpDelta = Mathf.Abs(red - color.Value.r) + Mathf.Abs(green - color.Value.g) + Mathf.Abs(blue - color.Value.b);
                Debug.Log(color.Key+" : "+ tmpDelta);
                if (tmpDelta < delta) { 
                    delta = tmpDelta;
                    definedColor = color.Key;
                }
                
            }
            Debug.Log(definedColor);


        }
        // Clean up
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }



    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
