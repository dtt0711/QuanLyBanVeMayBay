using System;
using System.Windows.Forms;
using BanVeMayBay.GUI;

namespace BanVeMayBay
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
