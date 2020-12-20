using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Animation transtionImage;

    public void FadeToBlack()
    {
        transtionImage.Play("FadeToBlack");
    }
    public void FadeToTransparent()
    {
        transtionImage.Play("FadeToTransparent");
    }
}
