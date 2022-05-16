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
using WF1_Exercicio.Service;

namespace WF1_Exercicio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnInserir.Enabled = false;
            btnAtualizar.Enabled = false;
            btnDeletar.Enabled = false;
            txtId.Enabled = false;

            SqlConnection objCon = ConnectionCreator.CreateConnection();
            objCon.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM clientes", objCon);
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter();

                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = ds.Tables[0].TableName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar BD.");
            }
            finally
            {
                objCon.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAtualizar.Enabled = true;
            btnDeletar.Enabled = true;

            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtIdade.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            SqlConnection objCon = ConnectionCreator.CreateConnection();
            string sql = @"UPDATE clientes SET nome = '" + txtNome.Text + "', idade = '" + int.Parse(txtIdade.Text) + "', email = '" + txtEmail.Text + "' WHERE cliente_id = '" + txtId.Text + "';";

            SqlCommand cmd = new SqlCommand(sql, objCon);

            cmd.CommandType = CommandType.Text;
            objCon.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Cliente atualizado com sucesso.");

                Form1_Load(sender, e);
                ClearTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar clinete.\n" + ex.Message);
            }
            finally
            {
                objCon.Close();
            }
        }
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            SqlConnection objCon = ConnectionCreator.CreateConnection();
            string sql = @"DELETE FROM clientes WHERE cliente_id = '" + txtId.Text + "';";

            SqlCommand cmd = new SqlCommand(sql, objCon);

            cmd.CommandType = CommandType.Text;
            objCon.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Cliente deletado com sucesso.");

                Form1_Load(sender, e);
                ClearTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar cliente.\n" + ex.Message);
            }
            finally
            {
                objCon.Close();
            }
        }
        private void btnInserir_Click(object sender, EventArgs e)
        {
            SqlConnection objCon = ConnectionCreator.CreateConnection();
            string sql = @"INSERT INTO clientes (nome, idade, email) VALUES ('" + txtNome.Text + "', '" + txtIdade.Text + "', '" + txtEmail.Text + "');";

            SqlCommand cmd = new SqlCommand(sql, objCon);

            cmd.CommandType = CommandType.Text;
            objCon.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Cliente adicionado com sucesso.");

                Form1_Load(sender, e);
                ClearTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir cliente.\n" + ex.Message);
            }
            finally
            {
                objCon.Close();
            }
        }
        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtId.Text.Length == 0 && txtNome.Text.Length > 1)
                btnInserir.Enabled = true;
            else
                btnInserir.Enabled = false;
        }

        internal void ClearTxt()
        {            
            txtId.Text = "";
            txtNome.Text = "";
            txtNome.Focus();
            txtIdade.Text = "";
            txtEmail.Text = "";
        }

    }
}
