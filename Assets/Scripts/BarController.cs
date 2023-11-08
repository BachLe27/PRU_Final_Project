using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    private Camera camera;
    public Transform target;

    public Gradient gradient;
    public Image fill;
    private void Start()
    {
        camera = Camera.main;
    }

    public void setValue(int value)
    {
        slider.value = value;
        if (fill)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    internal void setMaxValue(int maxValue)
    {
        slider.maxValue = maxValue;
        if (fill)
        {
            fill.color = gradient.Evaluate(1f);
        }
    }

    private void Update()
    {
        if (camera != null && target != null)
        {
            transform.rotation = camera.transform.rotation;
            transform.position = target.position + new Vector3(0, 1, 0);
        }
    }
}
