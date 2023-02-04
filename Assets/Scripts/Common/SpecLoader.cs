using System.Collections.Generic;
using UnityEngine;

public interface ISpecLoader<T> where T : new()
{
    List<T> GetAllSpecList();
}

public class SpecLoader<T> : ISpecLoader<T> where T : new()
{
    private List<T> list = new List<T>();

    public SpecLoader(string specPath)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(specPath);
        CSVReader<T> CSVReader = new CSVReader<T>(specPath, textAsset.bytes);
        list.AddRange(CSVReader.ReadCSV());
        Resources.UnloadAsset(textAsset);
    }

    public SpecLoader(string specPathForLog, string textInSpec)
    {
        CSVReader<T> CSVReader = new CSVReader<T>(specPathForLog, System.Text.Encoding.UTF8.GetBytes(textInSpec));
        list.AddRange(CSVReader.ReadCSV());
    }

    public SpecLoader(string specPathForLog, byte[] bytes)
    {
        CSVReader<T> CSVReader = new CSVReader<T>(specPathForLog, bytes);
        list.AddRange(CSVReader.ReadCSV());
    }

    public List<T> GetAllSpecList()
    {
        return new List<T>(list);
    }
}

public class Spec
{
    public string UniqueName;
    public string KO;
    public string EN;
}