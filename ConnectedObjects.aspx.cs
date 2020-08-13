using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebDataAccessConnected
{
    public partial class ConnectedObjects : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0K0H855;Initial Catalog=DontNetTraining;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        DataTable dt;
        public void ShowGrid()
        {
            conn.Open();
             cmd = new SqlCommand("select * from EmployeeTbl", conn);
             dr = cmd.ExecuteReader();
             dt = new DataTable();//empty table
            dt.Load(dr);//method of DataTable
            GridView1.DataSource = dt;
            GridView1.DataBind();
            dr.Close();
            conn.Close();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowGrid();
            }
           
            //while (dr.Read())//Read() is a method of SqlDataReader    
            //{
            //    GridView1.DataSource = dr;
            //    DropDownList1.DataSource = dr[1];
            //    GridView1.DataBind();
              
            //    DropDownList1.DataBind();
            //}
            
        }

        protected void btnInsertEmp_Click(object sender, EventArgs e)//insert using single and double inverted comma
        {
            conn.Open();
            cmd=new SqlCommand("insert into EmployeeTbl(empName,empSal) values('"+txtEmpName.Text+"',"+txtEmpSalary.Text+")",conn);
            int x= cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }
        protected void Button1_Click(object sender, EventArgs e)//insert using parameters 
        {
            conn.Open();
            cmd = new SqlCommand("insert into EmployeeTbl(empName,empSal) values(@empname,@empsal)", conn);
            cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = txtEmpName.Text;
            cmd.Parameters.Add("@empsal", SqlDbType.Float).Value = Convert.ToSingle(txtEmpSalary.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }
        protected void btnInsertSp_Click(object sender, EventArgs e)//insert using Storage Procedures
        {
            conn.Open();
            cmd = new SqlCommand("sp_Insert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = txtEmpName.Text;
            cmd.Parameters.Add("@empsal", SqlDbType.Float).Value = Convert.ToSingle(txtEmpSalary.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnUpdatePara_Click(object sender, EventArgs e)//update using Single and double inverted comma
        {
            conn.Open();
            cmd = new SqlCommand("update EmployeeTbl set empName='"+txtEmpName.Text+"',empSal="+txtEmpSalary.Text+" where empId="+txtEmpId.Text+"", conn);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
            
            
        }
        protected void btnUpdateParameter_Click(object sender, EventArgs e)//Update using Parameters
        {
            conn.Open();
            cmd = new SqlCommand("update EmployeeTbl set EmpName=@empname , empSal=@empsal where empId=@empid", conn);
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(txtEmpId.Text);
            cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = txtEmpName.Text;
            cmd.Parameters.Add("@empsal", SqlDbType.Float).Value = Convert.ToSingle(txtEmpSalary.Text);
            if (cmd.ExecuteNonQuery() > 0)
            {
                Response.Write("alert(one row updated)");
            }
            conn.Close();
            ShowGrid();
        }
        protected void btnUpdateSp_Click(object sender, EventArgs e)//update using Storage Procedures
        {
            conn.Open();
            cmd = new SqlCommand("sp_Update", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(txtEmpId.Text);
            cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = txtEmpName.Text;
            cmd.Parameters.Add("@empsal", SqlDbType.Float).Value = Convert.ToSingle(txtEmpSalary.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnDeleteSp_Click(object sender, EventArgs e)//Delete using Storage Parameters
        {

            //CommandType is a enum
            conn.Open();
            cmd = new SqlCommand("sp_DeleteEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(txtEmpId.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }
        protected void btnDelete_Click(object sender, EventArgs e)//Delete using single Double inverted Commas
        {
            conn.Open();
            cmd = new SqlCommand("delete from EmployeeTbl where empId=" + txtEmpId.Text + " ", conn);
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }

        protected void btnDeleteWithPara_Click(object sender, EventArgs e)//Delete using Parameters
        {
            conn.Open();
            cmd = new SqlCommand("delete from EmployeeTbl where empId=@empid", conn);
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(txtEmpId.Text);

            cmd.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }


        protected void btnSearchEmp_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("sp_SearchEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = Convert.ToInt32(txtEmpId.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtEmpName.Text = dr[0].ToString();
                txtEmpSalary.Text = dr["empSal"].ToString();


            }
            conn.Close();
        }

    }
}