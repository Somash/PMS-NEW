using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.Models.DbModels;

namespace PMS.RPTReportASPX
{
    public partial class ReportViwerASPX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<PMS.Models.DbModels.NewProject> NewProjectsObj = null;

                using (NewProjectDBContext np = new NewProjectDBContext())
                {

                    NewProjectsObj = np.NewProject.OrderBy(a => a.ProjectName).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/NewProjectRdtReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rnp = new ReportDataSource("MyDataset", NewProjectsObj);
                    ReportViewer1.LocalReport.DataSources.Add(rnp);
                    ReportViewer1.LocalReport.Refresh();
                }
                
            }

        }
    }
}