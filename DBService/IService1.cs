using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Message();
        [OperationContract]
        string AdminType();
        [OperationContract]
        string UserType();
        [OperationContract]
        string doSelect();
        [OperationContract]
        string doInsert();
        [OperationContract]
        string doUpdate();
        [OperationContract]
        string doDelete();

        //======================================================
        // Login
        // =====================================================
        [OperationContract]
        UserLogged Login(string Username, string Password);
        //======================================================
        // Admins
        // =====================================================
        [OperationContract]
        AdminsList AdminsSelectAll();
        [OperationContract]
        AdminsRec AdminsSelect(long id);
        [OperationContract]
        bool AdminsInsert(AdminsRec admin);
        [OperationContract]
        bool AdminsUpdate(AdminsRec admin);
        [OperationContract]
        bool AdminsDelete(long id);
        [OperationContract]
        UserLogged AdminsInsertWithPassword(AdminsRec admin, PasswordsRec password);


        //======================================================
        // Drugs
        // =====================================================
        [OperationContract]
        DrugsList DrugsSelectAll();
        [OperationContract]
        DrugsRec DrugsSelect(string Code);
        [OperationContract]
        bool DrugsInsert(DrugsRec drug);
        [OperationContract]
        bool DrugsUpdate(AdminsRec drug);
        [OperationContract]
        bool DrugsDelete(string Code);


        //======================================================
        // DrugTypes
        // =====================================================
        [OperationContract]
        DrugTypesList DrugTypesSelectAll();
        [OperationContract]
        DrugTypesRec DrugTypesSelect(string Code);
        [OperationContract]
        bool DrugTypesInsert(DrugTypesRec drugtype);
        [OperationContract]
        bool DrugTypesUpdate(DrugTypesRec drugtype);
        [OperationContract]
        bool DrugTypesDelete(string Code);


        //======================================================
        // Users
        // =====================================================
        [OperationContract]
        UsersList UsersSelectAll();
        [OperationContract]
        UsersRec UsersSelect(long ID);
        [OperationContract]
        bool UsersInsert(UsersRec user);
        [OperationContract]
        bool UsersUpdate(UsersRec user);
        [OperationContract]
        bool UsersDelete(long ID);
        [OperationContract]
        UserLogged UsersInsertWithPassword(UsersRec user, PasswordsRec password);



        //======================================================
        // Passwords
        // =====================================================
        [OperationContract]
        PasswordsList PasswordsSelectAll();
        [OperationContract]
        PasswordsRec PasswordsSelect(string Username);
        [OperationContract]
        bool PasswordsInsert(PasswordsRec password);
        [OperationContract]
        bool PasswordsUpdate(PasswordsRec password);
        [OperationContract]
        bool PasswordsDelete(string Username);



        //======================================================
        // UsersDrugs
        // =====================================================
        [OperationContract]
        UsersDrugsList UsersDrugsSelectAll();
        [OperationContract]
        UsersDrugsRec UsersDrugsSelect(int UserID, string DrugCode);
        [OperationContract]
        bool UsersDrugsInsert(UsersDrugsRec userdrug);
        [OperationContract]
        bool UsersDrugsUpdate(UsersDrugsRec userdrug);
        [OperationContract]
        bool UsersDrugsDelete(int UserID, string DrugCode);
    }
}
