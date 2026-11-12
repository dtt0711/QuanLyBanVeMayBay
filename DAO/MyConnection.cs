using MySql.Data.MySqlClient;

namespace BanVeMayBay.DAO;

public static class MyConnection
{
    private const string ConnectionString =
        "Server=localhost;Port=3306;Database=BanVeMayBay;User ID=root;Password=;SslMode=None;";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
}
