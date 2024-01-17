using System.Data;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerPosition : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;

    public void Save()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        string XAxisCurrentValue = _playerTransform.position.x.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
        string YAxisCurrentValue = _playerTransform.position.y.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

        DataBase.ExecuteQueryWithoutAnswer(string.Format("UPDATE PlayerPosition SET XAxis = {0}, YAxis = {1}, SceneIndex = {2} WHERE id = 1", XAxisCurrentValue, YAxisCurrentValue, CurrentSceneIndex));
    }

    public void Load()
    {
        DataTable PlayerPosition = DataBase.GetTable("SELECT * FROM PlayerPosition WHERE id = 1");

        float XAxisSavedValue = float.Parse(PlayerPosition.Rows[0][1].ToString());
        float YAxisSavedValue = float.Parse(PlayerPosition.Rows[0][2].ToString());
        int SavedSceneIndex = int.Parse(PlayerPosition.Rows[0][3].ToString());

        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SavedSceneIndex != CurrentSceneIndex)
            SceneManager.LoadScene(SavedSceneIndex);

        _playerTransform.position = new Vector3(XAxisSavedValue, YAxisSavedValue, 0);
    }
}