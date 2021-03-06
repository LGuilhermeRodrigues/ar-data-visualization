﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RenderSpace
{
    public class ChartRendererScript : MonoBehaviour
    {
        static float luisScale = 1f;
        private Vector3 commonScale = new Vector3(luisScale*0.3f, luisScale* 0.3f, luisScale*0.3f);
        private float rangeX = luisScale*4f;
        private float rangeY = luisScale * 0.7f;
        private Vector3 textScale = new Vector3(luisScale * 0.1f, luisScale * 0.1f, luisScale * 0.3f);

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void renderBars(GameObject parent, List<string> dims, List<string> metricStr, List<string> axes)
        {
            Debug.Log("dims and metric lines total=" + metricStr.Count + " and "+dims.Count);
            List<float> metric = new List<float>();
            for (int i = 0; i < metricStr.Count; i++)
            {
                if (metricStr[i].Equals(""))
                {
                    metricStr[i] = "0";
                    Debug.Log("Empty value detected");
                }
                //Debug.Log("metricStr["+i+"]=" + metricStr[i]);
                if (float.TryParse(metricStr[i], out float n))
                {
                    metric.Add(float.Parse(metricStr[i]));
                } else
                {
                    Debug.Log("error parsing a metric float value ("+ metricStr[i] + ";i="+i+")");
                    metric.Add(float.Parse("0"));
                }
                
            }
            //Debug.Log("metricStr.Count=" + metricStr.Count);
            //string aggMethod = "sum";

            List<string> uniqueValues = dims.Distinct().ToList();

            IDictionary<string, float> heights = new Dictionary<string, float>();
            for (int i = 0; i < uniqueValues.Count; i++)
            {
                heights[uniqueValues[i]] = 0;
            }
            float maxHeight = 0;
            for (int i = 0; i < dims.Count; i++)
            {
                heights[dims[i]] = heights[dims[i]] + metric[i];
                if (heights[dims[i]]>maxHeight)
                {
                    maxHeight = heights[dims[i]];
                }
            }
            Debug.Log("max height ="+ maxHeight);
            List<float> uniqueHeights = new List<float>();
            List<float> aggMetrics = new List<float>();
            for (int i = 0; i < uniqueValues.Count; i++)
            {
                aggMetrics.Add(heights[uniqueValues[i]]);
                var x = heights[uniqueValues[i]] * rangeY / maxHeight;
                uniqueHeights.Add(x);
                Debug.Log("n="+ heights[uniqueValues[i]]+ " --> x=" + x);
            }


            float xStepSize = rangeX / uniqueValues.Count;
            float xStart = 0 - rangeX / 2;
            for (int i = 0; i < uniqueValues.Count; i++)
            {
                GameObject gObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                gObject.transform.parent = parent.transform;
                gObject.transform.rotation = parent.transform.rotation;
                var y = uniqueHeights[i];
                gObject.transform.localPosition = new Vector3(xStart + i * xStepSize, y, 0.0f);
                gObject.transform.localScale = new Vector3(commonScale.x, y, commonScale.z);
                gObject.name = uniqueValues[i]+"-"+aggMetrics[i];

                GameObject dimName = new GameObject(uniqueValues[i] + "_label");
                dimName.transform.parent = parent.transform;
                TextMesh text = dimName.AddComponent<TextMesh>();
                text.text = uniqueValues[i];
                text.anchor = TextAnchor.MiddleCenter;
                dimName.transform.rotation = parent.transform.rotation;
                dimName.transform.localScale = textScale;
                dimName.transform.localPosition = new Vector3(xStart + i * xStepSize, textScale.y, -commonScale.z);
            }
            
            // Rendering X axis
            
            GameObject axisX = new GameObject("axisX_label");
            axisX.transform.parent = parent.transform;
            TextMesh axisXtext = axisX.AddComponent<TextMesh>();
            axisXtext.text = axes[1];
            axisXtext.anchor = TextAnchor.MiddleCenter;
            axisX.transform.rotation = parent.transform.rotation;
            axisX.transform.localScale = textScale;
            axisX.transform.localPosition = new Vector3(0.0f, textScale.y, -commonScale.z*1.5f);

            // Rendering Y axis

            var AxisLabelYHeight = rangeY;

            GameObject axisY = new GameObject("axisY_label");
            axisY.transform.parent = parent.transform;
            TextMesh axisYtext = axisY.AddComponent<TextMesh>();
            axisYtext.text = axes[0];
            axisYtext.anchor = TextAnchor.MiddleCenter;
            axisY.transform.rotation = parent.transform.rotation;
            axisY.transform.localScale = textScale;
            axisY.transform.localPosition = new Vector3(xStart - commonScale.x , AxisLabelYHeight, -commonScale.z * 1.5f);

        }

        public void renderChoice(GameObject parent, List<string> list1, string optionName)
        {
            List<string> uniqueValues = list1.Distinct().ToList();
            float xStepSize = rangeX / uniqueValues.Count;
            float xStart = 0 - rangeX / 2;
            for (int i = 0; i < uniqueValues.Count; i++)
            {
                GameObject gObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                gObject.transform.parent = parent.transform;
                gObject.transform.rotation = parent.transform.rotation;
                gObject.transform.localScale = commonScale;
                gObject.transform.localPosition = new Vector3(xStart + i * xStepSize, commonScale.y, 0.0f);
                gObject.name = uniqueValues[i];

                GameObject dimName = new GameObject(uniqueValues[i]+"_label");
                dimName.transform.parent = parent.transform;
                TextMesh text = dimName.AddComponent<TextMesh>();
                text.text = uniqueValues[i].Split(":"[0])[1];
                text.anchor = TextAnchor.MiddleCenter;
                //MeshRenderer mesh = dimName.AddComponent<MeshRenderer>();
                dimName.transform.rotation = parent.transform.rotation;
                dimName.transform.localScale = textScale;
                dimName.transform.localPosition = new Vector3(xStart + i * xStepSize, textScale.y, -commonScale.z);
            }

            // Rendering X axis

            GameObject axisX = new GameObject("axisX_label");
            axisX.transform.parent = parent.transform;
            TextMesh axisXtext = axisX.AddComponent<TextMesh>();
            axisXtext.text = optionName;
            axisXtext.anchor = TextAnchor.MiddleCenter;
            axisX.transform.rotation = parent.transform.rotation;
            axisX.transform.localScale = textScale;
            axisX.transform.localPosition = new Vector3(0.0f, textScale.y, -commonScale.z * 1.5f);

        }
    }
}