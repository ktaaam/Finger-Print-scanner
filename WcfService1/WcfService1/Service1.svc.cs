using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        static string myConnection = "DSN=MS Access Database";
        OdbcConnection myConn = new OdbcConnection(myConnection);
        OdbcCommand mycommand;
        OdbcTransaction transaction;
        OdbcDataAdapter adapter;

      

    //public string doPost(string value)
    //{
    //    // Trace.WriteLine("hello", value);
    //    byte[] b = Convert.FromBase64String(value);
    //    OdbcConnection myConn = new OdbcConnection(myConnection);
    //    string query = "INSERT INTO fingerPrintDb (fingerPrint) VALUES ('" + value + "')";
    //    OdbcCommand mycommand = new OdbcCommand(query, myConn);

    //    myConn.Open();
    //    transaction = myConn.BeginTransaction();

    //    mycommand.Connection = myConn;
    //    mycommand.Transaction = transaction;
    //    mycommand.CommandText = query;

    //    mycommand.ExecuteNonQuery();
    //    transaction.Commit();
    //    myConn.Close();

    //    return "Success";
    //}

    //public string doPost(string value)
    //{
    //    Trace.WriteLine("hello", value);
    //    byte[] b = Convert.FromBase64String(value);
    //    OdbcConnection myConn = new OdbcConnection(myConnection);
    //    string query = "INSERT INTO fingerPrintDb (fingerPrint) VALUES ('" + value +"')";
    //    OdbcCommand mycommand = new OdbcCommand(query, myConn);

    //    myConn.Open();
    //    transaction = myConn.BeginTransaction();

    //    mycommand.Connection = myConn;
    //    mycommand.Transaction = transaction;
    //    mycommand.CommandText = query;

    //    mycommand.ExecuteNonQuery();
    //    transaction.Commit();
    //    myConn.Close();

    //    return "Success";
    //}

    public string GetData()
        {
            OdbcConnection myConn = new OdbcConnection(myConnection);
            //string query = "INSERT INTO fingerPrintDb (fingerPrint) VALUES (" + value +")";s
            string query = "Select * FROM fingerPrintDb";
     
            transaction = null;
            

            myConn.Open();
            OdbcCommand command = new OdbcCommand(query, myConn);
            OdbcDataAdapter adapt = new OdbcDataAdapter(command);

            DataTable dt = new DataTable();
            adapt.Fill(dt);
          
            
            myConn.Close();
            string json = JsonConvert.SerializeObject(dt);
            return json;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
