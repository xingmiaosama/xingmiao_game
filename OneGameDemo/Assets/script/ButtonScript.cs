using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonScript : MonoBehaviour
{
    [Range(0, 1)]
    public float imageDisplay;
    public Image testImage;

    public Image image;
    private bool imageColorChanged;

    public Button myButton;
    public TextMeshProUGUI buttonText;

    // Update is called once per frame
    void Update()
    {
        ImageChange();
    }

    public void ImageChange()
    {
        testImage.fillAmount = imageDisplay;
    }

    public void ButtonClickTest()
    {
        if (!imageColorChanged)
        {
            image.color = Color.red;
            imageColorChanged = true;
        }
        else
        {
            image.color = new Color(255, 255, 255, 255);
            imageColorChanged = false;
        }
    }
}
