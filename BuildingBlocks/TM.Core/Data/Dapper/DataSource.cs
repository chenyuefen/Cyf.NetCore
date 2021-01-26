using System;

namespace TM.Core.Data.Dapper
{
    public class DataSource : IEquatable<DataSource>
    {
        internal DataSource(IDbProvider provider, string readingConnectionName, string writingConnectionName)
        {
            this.ReadingConnectionName = readingConnectionName;
            this.WritingConnectionName = writingConnectionName;
            this.DatabaseProvider = provider;
        }

        public IDbProvider DatabaseProvider { get; }

        public string ReadingConnectionName { get; }

        public string WritingConnectionName { get; }

        public bool Equals(DataSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(DatabaseProvider, other.DatabaseProvider) && string.Equals(ReadingConnectionName, other.ReadingConnectionName) && string.Equals(WritingConnectionName, other.WritingConnectionName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DataSource)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DatabaseProvider != null ? DatabaseProvider.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReadingConnectionName != null ? ReadingConnectionName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (WritingConnectionName != null ? WritingConnectionName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
