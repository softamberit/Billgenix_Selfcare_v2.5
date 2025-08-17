using BillingERPSelfCare.BusinessEntity;
using BillingERPConn;
using System;
using System.Collections;
using static BillingERPSelfCare.SSLCommarz;

namespace BillingERPSelfCare.DataAccess
{
    public class DAPop
    {

        public long SavePOPpermission(BOPop BOItem)
        {
            DBUtility db = new DBUtility();
            Hashtable ht = new Hashtable();
            ht.Add("PinNO", BOItem.PinNo);
            ht.Add("POPID", BOItem.POPID);
            ht.Add("CreatedBy", BOItem.CreatedBy);
            long res = db.InsertData(ht, "sp_Insertpoppermission");

            return res;
        }
        
         public static string InsertCollectionEntrySSL(SSLCommerzValidatorResponse objPay, string narration, string invoice, string loginId)
        {
            try
            {
                var collectionDate = DateTime.Today;
                //var collectionNo = UniqueCode.AutoCodeGenerate();

                var dbUtil = new DBUtility();

                // var transactionId=

//                WriteLogFile.WriteLog("result1" + "ClientId " + objPay.customer_id + "Amount " + objPay.amount + "PayMode" + objPay.card_type + "PinNumber" + "10000" + "TransactionId" + objPay.tran_id + "BankName" + "ChequeNo" +
//"ChequeDate" + "CollectionDate" + collectionDate.ToString() + "MRNo" + invoice + "Narration" + narration + "CollectionBy" + loginId);

                var ht = new Hashtable
                {
                    //{"CollectionNo", collectionNo},
                    {"ClientId", loginId},
                    {"Amount", Convert.ToDecimal(objPay.amount)}, // invoice Amount
                    {"PayMode", objPay.card_type}, //card_type
                    {"PinNumber", 10000}, //userId
                    {"TransactionId", objPay.tran_id},
                    {"BankName", ""}, //
                    {"ChequeNo", ""}, //
                    {"ChequeDate", ""}, //
                    {"CollectionDate", collectionDate},
                    {"MRNo", invoice}, //invoiceID Id
                    {"Narration", narration},
                    {"CollectionBy", loginId}
                };

//                WriteLogFile.WriteLog("result2" + "ClientId "+ objPay.customer_id+"Amount "+ objPay.amount+"PayMode"+ objPay.card_type+"PinNumber"+ "10000"+"TransactionId" +objPay.tran_id+"BankName"+"ChequeNo" +
//"ChequeDate" +"CollectionDate" + collectionDate.ToString()+"MRNo"+ invoice+ "Narration"+ narration+"CollectionBy"+ loginId);
                  
                return dbUtil.InsertData(ht, "InsertCollectionEntrySSL").ToString(); ;

            }
            catch (Exception ex)
            {
               // WriteLogFile.WriteLog("Insert Collection exception message " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }


        public static string InsertCollectionEntrySSL_IPN(SSLCommerzValidatorResponse objPay, string narration, string invoice, string loginId)
        {
            try
            {
                var collectionDate = DateTime.Today;

                var dbUtil = new DBUtility();
                
                var ht = new Hashtable
                {
                    //{"CollectionNo", collectionNo},
                    {"ClientId", loginId},
                    {"Amount", Convert.ToDecimal(objPay.amount)}, // invoice Amount
                    {"PayMode", objPay.card_type}, //card_type
                    {"PinNumber", 10000}, //userId
                    {"TransactionId", objPay.tran_id},
                    {"BankName", ""}, //
                    {"ChequeNo", ""}, //
                    {"ChequeDate", ""}, //
                    {"CollectionDate", collectionDate},
                    {"MRNo", invoice}, //invoiceID Id
                    {"Narration", narration},
                    {"CollectionBy", loginId}
                };

                //WriteLogFile.WriteLog("result2" + "ClientId "+ objPay.customer_id+"Amount "+ objPay.amount+"PayMode"+ objPay.card_type+"PinNumber"+ "10000"+"TransactionId" +objPay.tran_id+"BankName"+"ChequeNo" +
                //"ChequeDate" +"CollectionDate" + collectionDate.ToString()+"MRNo"+ invoice+ "Narration"+ narration+"CollectionBy"+ loginId);

                return dbUtil.InsertData(ht, "InsertCollectionEntrySSL_IPNTEST").ToString(); ;

            }
            catch (Exception ex)
            {
                // WriteLogFile.WriteLog("Insert Collection exception message " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }

        public static int SavePaymentLogFromSSL(SSLCommerzValidatorResponse objPay, string customerid, string ip,string source)
        {
            var dbUtil = new DBUtility();

            var ht = new Hashtable
            {
                {"tran_id", objPay.tran_id},
                {"tran_date", objPay.tran_date},
                {"status", objPay.status},
                {"val_id", objPay.val_id},
                {"amount", objPay.amount},
                {"store_amount", objPay.store_amount},
                {"currency", objPay.currency},
                {"bank_tran_id", objPay.bank_tran_id},
                {"card_type", objPay.card_type},
                {"card_no", objPay.card_no},
                {"card_issuer", objPay.card_issuer},
                {"card_brand", objPay.card_brand},
                {"card_issuer_country", objPay.card_issuer_country},
                {"card_issuer_country_code", objPay.card_issuer_country_code},
                {"currency_type", objPay.currency_type},
                {"currency_amount", objPay.currency_amount},
                {"currency_rate", objPay.currency_rate},
                {"base_fair", objPay.base_fair},
                {"value_a", objPay.value_a},
                {"value_b", objPay.value_b},
                {"value_c", objPay.value_c},
                {"risk_title", objPay.risk_title},
                {"risk_level", objPay.risk_level},
                {"APIConnect", objPay.APIConnect},
                {"validated_on", objPay.validated_on},
                {"gw_version", objPay.gw_version},
                {"customerId", customerid} ,
                {"publicIP", ip}   ,
                {"Source", source}


            };


            var status = Convert.ToInt32(dbUtil.InsertData(ht, "SSLPaymentLog_InsertSP"));
            return status;
        }
    }
}