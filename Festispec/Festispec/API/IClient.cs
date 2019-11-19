using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API
{
    public interface IClient
    {
        /// <summary>
        /// Voert een request uit.
        /// </summary>
        /// <param name="endpoint">Het eindpunt van de request.</param>
        /// <param name="method">De methode van de request.</param>
        /// <param name="urlSegments">Onderdelen van de URL.</param>
        /// <param name="parameters">Parameters welke gebruikt moeten worden.</param>
        /// <param name="headers">Headers welke gebruikt moeten worden.</param>
        /// <param name="files">Bestanden welke gebruikt moeten worden.</param>
        /// <returns>De response string van de request.</returns>
        string Request(string endpoint, Method method, Dictionary<string, string> urlSegments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, FileData[] files = null);

        /// <summary>
        /// Voert een request uit en stuurt een object terug.
        /// </summary>
        /// <typeparam name="T">Het type van het response object.</typeparam>
        /// <param name="endpoint">Het eindpunt van de request.</param>
        /// <param name="method">De methode van de request.</param>
        /// <param name="urlSegments">Onderdelen van de URL.</param>
        /// <param name="parameters">Parameters welke gebruikt moeten worden.</param>
        /// <param name="headers">Headers welke gebruikt moeten worden.</param>
        /// <param name="files">Bestanden welke gebruikt moeten worden.</param>
        /// <returns>Het response object van de request.</returns>
        T RequestAsJsonObject<T>(string endpoint, Method method, Dictionary<string, string> urlSegments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, FileData[] files = null);

        /// <summary>
        /// Voert een request uit en stuurt een object terug gehaald uit een property van de basis response.
        /// </summary>
        /// <typeparam name="T">Het type van het response object.</typeparam>
        /// <param name="endpoint">Het eindpunt van de request.</param>
        /// <param name="method">De methode van de request.</param>
        /// <param name="key">De property welke gebruikt moet worden.</param>
        /// <param name="urlSegments">Onderdelen van de URL.</param>
        /// <param name="parameters">Parameters welke gebruikt moeten worden.</param>
        /// <param name="headers">Headers welke gebruikt moeten worden.</param>
        /// <param name="files">Bestanden welke gebruikt moeten worden.</param>
        /// <returns>De response object van de request.</returns>
        T RequestAsJsonObjectFromValue<T>(string endpoint, Method method, string key, Dictionary<string, string> urlSegments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, FileData[] files = null);
    }
}
