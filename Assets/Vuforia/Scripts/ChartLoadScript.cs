using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatasetSpace;
using System.Linq;
using RenderSpace;

namespace MyNameSpace {

    public class ChartLoadScript : MonoBehaviour
    {

        public GameObject parentObject;

        private string chartNames;

        private string chartName;

        private string datasources;

        private string datasource;

        private int metricNum;

        private int dimNum;

        private IDictionary<string, string> metrics = new Dictionary<string, string>();
        private IDictionary<string, string> dims = new Dictionary<string, string>();

        private string[] dimensions;

        private bool isLoaded = false;

        // Start is called before the first frame update
        public void Load(string imageTargetName)
        {
            if (isLoaded == false)
            {
                isLoaded = true;

                bool somethingMissing = false;

                PlayerPrefs.SetString(imageTargetName + "_chartName", "Bars");
                PlayerPrefs.SetString(imageTargetName + "_datasource", "homes.csv");
                chartName = "Bars";
                datasource = "homes.csv";

                string[] keys = { imageTargetName+"_chartName", imageTargetName + "_datasource"};

                foreach (string k in keys)
                {
                    if (!PlayerPrefs.HasKey(k))
                    {
                        somethingMissing = true;
                    }
                }

                // Choose the Tipe of the Chart and The Table
                if (somethingMissing)
                {
                    //showOptions(imageTargetName, somethingMissing);
                }
                else
                {
                    //renderChart();
                }

                

                if (string.Equals(chartName, "Bars"))
                {
                    metricNum = 1;

                    dimNum = 1;
                }

                for (int i = 0; i < metricNum; i++)
                {
                    // check misssing
                    metrics.Add("metric"+i, "Sell");
                }
                for (int i = 0; i < dimNum; i++)
                {
                    dims.Add("dim" + i, "Beds");
                }

                if (somethingMissing)
                {
                    Debug.Log("Somethin is missing on chart");
                    //showOptions(imageTargetName, somethingMissing);
                }
                else
                {
                    renderChart(datasource,chartName,metrics,dims);
                }
                int[] data = { 1, 2, 3, 5, 5, 6, 3, 1 };
                for (int i = 0; i < 0; i++)
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

        private void renderChart(string datasource, string chartName, IDictionary<string, string> metrics, IDictionary<string, string> dims)
        {
            Debug.Log("Datasource="+datasource);
            var datasetScript = (DatasetConnectorScript)FindObjectOfType(typeof(DatasetConnectorScript));

            var renderScript = (ChartRendererScript)FindObjectOfType(typeof(ChartRendererScript));

            if (string.Equals(chartName, "Bars"))
            {

                var list1 = datasetScript.GetLines(datasource, dims["dim0"]);
                //var d = extractValues(datasource, metrics["metric0"]);
                var list2 = datasetScript.GetLines(datasource, metrics["metric0"]);
                renderScript.renderBars(parentObject,list1, list2);
            }
        }

        private void showOptions(string imageTargetName, bool somethingMissing)
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
                //metrics[0] = PlayerPrefs.GetString(imageTargetName + "_metric1");
                if (somethingMissing)
                {
                    //createOption("Metrics", metrics[0]);
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


    }
}
