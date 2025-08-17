namespace ReportBilling
{
    /// <summary>
    /// Summary description for InvoiceReport.
    /// </summary>
    public partial class rptCustomerLedger : Telerik.Reporting.Report
    {
        public rptCustomerLedger()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            sdc_CustomerInfo.ConnectionString = ReportConnectionManager.GetConnectionString();
            sdc_CustomerLedger.ConnectionString = ReportConnectionManager.GetConnectionString();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public string CustomerID
        {
            get
            {
                return (string)sdc_CustomerLedger.Parameters[0].Value;
            }
            set
            {
                sdc_CustomerLedger.Parameters[0].Value = value;
                sdc_CustomerInfo.Parameters[0].Value = value;
            }

        }
    }
}