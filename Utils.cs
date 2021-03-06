using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYSSaveUtils
{
    internal static class Utils
    {
        internal static int GetSaveID(this StreamReader reader)
        {
            string line;
            line = reader.ReadLine();
            return Convert.ToInt32(line);
        }

        internal static string GetString(this StreamReader reader, string name)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == name)
                {
                    string variable = reader.ReadLine();
                    if (variable == null)
                        throw new NullReferenceException("No match found.");
                    return variable;
                }
            }
        }

        internal static int GetInt(this StreamReader reader, string name)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == name)
                {
                    string variable = reader.ReadLine();
                    if (variable == null)
                        throw new NullReferenceException("No match found.");
                    return int.Parse(variable.Trim());
                }
            }
        }

        internal static float GetFloat(this StreamReader reader, string name)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == name)
                {
                    string variable = reader.ReadLine();
                    if (variable == null)
                        throw new NullReferenceException("No match found.");
                    return float.Parse(variable, CultureInfo.InvariantCulture);
                }
            }
        }

        internal static bool GetBool(this StreamReader reader, string name)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == name)
                {
                    string variable = reader.ReadLine();
                    if (variable == null)
                        throw new NullReferenceException("No match found.");
                    return int.Parse(variable.Trim()) != 0;
                }
            }
        }

        internal static List<string> GetStringList(this StreamReader reader, string name)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == name)
                {
                    int LineLen = int.Parse(reader.ReadLine());
                    List<string> returnList = new List<string>();
                    for (int i = 0; i < LineLen; i++)
                        returnList.Add(reader.ReadLine());
                    return returnList;
                }
            }
        }

        internal static List<int> GetIntList(this StreamReader reader, string name)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == name)
                {
                    int LineLen = int.Parse(reader.ReadLine());
                    List<int> returnList = new List<int>();
                    for (int i = 0; i < LineLen; i++)
                        returnList.Add(Convert.ToInt32(reader.ReadLine()));
                    return returnList;
                }
            }
        }

        internal static List<KeyBindsContent.KeyBind> GetKeyBindList(this StreamReader reader, string name)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == name)
                {
                    List<int> pairList = new List<int>();
                    string line2 = reader.ReadLine();
                    while (line2 != "")
                    {
                        pairList.Add(Convert.ToInt32(line2));
                        line2 = reader.ReadLine();
                    }

                    List<KeyBindsContent.KeyBind> returnList = new List<KeyBindsContent.KeyBind>();
                    for (int i = 0; i < pairList.Count; i += 2)
                    {
                        KeyBindsContent.KeyBind key = new KeyBindsContent.KeyBind
                        {
                            Key = (KeyBindsContent.Key)pairList[i],
                            Type = (KeyBindsContent.KeyType)pairList[i + 1]
                        };
                        returnList.Add(key);
                    }
                    return returnList;
                }
            }
        }

        internal static void EditSingle(this StreamWriter writer, object name, object value)
        {
            writer.WriteLine(name);
            if (value is float ValInt)
                writer.WriteLine(ValInt.ToString(CultureInfo.InvariantCulture));
            else if (value is bool ValBool)
                writer.WriteLine(Convert.ToInt32(ValBool));
            else
                writer.WriteLine(value);
            writer.WriteLine("");
        }

        internal static void EditList(this StreamWriter writer, object name, List<string> values)
        {
            writer.WriteLine(name);
            writer.WriteLine(values.Count);

            foreach (object value in values)
                writer.WriteLine(value);
            writer.WriteLine("");
        }

        internal static void EditList(this StreamWriter writer, object name, List<int> values)
        {
            writer.WriteLine(name);
            writer.WriteLine(values.Count);

            foreach (object value in values)
                writer.WriteLine(value);
            writer.WriteLine("");
        }

        internal static void EditKeyBinds(this StreamWriter writer, string name, List<KeyBindsContent.KeyBind> values)
        {
            writer.WriteLine(name);
            
            foreach (KeyBindsContent.KeyBind value in values)
            {
                writer.WriteLine((int)value.Key);
                writer.WriteLine((int)value.Type);
            }
            writer.WriteLine("");
        }
    }
}
