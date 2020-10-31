using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyNameSpace {

    public class ChartLoadScript : MonoBehaviour
    {

        public GameObject parentObject;

        private string chartName;

        private string datasource;

        private string[] metrics;

        private string[] dimensions;

        // Start is called before the first frame update
        public void Load(string imageTargetName)
        {
            int[] data = { 1, 2, 3, 5, 5, 6, 3, 1 };
            for (int i = 0; i < 10; i++)
            {
                
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                gameObject.transform.parent = parentObject.transform;
                gameObject.transform.position = parentObject.transform.position;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x+i*0.02f, gameObject.transform.position.y, gameObject.transform.position.z + i * 0.02f);
                gameObject.transform.rotation = this.gameObject.transform.rotation;
                gameObject.transform.localScale = this.gameObject.transform.localScale;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
