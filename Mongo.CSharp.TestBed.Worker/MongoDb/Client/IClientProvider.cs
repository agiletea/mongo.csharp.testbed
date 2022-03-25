﻿using MongoDB.Driver;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Client
{
        /// <summary>
        /// Allows access to the Mongo Db Driver Client and Database in an abstracted manner.
        /// </summary>
        internal interface IClientProvider : IDisposable
        {
            /// <summary>
            /// Return the Mongo Client.
            /// </summary>
            IMongoDbClient Client { get; }

            /// <summary>
            /// Returns the Database as per the options.
            /// </summary>
            IMongoDatabase Database { get; }
        }
}