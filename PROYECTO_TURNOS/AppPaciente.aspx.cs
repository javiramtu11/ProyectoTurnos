﻿using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace PG_CitasMedicas
{
    public partial class AppPaciente : System.Web.UI.Page
    {
        string CadenaConexion = "Data Source = DESKTOP-RTIU5G0; Initial Catalog = HospitalAdonai; Integrated Security = True";

        public void AddPaciente() {
            SqlConnection conexionSQL = new SqlConnection(CadenaConexion);
            SqlCommand cmd = new SqlCommand();

            string DPI = dpi.Value;
            string name = nombre.Value;
            string lastname = apellidox.Value;
            string Sexo = genero.Value;
            string dire = direccionx.Value;
            string fechaN = fechanac.Value;
//COMENTARIO OFENSIVO
            cmd.CommandText = "INSERT INTO PACIENTES (DPI, NOMBRE, APELLIDO, GENERO, DIRECCION, FECHA_NAC, FECHA_INGRESO, ESTADO)" +
                " VALUES (@DPI, @NOMBRE, @APELLIDO, @GENERO, @DIRECCION, @FECHA_NAC, GETDATE(), 1)";
            cmd.Parameters.Add("@DPI", SqlDbType.Text).Value = DPI;
            cmd.Parameters.Add("@NOMBRE", SqlDbType.Text).Value = name;
            cmd.Parameters.Add("@APELLIDO", SqlDbType.Text).Value = lastname;
            cmd.Parameters.Add("@GENERO", SqlDbType.Text).Value = Convert.ToString (Sexo);
            cmd.Parameters.Add("@DIRECCION", SqlDbType.Text).Value = dire;
            cmd.Parameters.Add("@FECHA_NAC", SqlDbType.Date).Value = Convert.ToDateTime(fechaN);

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexionSQL;
            conexionSQL.Open();
            cmd.ExecuteNonQuery();

        }

        public void obtenerPaciente() {
            SqlConnection conexionSQL = new SqlConnection(CadenaConexion);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT ID_PACIENTE, DPI, NOMBRE, GENERO, DIRECCION, FECHA_NAC  FROM PACIENTES WHERE ESTADO = 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexionSQL;
            conexionSQL.Open();

            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            Grid.DataSource = Datos;
            Grid.DataBind();
            conexionSQL.Close();
        }

        public void buscarPaciente() {

           
            SqlConnection conexionSQL = new SqlConnection(CadenaConexion);
            SqlCommand cmd = new SqlCommand();

            string buscar = TxtBuscar.Text;
            cmd.CommandText = "SELECT ID_PACIENTE, DPI, NOMBRE, GENERO, DIRECCION, FECHA_NAC  FROM PACIENTES WHERE DPI LIKE '%"+buscar+"%' OR NOMBRE = '"+buscar+"' AND ESTADO = 1 ";
            //cmd.Parameters.Add("@buscar", SqlDbType.Text).Value = buscar;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexionSQL;
            conexionSQL.Open();

            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            Grid.DataSource = Datos;
            Grid.DataBind();
            conexionSQL.Close();
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            obtenerPaciente();
        }

        protected void InsertarPaciente_Click(object sender, EventArgs e)
        {
            AddPaciente();
        }

        protected void Lbtn_Buscar_Click(object sender, EventArgs e)
        {
            buscarPaciente();
        }
    }
}
