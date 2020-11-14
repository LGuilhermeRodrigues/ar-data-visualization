using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RenderSpace
{
    public class ChartRendererScript : MonoBehaviour
    {
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
            for (int i = 0; i < 0; i++)//uniqueValues.Count
            {

                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                gameObject.transform.parent = parent.transform;
                gameObject.transform.position = parent.transform.position;
                gameObject.transform.rotation = this.gameObject.transform.rotation;
                //gameObject.transform.position = new Vector3(gameObject.transform.position.x + i * 0.02f, gameObject.transform.position.y, gameObject.transform.position.z + i * 0.02f);
                gameObject.transform.localScale = this.gameObject.transform.localScale;
            }

            GameObject gObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            gObject.transform.parent = parent.transform;
            gObject.transform.position = parent.transform.position;
            gObject.transform.rotation = parent.transform.rotation;
            gObject.transform.localScale = new Vector3(1,1,1);
        }
    }
}