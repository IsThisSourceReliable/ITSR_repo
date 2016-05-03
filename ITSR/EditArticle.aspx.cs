using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using ITSR.CLASSES.ARTICLE;
using ITSR.CLASSES.USER;

namespace ITSR
{
    public partial class EditArticle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("id"), new DataColumn("Author"), new DataColumn("Year"), new DataColumn("Title"), new DataColumn("URL") });
                ViewState["References"] = dt;
                this.BindGrid();
                BindDropDown();
            }
        }

        /// <summary>
        /// Binds dropdown with types of organisations
        /// </summary>
        private void BindDropDown()
        {
            Articles Orgs = new Articles();
            DataTable TypeOfOrgs = Orgs.GetTypeOfOrgs();

            dropDownTypeOfOrg.DataSource = TypeOfOrgs;
            dropDownTypeOfOrg.DataTextField = "name";
            dropDownTypeOfOrg.DataValueField = "idtypeoforg";
            dropDownTypeOfOrg.DataBind();
            this.dropDownTypeOfOrg.Items.Insert(0, "Choose");
        }

        /// <summary>
        /// Binds the grid with references.
        /// Usees viewstate in order to not have to use any type of datasource.
        /// </summary>
        protected void BindGrid()
        {
            gridViewReferences.DataSource = (DataTable)ViewState["References"];
            gridViewReferences.DataBind();

            if (gridViewReferences.Rows.Count == 0)
            {
                lblRef.Text = "Add some references to this source";
            }
            else
            {
                lblRef.Text = "";
            }
        }

        /// <summary>
        /// Clears all the textboxes.
        /// </summary>
        private void ClearReferenceTxtBoxes()
        {
            txtAuthor.Text = string.Empty;
            txtYear.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtURL.Text = string.Empty;
        }

        protected void btnAddRef_Click(object sender, EventArgs e)
        {

            if (btnAddRef.Text == "Update")
            {
                DataTable dt = (DataTable)ViewState["References"];
                string id = lblID.Text;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["id"].ToString() == id)
                    {
                        dr["Author"] = txtAuthor.Text.Trim();
                        dr["Year"] = txtYear.Text.Trim();
                        dr["Title"] = txtTitle.Text.Trim();
                        dr["URL"] = txtURL.Text.Trim();
                    }
                    btnAddRef.Text = "Add Reference";
                }
                ViewState["References"] = dt;
                this.BindGrid();
                ClearReferenceTxtBoxes();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["References"];
                int rowCount = dt.Rows.Count;
                int newIndex = 0;

                if (rowCount <= 0)
                {
                    newIndex = 0;
                }
                else
                {
                    int fakeIndex = rowCount - 1;
                    string highestID = gridViewReferences.Rows[fakeIndex].Cells[0].Text;
                    newIndex = Convert.ToInt32(highestID) + 1;
                }

                dt.Rows.Add(newIndex.ToString(), txtAuthor.Text.Trim(), txtYear.Text.Trim(), txtTitle.Text.Trim(), txtURL.Text.Trim());
                ViewState["References"] = dt;
                this.BindGrid();
                ClearReferenceTxtBoxes();
            }
        }

        protected void gridViewReferences_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                //Label1.Text = "EDIT!";
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                lblID.Text = rowIndex.ToString();
                txtAuthor.Text = gridViewReferences.Rows[rowIndex].Cells[1].Text;
                txtYear.Text = gridViewReferences.Rows[rowIndex].Cells[2].Text;
                txtTitle.Text = gridViewReferences.Rows[rowIndex].Cells[3].Text;
                txtURL.Text = gridViewReferences.Rows[rowIndex].Cells[4].Text;
                btnAddRef.Text = "Update";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OpenOverlay", "OpenOverlay();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DataTable dt = (DataTable)ViewState["References"];
                //int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                string id = e.CommandArgument.ToString();
                //string id = gridViewReferences.Rows[rowIndex].Cells[0].Text;
                //Label1.Text = rowIndex.ToString();
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["id"].ToString() == id)
                        dr.Delete();
                }
                ViewState["References"] = dt;
                this.BindGrid();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}