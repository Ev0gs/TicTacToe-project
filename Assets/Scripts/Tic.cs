using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tic : MonoBehaviour
{
    public void tic()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.5f);
    }
}
