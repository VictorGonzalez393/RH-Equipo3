﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgroNegocio_RH_ERP_ISC_8A.Datos;
using AgroNegocio_RH_ERP_ISC_8A.Modelo;

namespace AgroNegocio_RH_ERP_ISC_8A.Interfaces
{
    public partial class Empleados_nuevo : Form
    {
        Empleados_DAO empleadodao = new Empleados_DAO();
        Ciudades_DAO ciudadesdao = new Ciudades_DAO();
        public Empleados_nuevo()
        {
            InitializeComponent();
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Empleados_nuevo_Load(object sender, EventArgs e)
        {
            string consulta_where = " where estatus = @estatus";
            List<string> parametros = new List<string>();
            parametros.Add("@estatus");
            List<object> valores = new List<object>();
            valores.Add('A');

            List<Ciudad> ciudades = ciudadesdao.consultaGeneral(consulta_where, parametros, valores);

            foreach (Ciudad ciudad in ciudades)
            {
                ciudad_empleado.Items.Add(ciudad);
            }

        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (validarDatos() == true)
            {
                Ciudad ciudad = (Ciudad)ciudad_empleado.SelectedItem;
                Departamento departamento = (Departamento)departamento_empleado.SelectedItem;
                Puesto puesto = (Puesto)puesto_empleado.SelectedItem;
                Empleado empleado_nuevo = new Empleado(0, nombre_empleado.Text, apaterno_empleado.Text, amaterno_empleado.Text, sexo_empleado.Text
                    , fcontratacion_empleado.Text, fnacimiento_empleado.Text, 0, nss_empleado.Text, estadocivil_empleado.Text, 0, 0, direccion_empleado.Text
                    , colonia_empleado.Text, 0, escolaridad_empleado.Text, 0, 'A', departamento.idDepto, puesto.IdPuesto, ciudad.ID, 0);
                try
                {
                    if (empleadodao.validarEmpleado(empleado_nuevo))
                    {
                        if (empleadodao.registrar(empleado_nuevo))
                        {
                            MessageBox.Show("Registro exitoso");
                            Close();
                        }
                        else
                            MessageBox.Show("Error al registrar");
                    }
                    else
                        MessageBox.Show("Error al registrar. El empleado ya existe");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar al empleado");
                }
            }


        }

        private bool validarDatos()
        {
            if (!string.IsNullOrWhiteSpace(nombre_empleado.Text))
            {
                if (ciudad_empleado.SelectedIndex != -1)
                    return true;
                else
                {
                    MessageBox.Show("Falta la ciudad");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Falta el nombre");
                return false;
            }

        }



        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            Principal p = new Principal();
            p.ShowDialog();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}