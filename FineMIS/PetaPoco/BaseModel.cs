/************************************************************************************************************
 * 
 * the extensions for PetaPoco
 *       
 * v1.0.0    updated by wz @2016-03-23
 *
 * todo
 *           add IsNew and Save
 *           add TrackModifiedColumns
 * 
 ************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Web;
using FineMIS;

namespace PetaPoco
{
    /// <summary>
    /// base model
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Database object
        /// </summary>
        protected static Repository Db => Repository.Instance;

        protected static string CurrentUser => Current.UserName;

        protected static DateTime CurrentTime => DateTime.Now;

        protected static long UserIdBelongTo => Current.UserBelongTo;

        protected static long CmpyIdBelongTo => Current.CmpyBelongTo;

        public static void BeginTransaction()
        {
            Db.BeginTransaction();
        }

        public static void CompleteTransaction()
        {
            Db.CompleteTransaction();
        }

        public static void AbortTransaction()
        {
            Db.AbortTransaction();
        }
    }

    /// <summary>
    /// a base model inherited by all entities
    /// </summary>
    [Serializable]
    public abstract class BaseModel<T, TKey> : BaseModel
        where T : BaseModel<T, TKey>
    {
        #region fields

        [Ignore]
        public virtual TKey Id { get; set; }

        [Ignore]
        public virtual string CreateBy { get; set; }

        [Ignore]
        public virtual DateTime CreateDate { get; set; }

        [Ignore]
        public virtual string LastUpdateBy { get; set; }

        [Ignore]
        public virtual DateTime LastUpdateDate { get; set; }

        [Ignore]
        public virtual bool Active { get; set; }

        [Ignore]
        public virtual long UserBelongTo { get; set; }

        [Ignore]
        public virtual long CmpyBelongTo { get; set; }

        #endregion

        #region instance methods

        /// <summary>
        /// insert this
        /// </summary>
        public virtual TKey Insert()
        {
            CreateBy = CurrentUser;
            CreateDate = CurrentTime;
            LastUpdateBy = CurrentUser;
            LastUpdateDate = CurrentTime;
            Active = true;
            UserBelongTo = UserIdBelongTo;
            CmpyBelongTo = CmpyIdBelongTo;
            var id = Db.Insert(this);
            Id = (TKey)id;
            return Id;
        }

        /// <summary>
        /// update this
        /// </summary>
        public virtual int Update()
        {
            LastUpdateBy = CurrentUser;
            LastUpdateDate = CurrentTime;
            Active = true;

            return Db.Update(this);
        }

        /// <summary>
        /// update this by one specific column
        /// </summary>
        public virtual int Update(string column)
        {
            return Update(new[] { column });
        }

        /// <summary>
        /// update this by specific columns
        /// </summary>
        public virtual int Update(IEnumerable<string> columns)
        {
            var more = new List<string>();
            more.AddRange(columns);
            if (!more.Contains("LastUpdateBy"))
            {
                LastUpdateBy = CurrentUser;
                more.Add("LastUpdateBy");
            }
            if (!more.Contains("LastUpdateDate"))
            {
                LastUpdateDate = CurrentTime;
                more.Add("LastUpdateDate");
            }
            if (!more.Contains("Active"))
            {
                Active = true;
                more.Add("Active");
            }

            return Db.Update(this, more);
        }

        /// <summary>
        /// delete this
        /// </summary>
        public virtual int Delete(bool direct = false)
        {
            return direct ? DeleteA() : DeleteB();
        }

        /// <summary>
        /// direct delete it
        /// </summary>
        private int DeleteA()
        {
            return Db.Delete(this);
        }

        /// <summary>
        /// deactive it
        /// </summary>
        private int DeleteB()
        {
            Active = false;
            // only update one field
            return Update("Active");
        }

        #endregion

        #region static read

        public static List<T> Fetch(Sql sql = null)
        {
            if (sql == null) sql = Sql.Builder;
            return Db.Fetch<T>(sql);
        }

        public static List<T> Fetch(string sql, params object[] args)
        {
            return Db.Fetch<T>(sql, args);
        }

        public static IEnumerable<T> Query(Sql sql = null)
        {
            if (sql == null) sql = Sql.Builder;
            return Db.Query<T>(sql);
        }

        public static IEnumerable<T> Query(string sql, params object[] args)
        {
            return Db.Query<T>(sql, args);
        }

        public static List<T> Fetch(long page, long itemsPerPage, Sql sql = null)
        {
            if (sql == null) sql = Sql.Builder;
            return Db.Fetch<T>(page, itemsPerPage, sql);
        }

        public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args)
        {
            return Db.Fetch<T>(page, itemsPerPage, sql, args);
        }

        public static Page<T> Page(long page, long itemsPerPage, Sql sql = null)
        {
            if (sql == null) sql = Sql.Builder;
            return Db.Page<T>(page, itemsPerPage, sql);
        }

        public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args)
        {
            return Db.Page<T>(page, itemsPerPage, sql, args);
        }

        public static List<T> SkipTake(long skip, long take, Sql sql = null)
        {
            if (sql == null) sql = Sql.Builder;
            return Db.SkipTake<T>(skip, take, sql);
        }

        public static List<T> SkipTake(long skip, long take, string sql, params object[] args)
        {
            return Db.SkipTake<T>(skip, take, sql, args);
        }

        public static T Single(object primaryKey)
        {
            return Db.Single<T>(primaryKey);
        }

        public static T SingleOrDefault(object primaryKey)
        {
            return Db.SingleOrDefault<T>(primaryKey);
        }

        public static T Single(Sql sql)
        {
            return Db.Single<T>(sql);
        }

        public static T SingleOrDefault(Sql sql)
        {
            return Db.SingleOrDefault<T>(sql);
        }

        public static T Single(string sql, params object[] args)
        {
            return Db.Single<T>(sql, args);
        }

        public static T SingleOrDefault(string sql, params object[] args)
        {
            return Db.SingleOrDefault<T>(sql, args);
        }

        public static T First(Sql sql)
        {
            return Db.First<T>(sql);
        }

        public static T FirstOrDefault(Sql sql)
        {
            return Db.FirstOrDefault<T>(sql);
        }

        public static T First(string sql, params object[] args)
        {
            return Db.First<T>(sql, args);
        }

        public static T FirstOrDefault(string sql, params object[] args)
        {
            return Db.FirstOrDefault<T>(sql, args);
        }

        public static bool Exists(object primaryKey)
        {
            return Db.Exists<T>(primaryKey);
        }

        public static bool Exists(string sql, params object[] args)
        {
            return Db.Exists<T>(sql, args);
        }

        #endregion

        #region static write

        /// <summary>
        /// note that sql can only be from clause or condition clause
        /// use Set function to specify columns
        /// update clause must be omitted
        /// </summary>
        public static int Update(Sql sql)
        {
            return Db.Update<T>(
                Sql.Builder
                    .Set("LastUpdateBy = @0", CurrentUser)
                    .Set("LastUpdateDate = @0", CurrentTime)
                    .Set("Active = @0", 1)
                    .Append(sql.SQL, sql.Arguments)
                );
        }

        /// <summary>
        /// note that sql can only be from clause or condition clause
        /// delete clause must be omitted
        /// </summary>
        public static int Delete(Sql sql, bool direct = false)
        {
            return direct ? DeleteA(sql) : DeleteB(sql);
        }

        private static int DeleteA(Sql sql)
        {
            return Db.Delete<T>(sql);
        }

        private static int DeleteB(Sql sql)
        {
            return Db.Update<T>(
                Sql.Builder
                    .Set("LastUpdateBy = @0", CurrentUser)
                    .Set("LastUpdateDate = @0", CurrentTime)
                    .Set("Active = @0", 0)
                    .Append(sql)
                );
        }

        public static int Delete(object primaryKey, bool direct = false)
        {
            return SingleOrDefault(primaryKey)?.Delete(direct) ?? 0;
        }

        #endregion
    }
}