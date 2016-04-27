using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ITSR
{
    public partial class CreateArticle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("id"), new DataColumn("Author"), new DataColumn("Year"), new DataColumn("Title"), new DataColumn("URL") });
                ViewState["Customers"] = dt;
                this.BindGrid();
            }
        }

        protected void BindGrid()
        {
            gridViewReferences.DataSource = (DataTable)ViewState["Customers"];
            gridViewReferences.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if(Button2.Text == "Update")
            {
                DataTable dt = (DataTable)ViewState["Customers"];
                string id = Label1.Text;
                foreach(DataRow dr in dt.Rows)
                {
                    if(dr["id"].ToString() == id)
                    {
                        dr["Author"] = TextBox2.Text.Trim();
                        dr["Year"] = TextBox3.Text.Trim();
                        dr["Title"] = TextBox4.Text.Trim();
                        dr["URL"] = TextBox5.Text.Trim();
                    }
                    Button2.Text = "Button";
                }
                ViewState["Customers"] = dt;
                this.BindGrid();
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBox3.Text = string.Empty;
                TextBox4.Text = string.Empty;
                TextBox5.Text = string.Empty;
            }
            else
            {
                DataTable dt = (DataTable)ViewState["Customers"];
                dt.Rows.Add(TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox4.Text.Trim(), TextBox5.Text.Trim());
                ViewState["Customers"] = dt;
                this.BindGrid();
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBox3.Text = string.Empty;
                TextBox4.Text = string.Empty;
                TextBox5.Text = string.Empty;
            }


        }

        protected void gridViewReferences_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "EditRow")
            {
                //Label1.Text = "EDIT!";
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                Label1.Text = rowIndex.ToString();
                TextBox1.Text = gridViewReferences.Rows[rowIndex].Cells[0].Text;
                TextBox2.Text = gridViewReferences.Rows[rowIndex].Cells[1].Text;
                TextBox3.Text = gridViewReferences.Rows[rowIndex].Cells[2].Text;
                TextBox4.Text = gridViewReferences.Rows[rowIndex].Cells[3].Text;
                TextBox5.Text = gridViewReferences.Rows[rowIndex].Cells[4].Text;
                Button2.Text = "Update";
            }
        }
    }
}