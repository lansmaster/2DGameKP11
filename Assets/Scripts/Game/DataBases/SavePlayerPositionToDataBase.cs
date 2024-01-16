using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEditor.MemoryProfiler;

public class SavePlayerPositionToDataBase : MonoBehaviour
{
    [SerializeField] private Transform _playerTransfom;

    private string dbName = "PlayerPositionDB";
    
    private IDbConnection connection;

    private void Start()
    {
        connection = new SqliteConnection(string.Format("URI=file:Assets/StreamingAssets/DataBases/{0}.db", dbName));
    }

    void Update()
    {
        // When pressed Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Open database
            connection.Open();
            // Update Data in Save Slot
            PushCommand(string.Format("UPDATE Coordinates SET XAxis = {0}, YAxis = {1} , ZAxis = {2} WHERE Slot = 1;", _playerTransfom.position.x, _playerTransfom.position.y, _playerTransfom.position.z), connection);
        }

        // When pressed E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Open database
            connection.Open();

            // Read X , Y , Z Axis
            IDataReader dataReader = ReadSavedData();

            // Separate Float Data and assign to player position
            while (dataReader.Read())
            {
                // Assigning saved position
                _playerTransfom.position = new Vector3(dataReader.GetFloat(1), dataReader.GetFloat(2), dataReader.GetFloat(3));
            }
        }

        // Close database
        connection.Close();
    }

    private void PushCommand(string commandString, IDbConnection connection)
    {
        // Create new command
        IDbCommand command = connection.CreateCommand();
        // Add your comment text (queries)
        command.CommandText = string.Format("{0}", commandString);
        // Execute command reader - execute command
        command.ExecuteReader();
    }

    // Read last position from coordinates table
    private IDataReader ReadSavedData()
    {
        // Create command (query)
        IDbCommand command = connection.CreateCommand();
        // Get all data in Slot = 1 from coordinates table
        command.CommandText = "SELECT * FROM Coordinates WHERE Slot = 1;";
        // Execute command
        IDataReader dataReader = command.ExecuteReader();
        return dataReader;
    }
}
