using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DatasetSpace
{
    public class DatasetConnectorScript : MonoBehaviour
    {

        void GetTables()
        {
            Debug.Log("oioioioiosi");
        }

        public List<string> GetLines(string tableName, string colName)
        {
            bool firstLine = true;
            int index = 0;
            var numbers = new List<string>();
            using (var reader = new StreamReader("Assets\\Tables\\"+tableName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    
                    // do something with line... could even do a yield here if you're reading a large file
                    if (firstLine)
                    {
                        Debug.Log("header line=" + line);
                        index = Array.IndexOf(line.Split(","[0]), colName);
                        Debug.Log("col selected index =" + index);
                    }
                    else
                    {
                        Debug.Log("line=" + line);
                        var separatedLine = line.Split(","[0]);
                        if (separatedLine.Length > index)
                        {
                            numbers.Add(separatedLine[index]);
                        }
                    }
                    firstLine = false;
                }
            }

            return numbers;
            //Debug.Log("numbers="+ numbers.ToString());
        }
    }
}