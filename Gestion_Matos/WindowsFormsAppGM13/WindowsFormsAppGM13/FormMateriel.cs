using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppGM13
{
    public partial class FormMateriel : Form
    {

        private string connstring = @"Server=.\SQLEXPRESS;Database=GM;Trusted_Connection=True;";

        public FormMateriel()
        {
            InitializeComponent();
        }

        private void FormMateriel_Load(object sender, EventArgs e)
        {
            affcMatos();
        }

        private void affcMatos()
        {
            listBoxMatériel.Items.Clear();

            SqlConnection cn = null;
            SqlCommand com = null;
            SqlDataReader sqr = null;

            try
            {
                cn = new SqlConnection(this.connstring);
                cn.Open();

                string strsql = "select Nom from Materiel";
                com = new SqlCommand(strsql, cn);
                sqr = com.ExecuteReader();

                string str;
                while (sqr.Read() == true)
                {
                    str = sqr["Nom"].ToString();
                    listBoxMatériel.Items.Add(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème avec votre base de données, contactez votre admin !");
                Application.Exit();
            }

            if (cn != null)
            {
                cn.Close();
                cn.Dispose();
                if (com != null)
                {
                    com.Dispose();
                    if (sqr != null)
                    {
                        sqr.Close();
                    }
                }
            }
        }

        

        private void listBoxMatériel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMatériel.SelectedItem == null)
            {
                return;
            }

            string nom = listBoxMatériel.SelectedItem.ToString();

            SqlConnection cn = null;
            SqlCommand com = null;
            SqlDataReader sqr = null;

            cn = new SqlConnection(this.connstring);
            cn.Open();

            string strsql = "select * from Materiel where Nom = '" + nom + "'";
            com = new SqlCommand(strsql, cn);
            sqr = com.ExecuteReader();
            sqr.Read();

            textBoxNom.Text = sqr["Nom"].ToString();
            textBoxService.Text = sqr["NoSerie"].ToString();
            textBoxDate.Text = sqr["Date_Install"].ToString();
            textBoxMTBF.Text = sqr["MTBF"].ToString();
            textBoxType.Text = sqr["Type"].ToString();
            textBoxMarque.Text = sqr["Marque"].ToString();

            //fixer le Client


            if (cn != null)
            {
                cn.Close();
                cn.Dispose();
                if (com != null)
                {
                    com.Dispose();
                    if (sqr != null)
                    {
                        sqr.Close();
                    }
                }
            }
        }


    }
}
