/************************************************************************************************************
 * 
 * the extensions for PetaPoco
 *       
 * v1.0.0    updated by wz @2016-03-23
 * 
 ************************************************************************************************************/

using System;
using System.Data;
using System.Data.Common;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace PetaPoco
{
    /// <summary>
    ///  a thread safe and automatic maintain transaction internally database
    /// </summary>
    public partial class Repository : Database
    {
        [ThreadStatic]
        private static Repository _instance;

        public Repository()
            : base("connectionString")
        {
            _Contruct();
        }

        public Repository(string connectionStringName)
            : base(connectionStringName)
        {
            _Contruct();
        }

        partial void _Contruct();

        public interface IFactory
        {
            Repository GetInstance();
        }

        public static IFactory Factory { get; set; }

        public static Repository Instance => _instance ?? (Factory != null ? Factory.GetInstance() : new Repository());

        public override void OnBeginTransaction()
        {
            if (_instance == null)
                _instance = this;

            base.OnBeginTransaction();
        }

        public override void OnEndTransaction()
        {
            if (_instance == null)
                _instance = this;

            base.OnEndTransaction();
        }

        public override void OnExecutingCommand(IDbCommand cmd)
        {
            base.OnExecutingCommand(cmd);
        }

        public override void OnExecutedCommand(IDbCommand cmd)
        {
            base.OnExecutedCommand(cmd);
        }

        public override IDbConnection OnConnectionOpened(IDbConnection conn)
        {
            return new ProfiledDbConnection((DbConnection)conn, MiniProfiler.Current);
        }

        public override void OnConnectionClosing(IDbConnection conn)
        {
            base.OnConnectionClosing(conn);
        }

        public override bool OnException(Exception x)
        {
            return base.OnException(x);
        }
    }
}
