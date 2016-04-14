using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "IPAddress"
    /// </summary>
    public interface IIPAddressRepo : IRepo<IPAddress>
    {
        /// <summary>
        /// Получает IP адрес по значению
        /// </summary>
        /// <param name="ipAddress">IP адрес</param>
        /// <returns></returns>
        IPAddress GetByIPAddress(String ipAddress);
    }
}