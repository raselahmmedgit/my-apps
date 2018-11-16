﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using com.gpit.DataContext;
using com.gpit.Model;
using PayrollWeb.ViewModels;
using System.Web.Security;

namespace PayrollWeb.Utility
{
    public class SubMenuGenerator
    {
        private readonly payroll_systemContext dataContext;

        public SubMenuGenerator(payroll_systemContext cont)
        {
            this.dataContext = cont;
        }

        public List<SubMenu> GenerateSubMenuByMenuName(string menu_name, string roleName)
        {
            var Menu = dataContext.prl_menu.FirstOrDefault(x => x.menue_name == menu_name);
            var lstPrivilege = dataContext.prl_role_privilege.Where(x => x.menu_id == Menu.id && x.role == roleName).Select(x => x.sub_menu_id).ToList();
            var lstSubMenus = dataContext.prl_sub_menu.Where(p => p.id == 0).ToList();
            if (lstPrivilege.Count > 0)
            {
                lstSubMenus = dataContext.prl_sub_menu.Where(p => lstPrivilege.Contains(p.id)).ToList();
            }
            
            return Mapper.Map<List<SubMenu>>(lstSubMenus);
        }

        public void GenerateSubMenu(out List<SubMenu> lst, out string msg, string menu_name, string usrName)
        {
            string rolName = "";
            msg = "";
            var lstSubMenus = dataContext.prl_sub_menu.Where(p => p.id == 0).ToList();

            try
            {
                string[] roles = Roles.GetRolesForUser(usrName);
                
                if (roles.Count() > 0)
                {
                    rolName = roles[0];
                    var Menu = dataContext.prl_menu.FirstOrDefault(x => x.menue_name == menu_name);
                    var lstPrivilege = dataContext.prl_role_privilege.Where(x => x.menu_id == Menu.id && x.role == rolName).Select(x => x.sub_menu_id).ToList();

                    if (lstPrivilege.Count > 0)
                    {
                        lstSubMenus = dataContext.prl_sub_menu.Where(p => lstPrivilege.Contains(p.id)).ToList();
                    }
                    else
                    {
                        msg = "You are not authorised to access the desired page.";
                    }
                }
                else
                {
                    msg = "You are not assigned to any role.";
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
            lst = Mapper.Map<List<SubMenu>>(lstSubMenus);
        }
    }
}