using BillingERPSelfCare.BusinessEntity;
using BillingERPSelfCare.Utility;
using BillingERPConn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.DataAccess
{
    public class DAMenu
    {

        public DAMenu()
        {
        }

        /// <summary>
        /// Page Authenitaction Check
        /// </summary>
        /// <param name="PinNo"></param>
        /// <param name="PageName"></param>
        /// <returns></returns>
        public static int PageAuthenticationCheck(string PinNo, string PageName)
        {

            try
            {
            var idb = new DBUtility();            
            string pqry = @"select count(*) from dbo.AppsPermission where 
            PIN='" + PinNo + "'  and menu_id in   (select menu_id from dbo.AppsMenus where url='" + PageName + "')";
            
            int pagcount = Conversion.TryCastInteger(idb.AggRetrive(pqry).ToString());
            
            if (pagcount > 0)

                return 1;
            else

                return 0;
            }
            catch (Exception)
            {
                
                return 0;
            }
        }


        /// <summary>
        /// Left Menu 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="path"></param>
        /// <param name="pinnumber"></param>
        /// <returns></returns>
        public static string GetContentPageMenu(string pinnumber)
        {
            string FinalMenuString = "";
            try
            {
               
                    string menuChild = string.Empty;
                    string menu = string.Empty;  

                    Collection<BOMenu> rootMenus = GetMenuCollection(pinnumber);

                    if (rootMenus == null)
                    {
                        return string.Empty;
                    }

                    if (rootMenus.Count > 0)
                    {
                        int i = 1;

                        string top = "";
                        foreach (BOMenu rootMenu in rootMenus)
                        {

                           

                            

                            Collection<BOMenu> childMenus = GetChildMenuCollection(rootMenu.MenuId, 2, pinnumber);

                            if (childMenus.Count > 0)
                            {
                                string expandClass = "collapse";
                                if (rootMenu.IsExpand == true)
                                {
                                    expandClass = "collapse";
                                }



                                //top = "<li> <a href='#' class='has-arrow' aria-expanded='false'> <span class='has-icon'><i class=" + rootMenu.IconCss + "></i></span>"

                                top = "<li> <a href='#' class='has-arrow'> <span class='has-icon'><i class=" + rootMenu.IconCss + "></i></span>"
                                    + "<span class='nav-title'>" + rootMenu.MenuText + "</span>  </a><ul aria-expanded='false' class='" + expandClass + "'>";

                                foreach (BOMenu childMenu in childMenus)
                                {
                                    menuChild += string.Format(System.Threading.Thread.CurrentThread.CurrentCulture, "<li><a href=" + childMenu.Url + ">" + childMenu.MenuText + "</a></li>");
                                    // menu += string.Format(System.Threading.Thread.CurrentThread.CurrentCulture, "<a href='{0}' title='{1}' data-menucode='{2}' class='sub-menu-anchor'>{1}</a>", page.ResolveUrl(childMenu.Url), childMenu.MenuText, childMenu.MenuCode);
                                }



                                menu += top + menuChild + "</ul></li>";

                                top = "";
                                menuChild = "";
                            }


                            
                            i = i + 1;
                        }

                        
                    }

                    FinalMenuString = menu ;

                    return FinalMenuString;
               
            }
            catch (Exception)
            {

                return "";
            }


        }

        private static Collection<BOMenu> GetChildMenuCollection(int parentMenuId, int p2, string pinnumber)
        {
            try
            {

                var idb = new DBUtility();
                Collection<BOMenu> collection = new Collection<BOMenu>();

                Hashtable ht = new Hashtable();
                ht.Add("PIN", pinnumber);
                ht.Add("parentMenuId", parentMenuId);

                DataTable table = idb.GetDataByProc(ht, "sp_getChildMenusByUrl");
                if (table.Rows.Count > 0)
                {

                    foreach (DataRow row in table.Rows)
                    {
                        BOMenu model = new BOMenu();

                        model.MenuId = Conversion.TryCastInteger(row["menu_id"]);
                        model.MenuText = Conversion.TryCastString(row["menu_text"]);
                        model.Url = Conversion.ResolveUrl(Conversion.TryCastString(row["url"]));
                        model.MenuCode = Conversion.TryCastString(row["menu_code"]);
                        model.Level = Conversion.TryCastInteger(row["level"]);
                        model.ParentMenuId = Conversion.TryCastInteger(row["parent_menu_id"]);
                        model.CssClass = Conversion.TryCastString(row["CssClass"]);

                        collection.Add(model);
                    }
                }

                return collection;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static Collection<BOMenu> GetMenuCollection(string pinnumber)
        {
            Collection<BOMenu> collection = new Collection<BOMenu>();

            var idb = new DBUtility();          

            Hashtable ht = new Hashtable();
            ht.Add("PIN", pinnumber);     

            DataTable table = idb.GetDataByProc(ht, "sp_getMenusByUrl");          

            foreach (DataRow row in table.Rows)
            {
                BOMenu model = new BOMenu();

                model.MenuId = Conversion.TryCastInteger(row["menu_id"]);
                model.MenuText = Conversion.TryCastString(row["menu_text"]);
                model.Url = Conversion.ResolveUrl(Conversion.TryCastString(row["url"]));
                model.MenuCode = Conversion.TryCastString(row["menu_code"]);
                model.Level = Conversion.TryCastInteger(row["level"]);
                model.ParentMenuId = Conversion.TryCastInteger(row["parent_menu_id"]);
                model.CssClass = Conversion.TryCastString(row["CssClass"]);
                model.MenuName = Conversion.TryCastString(row["ParentMenuText"]);
                model.IconCss = Conversion.TryCastString(row["IconCss"]);
                model.IsExpand = Conversion.TryCastBoolean(row["IsExpand"]);

                collection.Add(model);
            }
            
            return collection;
        }



        #region MenuPermission

        public long SaveMenupermission(BOMenu BOItem)
        {
            DBUtility db = new DBUtility();
            Hashtable ht = new Hashtable();
            ht.Add("PinNO", BOItem.PinNo);
            ht.Add("MenuID", BOItem.MenuId);
            ht.Add("UserID", BOItem.CreatedBy);
            long res = db.InsertData(ht, "sp_InsertAppspermission");

            return res;
        }
        #endregion


    }
}