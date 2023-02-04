using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

/// <summary>
/// CSV Reader
/// raw데이터가 담긴 파일의 byte[]와 
/// 얻고자 하는 스펙 타입 하나 던저주면 
/// 데이터의 각 row를 하나의 spec화 시켜 spec의 list를 리턴해주는 클래스
/// 생성 후 ReadCSV() 하면 끝.
/// NOTE: from 애니메디
/// </summary>
/// <typeparam name="T"></typeparam>

public class CSVReader<T> where T : new()
{
    byte[] mBuffer;
    int mOffset = 0;
    string fileName;
    public CSVReader(string fileName, byte[] bytes) { this.fileName = fileName; mBuffer = bytes; }

    List<string> mTemp = new List<string>();
    public bool canRead { get { return (mBuffer != null && mOffset < mBuffer.Length); } }

    public List<T> ReadCSV()
    {
        List<string> line = new List<string>();
        Dictionary<string, int> titleIndexMap = null;
        List<List<string>> data = new List<List<string>>();

        try
        {
            //Column Titles
            line = ReadRawCSVFile();
            titleIndexMap = SetTitleIndexMap(line);

            while ((line = ReadRawCSVFile()) != null)
                data.Add(new List<string>(line));

            return GenSpecList(titleIndexMap, data);

        }
        catch (Exception e)
        {

            //Log.Notice(e.ToString());
        }
        return null;
    }
    List<string> ReadRawCSVFile()
    {
        mTemp.Clear();
        string line = "";
        bool insideQuotes = false;
        int wordStart = 0;

        while (canRead)
        {
            if (insideQuotes)
            {
                string s = ReadLine(false);
                if (s == null) return null;
                s = s.Replace("\\n", "\n");
                line += "\n" + s;
            }
            else
            {
                line = ReadLine(true);
                if (line == null) return null;
                line = line.Replace("\\n", "\n");
                wordStart = 0;
            }

            for (int i = wordStart, imax = line.Length; i < imax; ++i)
            {
                char ch = line[i];

                if (ch == ',')
                {
                    if (!insideQuotes)
                    {
                        mTemp.Add(line.Substring(wordStart, i - wordStart).Trim());
                        wordStart = i + 1;
                    }
                }
                else if (ch == '"')
                {
                    if (insideQuotes)
                    {
                        if (i + 1 >= imax)
                        {
                            mTemp.Add(line.Substring(wordStart, i - wordStart).Replace("\"\"", "\"").Trim());
                            return mTemp;
                        }

                        if (line[i + 1] != '"')
                        {
                            mTemp.Add(line.Substring(wordStart, i - wordStart).Replace("\"\"", "\"").Trim());
                            insideQuotes = false;

                            if (line[i + 1] == ',')
                            {
                                ++i;
                                wordStart = i + 1;
                            }
                        }
                        else ++i;
                    }
                    else
                    {
                        wordStart = i + 1;
                        insideQuotes = true;
                    }
                }
            }

            if (wordStart < line.Length)
            {
                if (insideQuotes) continue;
                mTemp.Add(line.Substring(wordStart, line.Length - wordStart).Trim());
            }
            return mTemp;
        }
        return null;
    }
    string ReadLine(bool skipEmptyLines)
    {
        int max = mBuffer.Length;

        // Skip empty characters
        if (skipEmptyLines)
        {
            while (mOffset < max && mBuffer[mOffset] < 32) ++mOffset;
        }

        int end = mOffset;

        if (end < max)
        {
            for (; ; )
            {
                if (end < max)
                {
                    int ch = mBuffer[end++];
                    if (ch != '\n' && ch != '\r') continue;
                }
                else ++end;

                string line = ReadLine(mBuffer, mOffset, end - mOffset - 1);
                mOffset = end;
                return line;
            }
        }
        mOffset = max;
        return null;
    }
    string ReadLine(byte[] buffer, int start, int count)
    {
        return Encoding.UTF8.GetString(buffer, start, count);
    }

    List<T> GenSpecList(Dictionary<string, int> titleIndexMap, List<List<string>> data)
    {
        List<T> specList = new List<T>();
        List<string> checkList = new List<string>();

        for (int i = 0; i < data.Count; i++)
        {
            T spec = new T();

            foreach (var map in titleIndexMap)
            {
                string titleName = map.Key;
                int titleIndex = map.Value;
                string rawString = data[i][titleIndex];
                if (rawString.Length <= 0)
                    continue;

                rawString = rawString.Replace("\\n", "\n").Replace("\\c", ",");
                titleName = titleName.Trim('\uFEFF', '\u200B');

                //Type type = spec.GetType();
                FieldInfo field = spec.GetType().GetField(titleName);
                if (field == null)
                {
                    if (!checkList.Contains(titleName))
                    {
                        checkList.Add(titleName);
                        //Log.Notice("No such spec column : {0}, filname: {1}", titleName, fileName);
                    }
                    continue;
                }

                try
                {
                    if (field.FieldType.IsEnum)
                        field.SetValue(spec, Enum.Parse(field.FieldType, rawString));
                    else if (field.FieldType == typeof(int[]))
                        field.SetValue(spec, JsonConvert.DeserializeObject<int[]>(rawString));
                    else
                        field.SetValue(spec, Convert.ChangeType(rawString, field.FieldType));
                }
                catch (Exception e)
                {
                    //Log.Notice("No such spec column : {0}, filname: {1}\nError : {2}", titleName, fileName, e.ToString());
                }
            }

            specList.Add(spec);
        }
        return specList;

    }

    Dictionary<string, int> SetTitleIndexMap(List<string> line)
    {
        Dictionary<string, int> titleIndexMap = new Dictionary<string, int>();

        for (int i = 0; i < line.Count; i++)
            if (line[i].Length > 0)
                titleIndexMap[line[i]] = i;

        return titleIndexMap;
    }
}