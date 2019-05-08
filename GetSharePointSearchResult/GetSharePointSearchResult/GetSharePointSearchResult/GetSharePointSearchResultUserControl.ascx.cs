using Microsoft.SharePoint;
using Microsoft.SharePoint.Search.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;

namespace GetSharePointSearchResult.GetSharePointSearchResult
{
    public partial class GetSharePointSearchResultUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SPSite site = SPContext.Current.Site)
            {
                KeywordQuery kq = new KeywordQuery(site);
                kq.SourceId = new Guid("8413cd39-2156-4e00-b54d-11efd9abdb89");
                kq.QueryText = "* " + searchText.Text;
                kq.SelectProperties.Add("Title");
                kq.SelectProperties.Add("Path");
                kq.RowLimit = 20;
                ResultTableCollection resultTables = new SearchExecutor().ExecuteQuery(kq);

                DataTable resultDataTable = resultTables.FirstOrDefault().Table;

                List<SearchResult> searchResultList = new List<SearchResult>();

                foreach (DataRow row in resultDataTable.Rows)
                {
                    SearchResult searchResult = new SearchResult();

                    searchResult.Title = row["Title"].ToString();
                    searchResult.Url = row["Path"].ToString();

                    searchResultList.Add(searchResult);
                }

                rptSearch.DataSource = searchResultList;
                rptSearch.DataBind();
            }
        }

        public class SearchResult
        {
            public string Title { set; get; }
            public string Url { set; get; }
        }
    }
}
