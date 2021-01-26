using System.Data.Common;

namespace TM.Core.Data.Dapper
{
    /// <summary>
    ///     Represents a contract for a database type provider.
    /// </summary>
    public interface IDbProvider
    {
        DbProviderFactory GetFactory();
        /// <summary>
        ///     Returns the prefix used to delimit parameters in SQL query strings.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The providers character for prefixing a query parameter.</returns>
        string GetParameterPrefix(string connectionString);
    }
}
