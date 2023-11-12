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
    public static DataTable tabla;

    protected void Page_Load(object sender, EventArgs e)
    {     
        Informacion.DataSource = recolectarInformacion();
        Informacion.DataBind();
    }
     
    protected DataTable recolectarInformacion()
    {
        iniciarConexion();
        MySqlCommand datos = new MySqlCommand("Select * from Facturas", con);
        tabla = new DataTable();
        tabla.Columns.Add("numFactura");
        tabla.Columns.Add("fechaFactura");
        tabla.Columns.Add("nombre");
        tabla.Columns.Add("apellidos");
        tabla.Columns.Add("CIFCliente");
        tabla.Columns.Add("importe");
        tabla.Columns.Add("importeIVA");
        tabla.Columns.Add("moneda");
        tabla.Columns.Add("estado");
        tabla.Columns.Add("fechaCobro");

        con.Open();
        MySqlDataReader lector = datos.ExecuteReader();
        while(lector.Read())
        {
            DataRow fila = tabla.NewRow();
            fila["numFactura"] = lector["numFactura"];
            fila["fechaFactura"] = ((DateTime)lector["fechaFactura"]).ToShortDateString();
            fila["nombre"] = lector["nombre"];
            fila["apellidos"] = lector["apellidos"];
            fila["CIFCliente"] = lector["CIFCliente"];
            fila["importe"] = formatearNumero(Convert.ToDouble(lector["importe"]));
            fila["importeIVA"] = formatearNumero(Convert.ToDouble(lector["importe"]) * 1.21);
            fila["moneda"] = lector["moneda"];
            fila["estado"] = lector["estado"];
            fila["fechaCobro"] = ((DateTime)lector["fechaCobro"]).ToShortDateString();

            tabla.Rows.Add(fila);
        }
        con.Close();
        return tabla;
    }

    protected String formatearNumero(double numero)
    {
        return numero.ToString("N2", new System.Globalization.CultureInfo("es-ES"));
    }

    protected void iniciarConexion()
    {
        cs = ConfigurationManager.ConnectionStrings["CONEXION"].ConnectionString;
        con = new MySqlConnection(cs);
    }



    protected void FuncionFiltro(object sender, EventArgs e)
    {
        string nombre = NombreCliente.Text.ToUpper();
        
    }

    protected void Informacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
{
    Informacion.PageIndex = e.NewPageIndex;
    // Vuelve a cargar los datos en el GridView
    Informacion.DataSource = tabla;
}

    

}