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
    public partial class CreateArticle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserID"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("id"), new DataColumn("Author"), new DataColumn("Year"), new DataColumn("Title"), new DataColumn("URL") });
                ViewState["References"] = dt;
                this.BindGrid();
                BindDropDown();

                lblTitleFail.Visible = false;
                lblURLFail.Visible = false;
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

            if(gridViewReferences.Rows.Count == 0)
            {
                lblRef.Text = "Add some references to this source";
            }
            else
            {
                lblRef.Text = "";
            }
        }

        private string RemoveHTTP()
        {
            string oldURL = txtArticleURL.Text;
            string newURL = oldURL.Replace("http://", "");
            newURL = newURL.Replace("https://", "");

            return newURL;
        }

        /// <summary>
        /// This method uses Articles class and methods in said class to
        /// add an article to the database.
        /// </summary>
        /// <returns></returns>
        private bool AddArticle()
        {
            bool ok = false;
            Articles sourceArticle = new Articles();

            sourceArticle.Title = txtArticleTitle.Text;
            sourceArticle.Text = txtInfo.Text.Replace("\r\n", "<br />");
            sourceArticle.AricleURL = RemoveHTTP();
            sourceArticle.TypeOfOrg_id = Convert.ToInt32(dropDownTypeOfOrg.SelectedValue);
            sourceArticle.lastEdit = DateTime.Now;
            sourceArticle.Publisher = txtUpHouseMan.Text;
            sourceArticle.domainOwner = txtDomainOwner.Text;
            sourceArticle.Financing = txtFinancer.Text;
            sourceArticle.Reference = CreateXML();
            sourceArticle.createUser_id = 21; //Has to be changed to whatever user that is logged in.
            sourceArticle.upVotes = 0; //Standard values for now
            sourceArticle.downVotes = 0; //Standard values for now
            sourceArticle.lastEditUser_id = 21; //Has to be changed to whatever user that is logged in.

            if (sourceArticle.CreateArticle())
            {
                ok = true;
            }

            return ok;
        }

        /// <summary>
        /// This method creats an XML of the references that a user has added to 
        /// the gridview.
        /// </summary>
        /// <returns></returns>
        private string CreateXML()
        {
            DataTable dt = (DataTable)ViewState["References"];
            if (dt.Rows.Count == 0)
            {
                string empty = string.Empty;
                return empty;
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                dt.WriteXml(ms);
                ms.Position = 0;
                string xmlContentx = new StreamReader(ms).ReadToEnd();
                return xmlContentx;
            }
        }

        /// <summary>
        /// Checks if there already is a source with title
        /// </summary>
        /// <returns></returns>
        private bool CheckTitle()
        {
            Articles titleSearch = new Articles();
            bool ok = true;
            string title = txtArticleTitle.Text;
            DataTable dt = titleSearch.SearchForSpecificArticle(title);

            if (dt.Rows.Count <= 0)
            {
                ok = false;
                lblTitleFail.Visible = false;
            }
            else
            {
                lblTitleFail.Visible = true;
                lblTitleFail.Text = "Obs, it seems like there already is a source article with this name.";
            }

            return ok;
        }

        /// <summary>
        /// Checks if there already is a source with URL
        /// </summary>
        /// <returns></returns>
        private bool CheckURL()
        {
            Articles titleSearch = new Articles();
            bool ok = true;
            string url = RemoveHTTP();
            DataTable dt = titleSearch.SearchForSpecificArticle(url);

            if (dt.Rows.Count <= 0)
            {
                ok = false;
                lblURLFail.Visible = false;
            }
            else
            {
                lblURLFail.Visible = true;
                lblURLFail.Text = "Obs, it seems like there already is a source article with this URL.";
            }

            return ok;
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

        /// <summary>
        /// Resets the gui and everything to standard values. 
        /// </summary>
        private void ResetEverything()
        {
            txtArticleTitle.Text = "";
            txtInfo.Text = "";
            txtURL.Text = "";
            txtUpHouseMan.Text = "";
            txtDomainOwner.Text = "";
            txtFinancer.Text = "";
            txtArticleURL.Text = string.Empty;

            lblTitleFail.Visible = false;
            lblURLFail.Visible = false;

            BindDropDown();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("id"), new DataColumn("Author"), new DataColumn("Year"), new DataColumn("Title"), new DataColumn("URL") });
            ViewState["References"] = dt;
            this.BindGrid();
        }

        /// <summary>
        /// Event for rowcommand.
        /// </summary>
        protected void gridViewReferences_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "EditRow")
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
            else if(e.CommandName == "DeleteRow")
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

        /// <summary>
        /// Event for when user clicks add reference button.
        /// Adds reference to the gridview.
        /// </summary>
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

        /// <summary>
        /// Event for when user clicks add article and uses other methods to get it done.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if(CheckTitle() || CheckURL())
            {

            }
            else
            {
                if (AddArticle())
                {
                    //Maybe redirect user to newly created article here?
                    ResetEverything();
                }
                else
                {
                    lblTitleFail.Text = "Obs something went wrong when creating the article, try again!";
                }
            }

        }

    }
}