using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DatasetSpace
{
    public class DatasetConnectorScript : MonoBehaviour
    {



        public List<string> GetTables()
        {
            var tables = new List<string>();
            Debug.Log("oioioioiosi");
            tables.Add("homes");
            return tables;
        }

        public List<string> GetLines(string tableName, string colName)
        {
            bool firstLine = true;
            int index = 0;
            var numbers = new List<string>();
            var textFile = Resources.Load<TextAsset>(tableName);
            var lines = textFile.text.Split("\n"[0]);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (firstLine)
                {
                    Debug.Log("header line=" + line);
                    index = Array.IndexOf(line.Split(","[0]), colName);
                    Debug.Log("col selected index =" + index);
                    firstLine = false;
                }
                else
                {
                    var separatedLine = line.Split(","[0]);
                    //Debug.Log("line with lenght"+ separatedLine.Length + "=" + line);
                    if (separatedLine.Length > 1)
                    {
                        //Debug.Log("line added = ("+ separatedLine[index] + ")");
                        numbers.Add(separatedLine[index]);
                    } else
                    {
                        //Debug.Log("line ignored");
                    }
                }
            }

            return numbers;
            //Debug.Log("numbers="+ numbers.ToString());
        }

        public List<string> GetSchema(string tableName)
        {
            var textFile = Resources.Load<TextAsset>(tableName);
            var lines = textFile.text.Split("\n"[0]); 
            return new List<string>(lines[0].Split(","[0]));
        }
    }
}