using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SimpleConfig
{
    private static Dictionary<string, Dictionary<string, string>> dataDict = new Dictionary<string, Dictionary<string, string>>();

    public static void Load(string configFilePath)
    {
        var enumer = dataDict.GetEnumerator();
        while (enumer.MoveNext())
        {
            enumer.Current.Value.Clear();
        }
        dataDict.Clear();

        System.IO.StreamReader sr = new System.IO.StreamReader(configFilePath);
        string line;
        string currSection = null;
        char[] splitor = new char[] { '=' };
        while((line = sr.ReadLine()) != null)
        {
            line = line.Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if(line.StartsWith("[") && line.EndsWith("]"))
            {
                currSection = line.Substring(1, line.Length - 2);
                if (!dataDict.ContainsKey(currSection))
                {
                    dataDict.Add(currSection, new Dictionary<string, string>());
                }
            }
            else
            {
                string[] keyAndValue = line.Split(splitor, 2);
                if(keyAndValue == null || keyAndValue.Length < 2)
                {
                    continue;
                }

                if(currSection == null)
                {
                    currSection = "Global";
                    if (!dataDict.ContainsKey(currSection))
                    {
                        dataDict.Add(currSection, new Dictionary<string, string>());
                    }
                }

                string key = keyAndValue[0].Trim();
                string value = keyAndValue[1].Trim();

                if (dataDict[currSection].ContainsKey(key))
                {
                    Debug.LogErrorFormat("在 {0} 部分已经包含名为 {1} 的变量！", currSection, key);
                    continue;
                }

                dataDict[currSection].Add(key, value);
            }
        }
    }

    public static int GetIntValue(string variable)
    {
        return GetIntValue("Global", variable);
    }

    public static int GetIntValue(string section, string variable)
    {
        if (dataDict.ContainsKey(section) && dataDict[section].ContainsKey(variable))
        {
            string rawValue = dataDict[section][variable];
            int value = 0;
            if (int.TryParse(rawValue, out value))
            {
                return value;
            }
        }

        Debug.LogErrorFormat("解析 {0} 部分的 {1} int 值出错", section, variable);
        return -1;
    }


    public static float GetFloatValue(string variable)
    {
        return GetFloatValue("Global", variable);
    }

    public static float GetFloatValue(string section, string variable)
    {
        if (dataDict.ContainsKey(section) && dataDict[section].ContainsKey(variable))
        {
            string rawValue = dataDict[section][variable];
            float value = 0;
            if (float.TryParse(rawValue, out value))
            {
                return value;
            }
        }

        Debug.LogErrorFormat("解析 {0} 部分的 {1} float 值出错", section, variable);
        return -1;
    }


    public static string GetStringValue(string variable)
    {
        return GetStringValue("Global", variable);
    }

    public static string GetStringValue(string section, string variable)
    {
        if (dataDict.ContainsKey(section) && dataDict[section].ContainsKey(variable))
        {
            string rawValue = dataDict[section][variable];
            return rawValue;
        }

        Debug.LogErrorFormat("解析 {0} 部分的 {1} string 值出错", section, variable);
        return string.Empty;
    }

    public static bool GetBoolValue(string variable)
    {
        return GetBoolValue("Global", variable);
    }

    public static bool GetBoolValue(string section, string variable)
    {
        if (dataDict.ContainsKey(section) && dataDict[section].ContainsKey(variable))
        {
            string rawValue = dataDict[section][variable].ToLower();
            bool value = false;
            if (bool.TryParse(rawValue, out value))
            {
                return value;
            }
        }

        Debug.LogErrorFormat("解析 {0} 部分的 {1} bool 值出错", section, variable);
        return false;
    }
}
