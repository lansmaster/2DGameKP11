using System.Data;
using System.Globalization;
using UnityEngine;

public class SavePlayerPosition : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;

    public void Save()
    {
        string XAxisValue = _playerTransform.position.x.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
        string YAxisValue = _playerTransform.position.y.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

        DataBase.ExecuteQueryWithoutAnswer(string.Format("UPDATE PlayerPosition SET XAxis = {0}, YAxis = {1} WHERE id = 1", XAxisValue, YAxisValue));
    }

    public void Load()
    {
        DataTable PlayerPosition = DataBase.GetTable("SELECT * FROM PlayerPosition WHERE id = 1");

        float XAxisValue = float.Parse(PlayerPosition.Rows[0][1].ToString());
        float YAxisValue = float.Parse(PlayerPosition.Rows[0][2].ToString());

        Debug.Log(XAxisValue + " " + YAxisValue);
        _playerTransform.position = new Vector3(XAxisValue, YAxisValue, 0);
    }
}