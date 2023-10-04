﻿using InterviewService.Models;
using InterviewService.MongoDbSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace InterviewService.DAL
{
    public interface IFeedbackDAL
    {
        List<Feedback> GetAllFeedback();
        void CreateFeedback(Feedback feedback);
    }

    public class FeedbackDAL : IFeedbackDAL
    {
        private readonly IMongoCollection<Feedback> _feedbacks;

        public FeedbackDAL(IOptions<InterviewDBSettings> settings)
        {
            //var client = new MongoClient(settings.Value.ConnectionString);
            //var database = client.GetDatabase(settings.Value.DatabaseName);
            //_feedbacks = database.GetCollection<Feedback>(settings.Value.FeedbackCollectionName);
            string connectionString =
     @"mongodb://interviewaccount:KpnsJlvBv78Ibq1mMNWd0niUQN82afATFfozvnTksOLuIfEssstFdxOg7TZc66bZXVGuGSL8vVa0ACDb0ndgJg==@interviewaccount.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@interviewaccount@";
            MongoClientSettings settings1 = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings1.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings1);
            var database = mongoClient.GetDatabase("InterviewDb");//.Value.DatabaseName);
            _feedbacks = database.GetCollection<Feedback>("Feedback");

        }

        public List<Feedback> GetAllFeedback()
        {
            return _feedbacks.Find(_ => true).ToList();
        }

        public void CreateFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            // If you want to generate a unique ID for Feedback, you can use a GUID:
           /* feedback.FeedbackID = Guid.NewGuid().ToString();*/ // Generate a new unique ID

            _feedbacks.InsertOne(feedback);
        }
    }
}
