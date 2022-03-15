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

        internal static void EditSingle(this StreamWriter writer, object name, object value)
        {
            writer.WriteLine(name);
            if (value is float Val)
            {
                writer.WriteLine(Val.ToString(CultureInfo.InvariantCulture));
            }
            else
                writer.WriteLine(value);
            writer.WriteLine("");
        }

        internal static void EditList(this StreamWriter writer, object name, List<string> values)
        {
            writer.WriteLine(name);
            writer.WriteLine(values.Count);

            foreach (object value in values)
            {
                writer.WriteLine(value);
            }
            writer.WriteLine("");
        }

        internal static void EditList(this StreamWriter writer, object name, List<int> values)
        {
            writer.WriteLine(name);
            writer.WriteLine(values.Count);

            foreach (object value in values)
            {
                writer.WriteLine(value);
            }
            writer.WriteLine("");
        }
    }
}
