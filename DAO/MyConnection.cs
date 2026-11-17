using System.Data;
using MySql.Data.MySqlClient;

namespace QuanLyBanVeMayBay.DAO;

public static class MyConnection
{
    private const string ConnectionString =
        "Server=localhost;Port=3306;Database=banvemaybay;User ID=root;Password=;SslMode=None;";

    // Dùng chung 1 kết nối cho cả chương trình. Trước đây mỗi DAO mở 1 kết nối mới
    // mà không đóng, mở nhiều form sẽ vượt giới hạn 151 kết nối của MySQL.
    private static MySqlConnection connection;

    public static MySqlConnection GetConnection()
    {
        if (connection == null)
            connection = new MySqlConnection(ConnectionString);
        return connection;
    }

    public static void Mo(MySqlConnection conn)
    {
        if (conn.State != ConnectionState.Open)
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Open();
        }
    }
}
