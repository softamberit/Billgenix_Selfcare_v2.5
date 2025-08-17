using System;

namespace ReportBilling
{
    /// <summary>
    /// Summary description for InvoiceReport.
    /// </summary>
    public partial class rptInvoiceStatement : Telerik.Reporting.Report
    {
        public rptInvoiceStatement()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            sdc_InvoiceStatement.ConnectionString = ReportConnectionManager.GetConnectionString();
          
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public int POPID
        {
            get
            {
                return (int)sdc_InvoiceStatement.Parameters[0].Value;
            }
            set
            {
                sdc_InvoiceStatement.Parameters[0].Value = value;
            }

        }
        public int All
        {
            get
            {
                return (int)sdc_InvoiceStatement.Parameters[1].Value;
            }
            set
            {
                sdc_InvoiceStatement.Parameters[1].Value = value;
            }

        }
        public DateTime StartDate
        {
            get { return (DateTime)sdc_InvoiceStatement.Parameters[2].Value; }
            set
            {
                sdc_InvoiceStatement.Parameters[2].Value = value;
            }
        }

        public DateTime EndDate
        {
            get { return (DateTime)sdc_InvoiceStatement.Parameters[3].Value; }
            set
            {
                sdc_InvoiceStatement.Parameters[3].Value = value;
            }
        }
    }
}