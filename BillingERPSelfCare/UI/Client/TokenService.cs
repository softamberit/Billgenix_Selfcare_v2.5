using BillingERPConn;
using BillingERPSelfCare.Bkash;
using BillingERPSelfCare.Bkash.Models;
using System;
using System.Collections;
using System.Data;

namespace BillingERPSelfCare.UI.Client
{
    public class TokenSevice
    {
        public TokenResponse GrantToken()
        {
            Hashtable ht = new Hashtable();
            DBUtility db = new DBUtility();
            TokenResponse tokenResponse = new TokenResponse();
            DataTable response = db.GetDataByProc("sp_SelfCare_getBkashToken");
            if (response.Rows.Count > 0)
            {
                int expiresIn = Convert.ToInt32(response.Rows[0]["expires_in"]);
                DateTime tokenCreationTime = Convert.ToDateTime(response.Rows[0]["created_at"]);
                DateTime currentTime = DateTime.Now;
                if (currentTime >= tokenCreationTime.AddSeconds(expiresIn)) 
                {
                    BkashService bkashService = new BkashService();
                    TokenResponse token =  bkashService.RefreshToken((response.Rows[0]["refresh_token"].ToString()));
                    if (string.IsNullOrEmpty(token.id_token))
                    {
                        token = bkashService.GrantToken();
                    }
                    ht.Add("id_token", token.id_token);
                    ht.Add("expires_in", token.expires_in);
                    ht.Add("refresh_token", token.refresh_token);
                    DataTable res = db.GetDataByProc(ht, "sp_SelfCare_InsertBkashToken");
                    var status = res.Rows[0]["Feedback"].ToString();
                    return token;
                }
                else
                {
                    tokenResponse.id_token = response.Rows[0]["id_token"].ToString();
                    tokenResponse.expires_in = response.Rows[0]["expires_in"].ToString();
                    tokenResponse.refresh_token = response.Rows[0]["refresh_token"].ToString();
                    return tokenResponse;
                }
            }
            else
            {
                BkashService bkashService = new BkashService();
                TokenResponse token = bkashService.GrantToken();
                ht.Add("id_token", token.id_token);
                ht.Add("expires_in", token.expires_in);
                ht.Add("refresh_token", token.refresh_token);
                DataTable res =db.GetDataByProc(ht,"sp_SelfCare_InsertBkashToken");
                var status = res.Rows[0]["Feedback"].ToString();
                return bkashService.GrantToken();
            }
        }
    }
}
