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

        

        private string[] dimensions;

        public bool isLoaded = false;

        // Start is called before the first frame update
        public void Load(string imageTargetName)
        {

            IDictionary<string, string> metrics = new Dictionary<string, string>();
            IDictionary<string, string> dims = new Dictionary<string, string>();

            if (isLoaded == false)
            {
                bool somethingMissing = false;

                chartName = "Bars";
                datasource = "homes";
                PlayerPrefs.SetString(imageTargetName + "_chartName", chartName);
                PlayerPrefs.SetString(imageTargetName + "_datasource", datasource);

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

                string metricNeeded;
                for (int i = 0; i < metricNum; i++)
                {
                    metricNeeded = imageTargetName + "_metric" + i;
                    if (!PlayerPrefs.HasKey(metricNeeded))
                    {
                        somethingMissing = true;
                        Debug.Log("_metric " + i + " is missing on chart");
                        var datasetScript = (DatasetConnectorScript)FindObjectOfType(typeof(DatasetConnectorScript));
                        var renderScript = (ChartRendererScript)FindObjectOfType(typeof(ChartRendererScript));
                        List<string> dimNames = datasetScript.GetSchema(datasource);
                        List<string> storedNames = dimNames
                            .Select(t => imageTargetName + "_metric" + i + ":" + t).ToList();

                        renderScript.renderChoice(parentObject, storedNames, "Choose Metric " + (i + 1));
                    }
                    else
                    {
                        Debug.Log("PlayerPrefs found  " + imageTargetName + "_metric " + i + " is " + PlayerPrefs.GetString(metricNeeded));
                        metrics.Add("metric" + i, PlayerPrefs.GetString(metricNeeded));
                    }
                }

                if (somethingMissing)
                {
                    Debug.Log("Metric is missing on chart");
                    return;
                }

                string dimNeeded;
                for (int i = 0; i < dimNum; i++)
                {
                    dimNeeded = imageTargetName + "_dim" + i;
                    if (!PlayerPrefs.HasKey(dimNeeded))
                    {
                        somethingMissing = true;
                        Debug.Log("_dim " + i+" is missing on chart");
                        var datasetScript = (DatasetConnectorScript)FindObjectOfType(typeof(DatasetConnectorScript));
                        var renderScript = (ChartRendererScript)FindObjectOfType(typeof(ChartRendererScript));
                        List<string> dimNames = datasetScript.GetSchema(datasource);
                        List<string> storedNames = dimNames
                            .Select(t => imageTargetName + "_dim" + i + ":" + t).ToList();

                        renderScript.renderChoice(parentObject, storedNames, "Choose Dimension "+(i+1));
                    }else
                    {
                        Debug.Log("PlayerPrefs found  "+imageTargetName+"_dim " + i + " is "+ PlayerPrefs.GetString(dimNeeded));
                        dims.Add("dim" + i,PlayerPrefs.GetString(dimNeeded));
                    }
                    //dims.Add("dim" + i, "Beds");
                }

                if (somethingMissing)
                {
                    Debug.Log("Somethin is missing on chart");
                }
                else
                {
                    renderChart(datasource,chartName,metrics,dims);
                    isLoaded = true;
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
                var axes = new List<string>();
                axes.Add(metrics["metric0"]);
                axes.Add(dims["dim0"]);
                renderScript.renderBars(parentObject,list1, list2, axes);
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
            if (PlayerPrefs.HasKey(imageTargetName + "_metric0"))
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
                datasource = "papers";
                createOption("Chart Name", datasource);
            }
        }

        private void createOption(string optionName, string selectedOption)
        {
            throw new NotImplementedException();
        }

        private string[] getDatasources()
        {
            string[] datsources = { "papers" };
            return datsources;
        }

        private void createOptions(string optionName, string[] options)
        {
            throw new NotImplementedException();
        }


    }
}
