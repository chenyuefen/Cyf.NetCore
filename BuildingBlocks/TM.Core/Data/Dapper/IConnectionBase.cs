using System;
using System.Data;
using TM.Core.Dependency;

namespace TM.Core.Data.Dapper
{
    public interface IConnectionBase : IDependency, IDisposable, ITransactionAccessor
    {
        /// <summary>
        ///     Gets the current <seealso cref="Provider" />.
        /// </summary>
        /// <returns>
        ///     The current database provider.
        /// </returns>
        IDbProvider Provider { get; }

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        /// <returns>
        ///     The connection string.
        /// </returns>
        string ConnectionString { get; }

        /// <summary>
        ///     Gets or sets the transaction isolation level.
        /// </summary>
        /// <remarks>
        ///     When value is null, the underlying providers default isolation level is used.
        /// </remarks>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        ///     Starts or continues a transaction.
        /// </summary>
        /// <returns>An ITransaction reference that must be Completed or disposed</returns>
        /// <remarks>
        ///     This method makes management of calls to Begin/End/CompleteTransaction easier.
        ///     The usage pattern for this should be:
        ///     using (var tx = db.GetTransaction())
        ///     {
        ///     // Do stuff
        ///     db.Update(...);
        ///     // Mark the transaction as complete
        ///     tx.Complete();
        ///     }
        ///     Transactions can be nested but they must all be completed otherwise the entire
        ///     transaction is aborted.
        /// </remarks>
        ITransaction GetTransaction();

        /// <summary>
        ///     Starts a transaction scope, see GetTransaction() for recommended usage
        /// </summary>
        void BeginTransaction();

        /// <summary>
        ///     Aborts the entire outer most transaction scope
        /// </summary>
        /// <remarks>
        ///     Called automatically by Transaction.Dispose()
        ///     if the transaction wasn't completed.
        /// </remarks>
        void AbortTransaction();

        /// <summary>
        ///     Marks the current transaction scope as complete.
        /// </summary>
        void CompleteTransaction();
    }
}
