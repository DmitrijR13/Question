using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "User"
    /// </summary>
    public interface IUserRepo : IRepo<User>
    {
        /// <summary>
        /// Получить пользователя по email и паролю
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        User GetByEmail(String email, Guid password);
    }
}