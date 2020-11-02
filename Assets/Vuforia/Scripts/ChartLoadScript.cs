using System;
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

        private bool isLoaded = false;

        // Start is called before the first frame update
        public void Load(string imageTargetName)
        {
            if (isLoaded == false)
            {
                isLoaded = true;

                bool somethingMissing = false;

                string[] keys = { imageTargetName+"_chartName", imageTargetName + "_datasource", imageTargetName + "_metrics", imageTargetName + "_dimensions" };

                foreach (string k in keys)
                {
                    if (!PlayerPrefs.HasKey(k))
                    {
                        somethingMissing = true;
                    } else
                    {

                    }
                }

                if (somethingMissing)
                {
                    showOptions();
                }
                else
                {
                    // render chart
                    renderChart();
                }
                int[] data = { 1, 2, 3, 5, 5, 6, 3, 1 };
                for (int i = 0; i < 10; i++)
                {

                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    gameObject.transform.parent = parentObject.transform;
                    gameObject.transform.position = parentObject.transform.position;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + i * 0.02f, gameObject.transform.position.y, gameObject.transform.position.z + i * 0.02f);
                    gameObject.transform.rotation = this.gameObject.transform.rotation;
                    gameObject.transform.localScale = this.gameObject.transform.localScale;
                }
            } 
        }

        private void showOptions()
        {
            // there is only one chart in project
            if (PlayerPrefs.HasKey(imageTargetName + "_chartName"))
            {
                chartName = PlayerPrefs.GetString(imageTargetName + "_chartName");
                if (somethingMissing)
                {
                    createOption("Chart Name", chartName);
                }

            }
            else
            {
                chartName = "bars";
                if (somethingMissing)
                {
                    createOption("Chart Name", chartName);
                }
            }


            // there is only one datasource in project
            if (PlayerPrefs.HasKey(imageTargetName + "_datasource"))
            {
                datasource = PlayerPrefs.GetString(imageTargetName + "_datasource");
                if (somethingMissing)
                {
                    createOption("Chart Name", datasource);
                }
            }
            else
            {
                //createOptions("Chart Name", getDatasources());
                datasource = "papers.csv";
                createOption("Chart Name", datasource);
            }


            // metrics according to the datasource
            if (PlayerPrefs.HasKey(imageTargetName + "_metric1"))
            {
                metrics = { PlayerPrefs.GetString(imageTargetName + "_metric1")};
                if (somethingMissing)
                {
                    createOption("Metrics", metrics{ });
                }
            }
            else
            {
                //createOptions("Chart Name", getDatasources());
                datasource = "papers.csv";
                createOption("Chart Name", datasource);
            }
        }

        private void createOption(string optionName, string selectedOption)
        {
            throw new NotImplementedException();
        }

        private string[] getDatasources()
        {
            string[] datsources = { "papers.csv" };
            return datsources;
        }

        private void createOptions(string optionName, string[] options)
        {
            throw new NotImplementedException();
        }

        // Update is called once per frame
        void renderChart()
        {

        }

    }
}
