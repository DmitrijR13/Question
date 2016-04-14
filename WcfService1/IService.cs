using Questionnaire.Service.Models;
using Questionnaire.Service.Setting.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Questionnaire.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        #region методы меню IP организации

        /// <summary>
        /// Получить все ip адреса
        /// </summary>
        [OperationContract]
        List<IPAddressSM> GetAllIPAddress(GridOptions options);

        /// <summary>
        /// Получить количество ip адресов
        /// </summary>
        [OperationContract]
        Int32 GetCountIPAddress(GridOptions options);

        /// <summary>
        /// Открывает окно редактирования ip адреса организации
        /// </summary>
        /// <param name="ipAddressID">ID ip адреса</param>
        /// <returns></returns>
        [OperationContract]
        IPAddressSM GetIPAddress(Int32 ipAddressID);

        /// <summary>
        /// Сохраняет данные редактирования ip адреса
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SetIPAddress(IPAddressSM model);

        /// <summary>
        /// Удаляет ip адрес организации
        /// </summary>
        /// <param name="ipAddressID"></param>
        [OperationContract]
        void DeleteIPAddress(Int32 ipAddressID);

        #endregion методы меню IP организации

        #region методы меню Роли

        /// <summary>
        /// Получить все роли в проекте
        /// </summary>
        [OperationContract]
        List<RoleSM> GetAllRole(GridOptions options);

        /// <summary>
        /// Получить количество ролей в проекте
        /// </summary>
        [OperationContract]
        Int32 GetCountRole(GridOptions options);

        /// <summary>
        /// Получить все роли в проекте
        /// </summary>
        [OperationContract]
        List<RoleSM> GetSimpleAllRole();

        /// <summary>
        /// Получить количество ролей в проекте
        /// </summary>
        [OperationContract]
        Int32 GetSimpleCountRole();

        /// <summary>
        /// Открывает окно редактирования роли
        /// </summary>
        /// <param name="roleID">ID роли</param>
        /// <returns></returns>
        [OperationContract]
        RoleSM GetRole(Int32 roleID);

        /// <summary>
        /// Сохраняет роль
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SetRole(RoleSM model);
        
        #endregion методы меню Роли

        #region методы меню Пользователи

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        [OperationContract]
        List<UserSM> GetAllUser(GridOptions options);

        /// <summary>
        /// Получить количество всех пользователей
        /// </summary>
        [OperationContract]
        Int32 GetCountUser(GridOptions options);

        /// <summary>
        /// Открывает окно редактирования пользователя
        /// </summary>
        /// <param name="questionID">ID пользователя</param>
        /// <returns></returns>
        [OperationContract]
        UserSM GetUser(Int32 userID);

        /// <summary>
        /// Получает данные редактирования пользователя
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SetUser(UserSM model);

        #endregion методы меню Пользователи

        #region методы меню паспорта МО

        /// <summary>
        /// Получить все паспорта МО
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PassportMOSM> GetAllPassportMO(GridOptions options);

        /// <summary>
        /// Получить каоличество всех паспортов МО
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Int32 GetCountPassportMO(GridOptions options);

        #endregion

        #region методы меню паспорта СМО

        /// <summary>
        /// Получить все паспорта СМО
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PassportSMOSM> GetAllPassportSMO(GridOptions options);

        /// <summary>
        /// Получить каоличество всех паспортов СМО
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Int32 GetCountPassportSMO(GridOptions options);

        #endregion

        #region Вопросы

        /// <summary>
        /// Получить все вопросы данной категории
        /// </summary>
        [OperationContract]
        List<QuestionSM> GetAllQuestionByCharter(GridOptions options, Int32 charterID);

        /// <summary>
        /// Открывает окно редактирования вопроса
        /// </summary>
        /// <param name="questionID">ID вопроса</param>
        /// <returns></returns>
        [OperationContract]
        QuestionSM GetQuestion(Int32 questionID, Int32 charterID);
        /// <summary>
        /// Получает данные редактирования вопроса
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SetQuestion(QuestionSM model);

        /// <summary>
        /// Удалить вопрос
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void DeleteQuestion(Int32 questionID, Int32 charterID);

        #endregion
    }
}
