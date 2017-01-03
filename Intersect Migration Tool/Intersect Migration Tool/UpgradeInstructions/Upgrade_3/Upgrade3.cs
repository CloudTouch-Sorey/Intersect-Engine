﻿using System;
using Mono.Data.Sqlite;

namespace Intersect_Migration_Tool.UpgradeInstructions.Upgrade_3
{
    public class Upgrade3
    {
        private SqliteConnection _dbConnection;
        private Object _dbLock = new Object();

        //Time of Day Table Constants
        private const string TIME_TABLE = "time";
        private const string TIME_DATA = "data";

        public Upgrade3(SqliteConnection connection)
        {
            _dbConnection = connection;
        }

        public void Upgrade()
        {
            //Upgrade Steps
            CreateTimeTable();
        }

        private void CreateTimeTable()
        {
            var cmd = "CREATE TABLE " + TIME_TABLE + " (" + TIME_DATA + " BLOB);";
            using (var createCommand = _dbConnection.CreateCommand())
            {
                createCommand.CommandText = cmd;
                createCommand.ExecuteNonQuery();
            }
            InsertTime();
        }
        private void InsertTime()
        {
            var cmd = "INSERT into " + TIME_TABLE + " (" + TIME_DATA + ") VALUES (" + "NULL" + ");";
            using (var createCommand = _dbConnection.CreateCommand())
            {
                createCommand.CommandText = cmd;
                createCommand.ExecuteNonQuery();
            }
        }
    }
}