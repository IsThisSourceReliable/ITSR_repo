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
                BindDropDown();
                LoadArticle();

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
        /// This method load an article that a user has serached for. Calls other classes
        /// and method in said classes and methods in code behind to perfom.
        /// </summary>
        private void LoadArticle()
        {
            string articleID = Session["ArticleID"].ToString();
            Articles article = new Articles();
            article.ID = Convert.ToInt32(articleID);
            DataTable dt = article.GetArticle();

            hiddenArticleID.Value = dt.Rows[0]["idarticle"].ToString();
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

        /// <summary>
        /// This method binds the gridview with values from the database. Recives 
        /// string xml from database and evaluates said string. Uses viewstate later on 
        /// to be able to use gridview without having to update database.
        /// </summary>
        /// <param name="xml"></param>
        private void BindGridDataSource(string xml)
        {
            if (xml == string.Empty)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("id"), new DataColumn("Author"), new DataColumn("Year"), new DataColumn("Title"), new DataColumn("URL") });
                ViewState["References"] = dt;
                gridViewReferences.DataSource = (DataTable)ViewState["References"];
                gridViewReferences.DataBind();

                if (gridViewReferences.Rows.Count == 0)
                {
                    lblRef.Text = "Add some references to this source";
                }
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

        /// <summary>
        /// This method gets the xml as a string and returns it as a 
        /// datatable to be able to bind xml/datatable with bridview
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private DataTable DaStuff(string xml)
        {
            StringReader theReader = new StringReader(xml);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet.Tables[0];
        }

        /// <summary>
        /// This method removs the http/https to be able to store in database.
        /// </summary>
        /// <returns></returns>
        private string RemoveHTTP()
        {
            string oldURL = txtArticleURL.Text;
            string newURL = oldURL.Replace("http://", "");
            newURL = newURL.Replace("https://", "");

            return newURL;
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
        /// This method checks the database if there is an article with the same title.
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
                string idArticle = dt.Rows[0]["idarticle"].ToString();
                string hiddenID = hiddenArticleID.Value;
                if(idArticle == hiddenID)
                {
                    ok = false;
                    lblTitleFail.Visible = false;
                }
                else
                {
                    lblTitleFail.Visible = true;
                    lblTitleFail.Text = "Obs, it seems like there already is a source article with this name.";
                }
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
                string idArticle = dt.Rows[0]["idarticle"].ToString();
                string hiddenID = hiddenArticleID.Value;
                if (idArticle == hiddenID)
                {
                    ok = false;
                    lblTitleFail.Visible = false;
                }
                else
                {
                    lblURLFail.Visible = true;
                    lblURLFail.Text = "Obs, it seems like there already is a source article with this URL.";
                }
            }

            return ok;
        }

        /// <summary>
        /// This method uses Articles class and methods in said class to
        /// add an article to the database.
        /// </summary>
        /// <returns></returns>
        private bool UpdateArticle()
        {
            bool ok = false;
            Articles sourceArticle = new Articles();

            sourceArticle.ID = Convert.ToInt32(hiddenArticleID.Value);

            sourceArticle.Title = txtArticleTitle.Text;
            sourceArticle.Text = txtInfo.Text.Replace("\r\n", "<br />");
            sourceArticle.AricleURL = RemoveHTTP();
            sourceArticle.TypeOfOrg_id = Convert.ToInt32(dropDownTypeOfOrg.SelectedValue);
            sourceArticle.lastEdit = DateTime.Now;
            sourceArticle.Publisher = txtUpHouseMan.Text;
            sourceArticle.domainOwner = txtDomainOwner.Text;
            sourceArticle.Financing = txtFinancer.Text;
            sourceArticle.lastEditUser_id = int.Parse(Session["UserID"].ToString());
            sourceArticle.Reference = CreateXML();

            if(sourceArticle.SaveOldArticle())
            {
                if (sourceArticle.UpdateArticle())
                {
                    ok = true;
                }
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
        /// Event for when user wants to add/update references. Checks wheter or not 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                ViewState["References"] = dt;
                gridViewReferences.DataSource = dt;
                gridViewReferences.DataBind();
                ClearReferenceTxtBoxes();
            }
        }

        /// <summary>
        /// Event for when a user clicks a linkbutton in the gridview. Calls other methods to perfrom.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string id = e.CommandArgument.ToString();
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

        /// <summary>
        /// Event for when user clicks the save button to be able to save the newly edited data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckTitle() || CheckURL())
            {

            }
            else
            {
                if (UpdateArticle())
                {
                    Session["ArticleID"] = hiddenArticleID.Value.ToString();
                    Response.Redirect("~/Article.aspx");
                    //ResetEverything();
                }
                else
                {
                    lblTitleFail.Text = "OBS, something went wrong during when trying to update, try again!";
                }
            }
        }
    }
}