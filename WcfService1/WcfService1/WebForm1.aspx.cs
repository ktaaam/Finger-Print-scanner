using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WcfService1
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        static string myConnection = "DSN=MS Access Database";
        OdbcConnection myConn = new OdbcConnection(myConnection);
        OdbcCommand mycommand;
        OdbcTransaction transaction;
        OdbcDataAdapter adapter;
        protected void Page_Load(object sender, EventArgs e)
        {


            Stream s = Request.InputStream;
            StreamReader stream = new StreamReader(s);
            string x2 = "";
            string x = stream.ReadToEnd();
            if ( x.Length > 0 )
            {
                int position =  x.IndexOf("Value");
                int pos2 = x.IndexOf("}");
                x2 = x.Substring(position + 3 + 5, x.Length - 2 - position  - 3 - 5);
                byte[] b = Convert.FromBase64String(x2);
                OdbcConnection myConn = new OdbcConnection(myConnection);
                string query = "INSERT INTO fingerPrintDb (fingerPrint) VALUES (?)";
                OdbcCommand mycommand = new OdbcCommand(query, myConn);

                mycommand.Parameters.Add("fingerPrint", OdbcType.VarBinary).Value = b;

                myConn.Open();
                transaction = myConn.BeginTransaction();

                mycommand.Connection = myConn;
                mycommand.Transaction = transaction;
                mycommand.CommandText = query;

                mycommand.ExecuteNonQuery();
                transaction.Commit();
                myConn.Close();
            }

           



    }


    }
}