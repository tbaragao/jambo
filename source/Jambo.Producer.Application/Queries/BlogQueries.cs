﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Jambo.Producer.Application.Queries
{
    public class BlogQueries : IBlogQueries
    {
        private readonly IMongoDatabase database;
        public IMongoCollection<ExpandoObject> Blogs
        {
            get
            {
                return database.GetCollection<ExpandoObject>("Blogs");
            }
        }

        public BlogQueries(string connectionString, string database)
        {
            MongoClient mongoClient = new MongoClient(connectionString);
            this.database = mongoClient.GetDatabase(database);
        }

        public async Task<IEnumerable<ExpandoObject>> GetBlogsAsync()
        {
            return await Blogs.Find(e => true).ToListAsync();
        }

        public async Task<ExpandoObject> GetBlogAsync(Guid id)
        {
            return await Blogs.Find(Builders<ExpandoObject>.Filter.Eq("_id", id)).SingleAsync();
        }
    }
}
