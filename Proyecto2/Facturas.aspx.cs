using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


public partial class Facturas : System.Web.UI.Page
{
    private static string cs;
    private static MySqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {     
        Informacion.DataSource = recolectarInformacion();
        Informacion.DataBind();
    }

    protected DataTable recolectarInformacion()
    {
        iniciarConexion();
        MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Facturas", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    protected void iniciarConexion()
    {
        cs = ConfigurationManager.ConnectionStrings["CONEXION"].ConnectionString;
        con = new MySqlConnection(cs);
    }
}