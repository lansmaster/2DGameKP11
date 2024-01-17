using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class SavePlayerPositionToDataBase : MonoBehaviour
{
    [SerializeField] private Transform _playerTransfom;

    private string dbName = "PlayerPositionDB";
    
    private IDbConnection _connection;

    private void Start()
    {
        _connection = new SqliteConnection(string.Format("FullUri=file:{0}.db?cache=shared", dbName));
    }

    public void SavePlayerPosition()
    {
        _connection.Open();

        PushCommand(string.Format("UPDATE Coordinates SET XAxis = {0}, YAxis = {1}, ZAxis = {2} WHERE Slot = 1", 
            _playerTransfom.position.x, _playerTransfom.position.y, _playerTransfom.position.z), 
            _connection);

        _connection.Close();
    }

    public void LoadPlayerPosition()
    {
        _connection.Open();

        IDataReader dataReader = ReadSavedData(); //

        while (dataReader.Read())
        {
            _playerTransfom.position = new Vector3(dataReader.GetFloat(1), dataReader.GetFloat(2), dataReader.GetFloat(3));
        }

        _connection.Close();
    }

    private void PushCommand(string commandString, IDbConnection connection)
    {
        IDbCommand command = connection.CreateCommand();

        command.CommandText = string.Format("{0}", commandString);

        command.ExecuteReader();
    }

    private IDataReader ReadSavedData()
    {
        IDbCommand commad = _connection.CreateCommand();

        commad.CommandText = "SELECT * FROM Coordinates WHERE Slot = 1";

        IDataReader dataReader = commad.ExecuteReader();
        return dataReader;
    }
}
