﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace PROYECTO_TURNOS
{
    public partial class ConfigClinica : System.Web.UI.Page
    {
        string CadenaConexion = "Data Source = DESKTOP-RTIU5G0; Initial Catalog = HospitalAdonai; Integrated Security = True";

        public void AddCline() {

            SqlConnection conexionSQL = new SqlConnection(CadenaConexion);
            SqlCommand cmd = new SqlCommand();

            string cline = clinica.Value;
            string desc = Descripcion.Value;

            cmd.CommandText = "INSERT INTO CLINICAS (CLINICA, DESCRIPCION, ESTADO)" +
                " VALUES (@CLINICA, @DESCRIPCION, 1)";
            cmd.Parameters.Add("@CLINICA", SqlDbType.Text).Value = cline;
            cmd.Parameters.Add("@DESCRIPCION", SqlDbType.Text).Value = desc;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexionSQL;
            conexionSQL.Open();
            cmd.ExecuteNonQuery();
            
        }

        public void obtenerClinica() {
            SqlConnection conexionSQL = new SqlConnection(CadenaConexion);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT ID_CLINICA, CLINICA, DESCRIPCION FROM CLINICAS WHERE ESTADO = 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexionSQL;
            conexionSQL.Open();

            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            Grid.DataSource = Datos;
            Grid.DataBind();
            conexionSQL.Close();
        }

        public void buscarClinica() {
            SqlConnection conexionSQL = new SqlConnection(CadenaConexion);
            SqlCommand cmd = new SqlCommand();

            string buscar = txtbuscar.Value;
            cmd.CommandText = "SELECT ID_CLINICA, CLINICA, DESCRIPCION FROM CLINICAS WHERE CLINICA LIKE '%" + buscar + "%' AND ESTADO = 1 ";

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
            obtenerClinica();
        }

        protected void InsertarClinica_Click(object sender, EventArgs e)
        {
            AddCline();
        }

        protected void BtnBuscarClinica_Click(object sender, EventArgs e)
        {
            buscarClinica();
        }
    }
}