using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public static class DataBase
{
    private const string _fileName = "DataBase.bytes";
    private static string _filePath;
    private static SqliteConnection _connection;
    private static SqliteCommand _command;

    static DataBase()
    {
        _filePath = GetDataBasePath();
    }

    private static string GetDataBasePath()
    {
        #if UNITY_EDITOR
            return Path.Combine(Application.streamingAssetsPath, _fileName);
        #endif

        #if UNITY_STANDALONE
            string filePath = Path.Combine(Application.dataPath, _fileName);
            if (!File.Exists(filePath)) UnpackDatabase(filePath);
            return filePath;
        #elif UNITY_ANDROID
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if(!File.Exists(filePath)) UnpackDatabase(filePath);
            return filePath;
        #endif
    }

    private static void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, _fileName);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);
    }

    private static void OpenConnection()
    {
        _connection = new SqliteConnection("Data Source=" + _filePath);
        _command = new SqliteCommand(_connection);
        _connection.Open();
    }

    public static void CloseConnection()
    {
        _connection.Close();
        _command.Dispose();
    }

    public static void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        _command.CommandText = query;
        _command.ExecuteNonQuery();
        CloseConnection();
    }

    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        _command.CommandText = query;
        var ansver = _command.ExecuteScalar();
        CloseConnection();

        if (ansver != null)
            return ansver.ToString();
        else 
            return null;
    }

    public static DataTable GetTable(string query)
    {
        OpenConnection();

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, _connection);

        DataSet dataSet = new DataSet();
        adapter.Fill(dataSet);
        adapter.Dispose();

        CloseConnection();

        return dataSet.Tables[0];
    }
}