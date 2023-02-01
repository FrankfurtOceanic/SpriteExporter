using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class SpriteExporter : MonoBehaviour
{
    public int numberOfFrames = 24;
    public int framesPerSecond = 24;
    public string filePath;
    public string fileName;

    private RenderTexture rTex;

    private float time =  0;
    private int currentFrame = 1;
    // Start is called before the first frame update
    void Start()
    {
        rTex = GetComponent<Camera>().targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
      
        if (currentFrame <= numberOfFrames)
        {
            
            
            if (!AssetDatabase.IsValidFolder("Assets/" + filePath))
            {
                AssetDatabase.CreateFolder("Assets", filePath);

            }







            time += Time.deltaTime;
            if (time > 1 / framesPerSecond)
            {
                Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);

                // Remember currently active render texture
                RenderTexture currentActiveRT = RenderTexture.active;

                RenderTexture.active = rTex;
                tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);

                // Restore previously active render texture
                RenderTexture.active = currentActiveRT;



                byte[] bytes = tex.EncodeToPNG();

                //creating folder if it doesn't exist

                Debug.Log(Application.dataPath + "/" + filePath + "/" + fileName+currentFrame);
                File.WriteAllBytes(Application.dataPath + "/" + filePath + "/" + fileName + currentFrame + ".png", bytes);

                if (currentFrame == numberOfFrames)
                {
                    Debug.Log("Export Complete");
                }
                currentFrame++;
            }
        }
        
        
    }
}
