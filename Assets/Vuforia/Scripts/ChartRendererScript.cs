using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RenderSpace
{
    public class ChartRendererScript : MonoBehaviour
    {
        static float luisScale = 1f;
        private Vector3 commonScale = new Vector3(luisScale*0.3f, luisScale* 0.3f, luisScale*0.3f);
        private float rangeX = luisScale*2f;
        private float rangeY = luisScale * 1f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void renderBars(GameObject parent, List<string> list1, List<string> list2)
        {
            List<string> uniqueValues = list1.Distinct().ToList();
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
    }
}