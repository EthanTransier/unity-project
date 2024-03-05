using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.IO;
using System.Collections.Generic;

public class GetLeaderboard : MonoBehaviour
{
    // MongoDB connection string
    private string connectionString = "mongodb+srv://etrans914:LAD80lJ6l3l0kaiF@unity.ls5afvg.mongodb.net/?retryWrites=true&w=majority&appName=unity";

    // Database name and collection name
    private string databaseName = "unity";
    private string collectionName = "leaderboard";

    // File path to save JSON file
    private string jsonFilePath = "../leaderboard.json";

    // Class for leaderboard data
    private class LeaderboardData
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
        // public int Id { get; set; }
        public string Deaths { get; set; }
    }

    void Start()
    {
        // Connect to MongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        var collection = database.GetCollection<LeaderboardData>(collectionName);

        // Retrieve all documents from the collection
        var documents = collection.Find(new BsonDocument()).ToList();

        // Convert ObjectId to string
        foreach (var doc in documents)
        {
            doc.Id = doc.Id.ToString();
        }

        // Serialize documents to JSON format
        var jsonText = documents.ToJson();

        // Write JSON data to a file
        File.WriteAllText(jsonFilePath, jsonText);

        Debug.Log("Leaderboard data retrieved and saved to JSON file.");
    }
}
