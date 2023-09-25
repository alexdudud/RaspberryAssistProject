using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class changeColor : MonoBehaviour
{

    public GameObject SizeText;
    public GameObject ColorText;
    public GameObject ObjectTracked;



    // Start is called before the first frame update
    void Start()
    {
    }


    public void ColorUp(GameObject test)
    {
        var objectRenderer = test.GetComponent<Renderer>();
        objectRenderer.material.color += new Color(0.1f, 0.1f, 0.1f);
        ColorText.GetComponent<TextMeshPro>().text = "Actual Color :\n" + ObjectTracked.GetComponent<Renderer>().material.color;

    }
    public void ColorDown(GameObject test)
    {
        var objectRenderer = test.GetComponent<Renderer>();
        objectRenderer.material.color -= new Color(0.1f, 0.1f, 0.1f);
        ColorText.GetComponent<TextMeshPro>().text = "Actual Color :\n" + ObjectTracked.GetComponent<Renderer>().material.color;

    }
    public void ScaleUp(GameObject test)
    {
        test.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        SizeText.GetComponent<TextMeshPro>().text = "Actual Size :" + ObjectTracked.transform.localScale;
    }
    public void ScaleDown(GameObject test)
    {
        if (test.transform.localScale.x - 0.1f > 0.00f && test.transform.localScale.y - 0.1f > 0.00f && test.transform.localScale.z - 0.1f > 0.00f)
        {
            test.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

        }
        SizeText.GetComponent<TextMeshPro>().text = "Actual Size :" + test.transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3 middleScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);


        //Ray ray = camera.ScreenPointToRay(middleScreen);
        //Debug.DrawRay(Camera.current.transform.position, ray.direction, Color.blue);
        //RenderTexture renderTexture = Camera.current.targetTexture;
        //Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //RenderTexture.active = renderTexture;
        ////texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        //texture.Apply();
        //SizeText.GetComponent<TextMeshPro>().text = texture.GetPixel(Screen.width / 2, Screen.height / 2).ToString();
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Transform objectHit = hit.transform;

        //    // Do something with the object that was hit by the raycast.
        //}

    }
}
