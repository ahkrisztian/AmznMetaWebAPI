namespace AmznMetaLibrary.Repo.Data
{
    public interface ISqliteDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlstatement, U parameters, string connectionStringName);
        Task SaveData<T>(string sqlstatement, T parameters, string connectionStringName);
    }
}