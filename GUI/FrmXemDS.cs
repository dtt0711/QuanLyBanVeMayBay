using System.Collections;
using System.Reflection;

namespace BanVeMayBay.GUI;

public partial class FrmXemDS : Form
{
    public FrmXemDS()
    {
        InitializeComponent();
    }

    public FrmXemDS(string tieuDe, IList ds) : this()
    {
        Text = tieuDe;
        if (ds == null)
            return;

        Type kieu = ds.GetType().GetGenericArguments()[0];
        PropertyInfo[] cot = kieu.GetProperties();
        foreach (PropertyInfo p in cot)
            lvDS.Columns.Add(p.Name, 120);

        foreach (object item in ds)
        {
            int n = lvDS.Items.Count;
            for (int i = 0; i < cot.Length; i++)
            {
                object v = cot[i].GetValue(item);
                string text = v is DateTime d ? d.ToString("dd/MM/yyyy") : v is decimal m ? m.ToString("N0") : v?.ToString() ?? "";
                if (i == 0) lvDS.Items.Add(text);
                else lvDS.Items[n].SubItems.Add(text);
            }
        }
    }
}
