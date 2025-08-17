using System;

namespace BillingERPSelfCare.BusinessEntity
{
    public  class PaymentLog
    {
        public int PaymentLogID { get; set; }
        //public string tran_id { get; set; }
        //public Nullable<System.DateTime> tran_date { get; set; }
        //public string status { get; set; }
        //public string val_id { get; set; }
        //public decimal amount { get; set; }
        //public decimal store_amount { get; set; }
        //public string currency { get; set; }
        //public string bank_tran_id { get; set; }
        //public string card_type { get; set; }
        //public string card_no { get; set; }
        //public string card_issuer { get; set; }
        //public string card_brand { get; set; }
        //public string card_issuer_country { get; set; }
        //public string card_issuer_country_code { get; set; }
        //public string currency_type { get; set; }
        //public Nullable<decimal> currency_amount { get; set; }
        //public Nullable<decimal> currency_rate { get; set; }
        //public Nullable<decimal> base_fair { get; set; }
        //public string value_a { get; set; }
        //public string value_b { get; set; }
        //public string value_c { get; set; }
        //public string value_d { get; set; }

        //public int emi_instalment { get; set; }
        //public decimal emi_amount { get; set; }
        //public string emi_description { get; set; }
        //public string emi_issuer { get; set; }
        //public string risk_title { get; set; }
        //public Nullable<int> risk_level { get; set; }


        //public decimal discount_percentage { get; set; }
        //public string discount_remarks { get; set; }



        //public string APIConnect { get; set; }
        //public string validated_on { get; set; }
        //public string gw_version { get; set; }
        public string customer_id { get; set; }

        //public string cus_name { get; set; }
        //public string cus_email { get; set; }
        //public string cus_phone { get; set; }



        public string store_id { get; set; }
        public string verify_sign { get; set; }
        public string verify_key { get; set; }
        public string store_passwd { get; set; }

       
       



        //================= 

        public string status { get; set; }
        public string tran_date { get; set; }
        public string tran_id { get; set; }
        public string val_id { get; set; }
        public string amount { get; set; }
        public string store_amount { get; set; }
        public string currency { get; set; }
        public string bank_tran_id { get; set; }
        public string card_type { get; set; }
        public string card_no { get; set; }
        public string card_issuer { get; set; }
        public string card_brand { get; set; }
        public string card_issuer_country { get; set; }
        public string card_issuer_country_code { get; set; }
        public string currency_type { get; set; }
        public string currency_amount { get; set; }
        public string currency_rate { get; set; }
        public string base_fair { get; set; }
        public string value_a { get; set; }
        public string value_b { get; set; }
        public string value_c { get; set; }
        public string value_d { get; set; }
        public string emi_instalment { get; set; }
        public string emi_amount { get; set; }
        public string emi_description { get; set; }
        public string emi_issuer { get; set; }
        public string risk_title { get; set; }
        public string risk_level { get; set; }
        public string discount_percentage { get; set; }
        public string discount_remarks { get; set; }
        public string APIConnect { get; set; }
        public string validated_on { get; set; }
        public string gw_version { get; set; }
        public string cus_name { get; set; }
        public string cus_email { get; set; }
        public string cus_phone { get; set; }


    }
}