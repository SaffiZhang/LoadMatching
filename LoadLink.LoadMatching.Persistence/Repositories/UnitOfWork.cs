using LoadLink.LoadMatching.Application.Persistence;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Data;

namespace LoadLink.LoadMatching.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection = null;
        private Guid _id;


        internal UnitOfWork(IConnectionFactory connectionFactory)
        {
            _id = Guid.NewGuid();
            _connection = connectionFactory.GetConnection;
        }


        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }

        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }

        private IDbTransaction _transaction = null;
        IDbTransaction IUnitOfWork.Transaction => _transaction;

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null)
                    _transaction.Dispose();

                if (_connection != null)
                    _connection.Dispose();
            }
            _transaction = null;
            _connection = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);


        }

    }
}
