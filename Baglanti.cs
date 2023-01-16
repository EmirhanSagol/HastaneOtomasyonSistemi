using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GörselProgramlamaFinal
{
    public class Baglanti
    {
        public OleDbConnection baglan()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\emirhansagol\\source\\repos\\GörselProgramlamaFinal\\Database1.accdb");
            con.Open();
            return con;
        }
    }
}
