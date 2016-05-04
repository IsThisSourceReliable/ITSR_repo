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
                //CreateFakeDataTable();
                BindDropDown();
                LoadArticle();
            }
        }

        private void CreateFakeDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("id"), new DataColumn("Author"), new DataColumn("Year"), new DataColumn("Title"), new DataColumn("URL") });
            ViewState["References"] = dt;
            this.BindGrid();
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

        private void LoadArticle()
        {
            Articles article = new Articles();
            article.ID = 121;
            DataTable dt = article.GetArticle();

            lblRef.Text = dt.Rows.Count.ToString();

            txtArticleTitle.Text = dt.Rows[0]["title"].ToString();
            txtInfo.Text = dt.Rows[0]["text"].ToString().Replace("<br />", "\r\n");
            txtArticleURL.Text = dt.Rows[0]["url"].ToString();
            dropDownTypeOfOrg.SelectedValue = dt.Rows[0]["orgtype_id"].ToString();
            txtUpHouseMan.Text = dt.Rows[0]["publisher"].ToString();
            txtDomainOwner.Text = dt.Rows[0]["domainowner"].ToString();
            txtFinancer.Text = dt.Rows[0]["financing"].ToString();

            string xmlReferences = dt.Rows[0]["reference_xml"].ToString();

            BindGridDataSource(xmlReferences);
        }

        private void BindGridDataSource(string xml)
        {
            if (xml == string.Empty)
            {
                lblRef.Text = "This source article doesn't have any references";
            }
            else
            {
                lblRef.Text = "";
                DataTable dt = DaStuff(xml);
                ViewState["References"] = dt;
                gridViewReferences.DataSource = dt;
                gridViewReferences.DataBind();
            }
        }

        private DataTable DaStuff(string xml)
        {
            StringReader theReader = new StringReader(xml);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet.Tables[0];
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
                gridViewReferences.DataSource = dt;
                gridViewReferences.DataBind();
                ClearReferenceTxtBoxes();
            }
            else
            {
                DataTable dt = (DataTable)ViewState["References"];
                //DataTable dt = gridViewReferences.DataSource as DataTable;

                lblRef.Text = dt.Rows.Count.ToString();
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
                //ViewState["References"] = dt;
                //this.BindGrid();
                ViewState["References"] = dt;
                gridViewReferences.DataSource = dt;
                gridViewReferences.DataBind();
                ClearReferenceTxtBoxes();

                //if(dt.Rows.Count == 0)
                //{
                //    CreateFakeDataTable();
                //    int rowCount = dt.Rows.Count;
                //    int newIndex = 0;

                //    if (rowCount <= 0)
                //    {
                //        newIndex = 0;
                //    }
                //    else
                //    {
                //        int fakeIndex = rowCount - 1;
                //        string highestID = gridViewReferences.Rows[fakeIndex].Cells[0].Text;
                //        newIndex = Convert.ToInt32(highestID) + 1;
                //    }

                //    dt.Rows.Add(newIndex.ToString(), txtAuthor.Text.Trim(), txtYear.Text.Trim(), txtTitle.Text.Trim(), txtURL.Text.Trim());
                //    ViewState["References"] = dt;
                //    this.BindGrid();
                //    ClearReferenceTxtBoxes();
                //}
                //else
                //{
                //    int rowCount = dt.Rows.Count;
                //    int newIndex = 0;

                //    if (rowCount <= 0)
                //    {
                //        newIndex = 0;
                //    }
                //    else
                //    {
                //        int fakeIndex = rowCount - 1;
                //        string highestID = gridViewReferences.Rows[fakeIndex].Cells[0].Text;
                //        newIndex = Convert.ToInt32(highestID) + 1;
                //    }

                //    dt.Rows.Add(newIndex.ToString(), txtAuthor.Text.Trim(), txtYear.Text.Trim(), txtTitle.Text.Trim(), txtURL.Text.Trim());
                //    ViewState["References"] = dt;
                //    this.BindGrid();
                //    ClearReferenceTxtBoxes();
                //}


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
                gridViewReferences.DataSource = dt;
                gridViewReferences.DataBind();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}