using System;
using System.Windows.Forms;
using QuanLyBanVeMayBay.GUI;

namespace QuanLyBanVeMayBay
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Đổi thành frmSanBay hoặc frmHangHangKhong nếu muốn mở form khác
            Application.Run(new FrmMain());
        }
    }
}
