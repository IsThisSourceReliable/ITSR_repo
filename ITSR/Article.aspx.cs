using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using ITSR.CLASSES.ARTICLE;
using System.IO;

namespace ITSR
{
    public partial class Article : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadArticle();
            ListView1.DataSource = GetStuff();
            ListView1.DataBind();
        }

        /// <summary>
        /// Method loads article and sets lables and relevant values. 
        /// </summary>
        private void LoadArticle()
        {
            string articleID = Session["ArticleID"].ToString();
            Articles getArticle = new Articles();
            getArticle.ID = Convert.ToInt32(articleID);
            DataTable dt = getArticle.GetArticle();

            string referenceXML = dt.Rows[0]["reference_xml"].ToString();

            getArticle.upVotes = int.Parse(dt.Rows[0]["votes_up"].ToString());
            getArticle.downVotes = int.Parse(dt.Rows[0]["votes_down"].ToString());

            int totalVotes = getArticle.SetTotalVotes();
            double upVotePercent = getArticle.SetUpVotePercent(totalVotes);
            double downVotePercent = getArticle.SetDownVotesPercent(upVotePercent);

            SetArticleLables(dt);
            BindReferences(referenceXML);
            SetVotes(totalVotes, upVotePercent, downVotePercent);
        }

        private void SetArticleLables(DataTable dt)
        {
            hiddenArticleID.Value = dt.Rows[0]["idarticle"].ToString();
            lblArticleName.Text = dt.Rows[0]["title"].ToString();
            lblTypeOfOrg.Text = dt.Rows[0]["orgtype"].ToString();
            lblUpHouseMan.Text = dt.Rows[0]["publisher"].ToString();
            lblDomainOwner.Text = dt.Rows[0]["domainowner"].ToString();
            lblFinancer.Text = dt.Rows[0]["financing"].ToString();
            lblEditDate.Text = dt.Rows[0]["lastedit_date"].ToString();
            linkBtnLastEdit.Text = dt.Rows[0]["edituser"].ToString();
            articleText.InnerHtml = dt.Rows[0]["text"].ToString();
        }

        private void BindReferences(string xml)
        {
            if (xml == string.Empty)
            {
                lblRefText.Text = "This source article doesn't have any references";
            }
            else
            {
                lblRefText.Text = "";
                DataTable dt = DaStuff(xml);
                ListViewReferences.DataSource = dt;
                ListViewReferences.DataBind();
            }
        }

        private DataTable DaStuff(string xml)
        {
            StringReader theReader = new StringReader(xml);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet.Tables[0];
        }

        private void SetVotes(int totalVotes, double upVotes, double downVotes)
        {
            lblTotalVotes.Text = totalVotes.ToString();
            upvoteBar.Style.Add("width", "" + upVotes + "%");
            downvoteBar.Style.Add("width", "" + downVotes + "%");
        }

        /// <summary>
        /// Just for testpurpose
        /// </summary>
        /// <returns></returns>
        private DataTable GetStuff()
        {
            MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT iderik_table, erik_text FROM erik_table; ", conn);

                MySqlDataAdapter da = new MySqlDataAdapter();

                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                da.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLinkTest_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/DaTest.aspx");
            //Session["id"] = 
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //Label3.Text = "Index: " + e.Item.DataItemIndex.ToString() + " , Arg: " + e.CommandArgument;
            //if(e.CommandName == "ReportComment")
            //{
            //    Label3.Text = "Report!";
            //}
            string value = e.CommandName.ToString();
            string listViewIndex = e.Item.DataItemIndex.ToString();
            string dataBaseIndex = e.CommandArgument.ToString();
            Label lbltext = (Label)e.Item.FindControl("Label2");
            switch (value)
            {
                case "ReportComment":
                    ReportComment(listViewIndex, dataBaseIndex, lbltext);
                    break;

                case "DeleteComment":
                    DeleteComment(listViewIndex, dataBaseIndex, lbltext);
                    break;

                default:
                    Label3.Text = "Default";
                    break;
            }

        }

        private void ReportComment(string listViewIndex, string dataBaseIndex, Label lbltext)
        {
            Label3.Text = "Report!";
            lblIndexListView.Text = listViewIndex;
            lblIndexDataBase.Text = dataBaseIndex;
            lblUserName.Text = lbltext.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
        }

        private void DeleteComment(string listViewIndex, string dataBaseIndex, Label lbltext)
        {
            Label3.Text = "Delete!";
            lblIndexListView.Text = listViewIndex;
            lblIndexDataBase.Text = dataBaseIndex;
            lblUserName.Text = lbltext.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
        }

        protected void lBtnEdit_Click(object sender, EventArgs e)
        {           
            Session["ArticleID"] = hiddenArticleID.Value.ToString();
            Response.Redirect("~/EditArticle.aspx");
        }
    }
}