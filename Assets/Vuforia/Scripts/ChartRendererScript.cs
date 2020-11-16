using System.Collections;
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
        private float rangeY = luisScale * 1f;
        private Vector3 textScale = new Vector3(luisScale * 0.1f, luisScale * 0.1f, luisScale * 0.3f);

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void renderBars(GameObject parent, List<string> dims, List<string> metric)
        {
            List<string> uniqueValues = dims.Distinct().ToList();
            float xStepSize = rangeX / uniqueValues.Count;
            float xStart = 0-rangeX/2;
            for (int i = 0; i < uniqueValues.Count; i++)
            {
                GameObject gObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                gObject.transform.parent = parent.transform;
                gObject.transform.rotation = parent.transform.rotation;
                gObject.transform.localScale = commonScale;
                gObject.transform.localPosition = new Vector3(xStart+i*xStepSize, commonScale.y, 0.0f);
            }
            
        }

        public void renderChoice(GameObject parent, List<string> list1)
        {
            //list1 = new List<string>();
            //list1.Add("oi");
            //list1.Add("ooi");
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

        }
    }
}