using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ViewModel;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string Message()
        {
            return General.message;
        }
        public string AdminType()
        {
            return General.TYPE_ADMIN;
        }
        public string UserType()
        {
            return General.TYPE_USER;
        }
        public string doSelect()
        {
            return ViewModel.DB.doSelect;
        }
        public string doInsert()
        {
            return ViewModel.DB.doInsert;
        }
        public string doUpdate()
        {
            return ViewModel.DB.doUpdate;
        }
        public string doDelete()
        {
            return ViewModel.DB.doDelete;
        }
        //======================================================
        // Login
        // =====================================================
        public UserLogged Login(string Username, string Password)
        {
            if (!ViewModel.DB.OpenDatabase())
            {
                General.message = General.ERR_CONNECT_DB;
                return null;
            }

            PasswordsDB passwordsDB = new PasswordsDB();
            PasswordsRec password = passwordsDB.SelectByUsernameAndPassword(Username, Password);

            if (password == null)
            {
                General.message = General.ERR_WRONG_USERNAME;
                return null;
            }

            UserLogged user = new UserLogged();

            if (password != null)
            {
                switch (password.Type)
                {
                    case General.TYPE_ADMIN:
                        AdminsDB adminsDB = new AdminsDB();
                        AdminsRec adminsRec = adminsDB.SelectByUsername(password.Username);
                        if (adminsRec != null)
                        {
                            user.ID = adminsRec.ID;
                            user.Name = adminsRec.Name;
                        }
                        else
                        {
                            user = null;
                            General.message = General.ERR_ADMIN_NOT_FOUND;
                        }
                        break;
                    case General.TYPE_USER:
                        UsersDB usersDB = new UsersDB();
                        UsersRec usersRec = usersDB.SelectByUsername(password.Username);
                        if (usersRec != null)
                        {
                            user.ID = usersRec.ID;
                            user.Name = usersRec.Name;
                        }
                        else
                        {
                            user = null;
                            General.message = General.ERR_USER_NOT_FOUND;
                        }
                        break;
                }
            }

            ViewModel.DB.CloseDatabase();
            General.message = DB.Error;

            if (user == null) return null;

            return new UserLogged(password.Username, password.Password, password.Type, user.ID, user.Name);
        }
        //======================================================
        // Admins
        // =====================================================
        public AdminsList AdminsSelectAll()
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                AdminsDB adminsDB = new AdminsDB();
                AdminsList adminsList = adminsDB.SelectAll();
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return adminsList;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public AdminsRec AdminsSelect(long ID)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                AdminsDB adminsDB = new AdminsDB();
                AdminsRec admin = new AdminsRec();
                admin.ID = ID;
                admin = (AdminsRec)adminsDB.Select(admin);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return admin;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public bool AdminsInsert(AdminsRec admin)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                AdminsDB adminsDB = new AdminsDB();
                bool result = adminsDB.Insert(admin);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool AdminsUpdate(AdminsRec admin)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                AdminsDB adminsDB = new AdminsDB();
                bool result = adminsDB.Update(admin);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool AdminsDelete(long ID)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                AdminsDB adminsDB = new AdminsDB();
                AdminsRec admin = new AdminsRec();
                admin.ID = ID;
                bool result = adminsDB.Delete(admin);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }

        public UserLogged AdminsInsertWithPassword(AdminsRec admin, PasswordsRec password)
        {
            password.Type = AdminType();
            return SignUp(admin, password);
        }

        //======================================================
        // Drugs
        // =====================================================
        public DrugsList DrugsSelectAll()
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugsDB drugsDB = new DrugsDB();
                DrugsList drugsList = drugsDB.SelectAll();
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return drugsList;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public DrugsRec DrugsSelect(string Code)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugsDB drugsDB = new DrugsDB();
                DrugsRec drug = new DrugsRec();
                drug.Code = Code;
                drug = (DrugsRec)drugsDB.Select(drug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return drug;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public bool DrugsInsert(DrugsRec drug)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugsDB drugsDB = new DrugsDB();
                bool result = drugsDB.Insert(drug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool DrugsUpdate(AdminsRec drug)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugsDB drugsDB = new DrugsDB();
                bool result = drugsDB.Update(drug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool DrugsDelete(string Code)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugsDB drugsDB = new DrugsDB();
                DrugsRec drug = new DrugsRec();
                drug.Code = Code;
                bool result = drugsDB.Delete(drug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }

        //======================================================
        // DrugTypes
        // =====================================================
        public DrugTypesList DrugTypesSelectAll()
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugTypesDB drugtypesDB = new DrugTypesDB();
                DrugTypesList drugtypesList = drugtypesDB.SelectAll();
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return drugtypesList;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public DrugTypesRec DrugTypesSelect(string Code)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugTypesDB drugtypesDB = new DrugTypesDB();
                DrugTypesRec drugtype = new DrugTypesRec();
                drugtype.Code = Code;
                drugtype = (DrugTypesRec)drugtypesDB.Select(drugtype);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return drugtype;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public bool DrugTypesInsert(DrugTypesRec drugtype)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugTypesDB drugtypesDB = new DrugTypesDB();
                bool result = drugtypesDB.Insert(drugtype);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool DrugTypesUpdate(DrugTypesRec drugtype)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugTypesDB drugtypesDB = new DrugTypesDB();
                bool result = drugtypesDB.Update(drugtype);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool DrugTypesDelete(string Code)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                DrugTypesDB drugtypesDB = new DrugTypesDB();
                DrugTypesRec drugtype = new DrugTypesRec();
                drugtype.Code = Code;
                bool result = drugtypesDB.Delete(drugtype);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }


        //======================================================
        // Users
        // =====================================================
        public UsersList UsersSelectAll()
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDB usersDB = new UsersDB();
                UsersList usersList = usersDB.SelectAll();
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return usersList;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public UsersRec UsersSelect(long ID)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDB usersDB = new UsersDB();
                UsersRec user = new UsersRec();
                user.ID = ID;
                user = (UsersRec)usersDB.Select(user);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return user;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public bool UsersInsert(UsersRec user)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDB usersDB = new UsersDB();
                bool result = usersDB.Insert(user);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool UsersUpdate(UsersRec user)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDB usersDB = new UsersDB();
                bool result = usersDB.Update(user);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool UsersDelete(long ID)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDB usersDB = new UsersDB();
                UsersRec user = new UsersRec();
                user.ID = ID;
                bool result = usersDB.Delete(user);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public UserLogged UsersInsertWithPassword(UsersRec user, PasswordsRec password)
        {
            password.Type = UserType();
            return SignUp(user, password);
        }

        //======================================================
        // Passwords
        // =====================================================
        public PasswordsList PasswordsSelectAll()
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                PasswordsDB passwordsDB = new PasswordsDB();
                PasswordsList passwordsList = passwordsDB.SelectAll();
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return passwordsList;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public PasswordsRec PasswordsSelect(string Username)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                PasswordsDB passwordsDB = new PasswordsDB();
                PasswordsRec password = new PasswordsRec();
                password.Username = Username;
                password = (PasswordsRec)passwordsDB.Select(password);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return password;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public bool PasswordsInsert(PasswordsRec password)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                PasswordsDB passwordsDB = new PasswordsDB();
                bool result = passwordsDB.Insert(password);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool PasswordsUpdate(PasswordsRec password)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                PasswordsDB passwordsDB = new PasswordsDB();
                bool result = passwordsDB.Update(password);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool PasswordsDelete(string Username)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                PasswordsDB passwordsDB = new PasswordsDB();
                PasswordsRec password = new PasswordsRec();
                password.Username = Username;
                bool result = passwordsDB.Delete(password);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }

        //======================================================
        // UsersDrugs
        // =====================================================
        public UsersDrugsList UsersDrugsSelectAll()
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDrugsDB usersdrugsDB = new UsersDrugsDB();
                UsersDrugsList UsersDrugsList = usersdrugsDB.SelectAll();
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return UsersDrugsList;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public UsersDrugsRec UsersDrugsSelect(int UserID, string DrugCode)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDrugsDB usersdrugsDB = new UsersDrugsDB();
                UsersDrugsRec userdrug = new UsersDrugsRec();
                userdrug.UserID = UserID;
                userdrug.DrugCode = DrugCode;
                userdrug = (UsersDrugsRec)usersdrugsDB.Select(userdrug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return userdrug;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }
        }
        public bool UsersDrugsInsert(UsersDrugsRec userdrug)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDrugsDB usersdrugsDB = new UsersDrugsDB();
                bool result = usersdrugsDB.Insert(userdrug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool UsersDrugsUpdate(UsersDrugsRec userdrug)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDrugsDB userdrugsDB = new UsersDrugsDB();
                bool result = userdrugsDB.Update(userdrug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public bool UsersDrugsDelete(int UserID, string DrugCode)
        {
            try
            {
                ViewModel.DB.OpenDatabase();
                UsersDrugsDB userdrugsDB = new UsersDrugsDB();
                UsersDrugsRec userdrug = new UsersDrugsRec();
                userdrug.UserID = UserID;
                bool result = userdrugsDB.Delete(userdrug);
                ViewModel.DB.CloseDatabase();
                General.message = DB.Error;
                return result;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }

        //======================================================
        // SignUp
        // =====================================================
        private UserLogged SignUp(AbsRec absRec, PasswordsRec password)
        {
            bool result = true;
            long ID = 0;
            string Name = string.Empty;

            if (!ViewModel.DB.OpenDatabase())
            {
                General.message = General.ERR_CONNECT_DB;
                return null;
            }

            DB.BeginTransaction();

            if (password.Type == AdminType())
            {
                AdminsDB adminsDB = new AdminsDB();
                if (adminsDB.Insert(absRec))
                {
                    ((AdminsRec)absRec).ID = DB.GetLastKey();
                    ID = ((AdminsRec)absRec).ID;
                    Name = ((AdminsRec)absRec).Name;
                }
                else
                {
                    DB.RollbackTransaction();
                    result = false;
                }
            }
            else if (password.Type == UserType())
            {
                UsersDB usersDB = new UsersDB();
                if (usersDB.Insert(absRec))
                {
                    ((UsersRec)absRec).ID = DB.GetLastKey();
                    ID = ((UsersRec)absRec).ID;
                    Name = ((UsersRec)absRec).Name;
                }
                else
                {
                    DB.RollbackTransaction();
                    result = false;
                }
            }
            if (result)
            {
                PasswordsDB passwordsDB = new PasswordsDB();
                if (!passwordsDB.Insert(password))
                {
                    DB.RollbackTransaction();
                    result = false;
                }
            }

            DB.CommitTransaction();

            ViewModel.DB.CloseDatabase();
            General.message = DB.Error;

            if (!result) return null;

            return new UserLogged(password.Username, password.Password, password.Type, ID, Name);
        }
    }
}
