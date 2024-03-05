using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.IO;

public class SendJSONToMongoDB : MonoBehaviour
{
    // MongoDB connection string
    private string connectionString = "mongodb+srv://etrans914:LAD80lJ6l3l0kaiF@unity.ls5afvg.mongodb.net/?retryWrites=true&w=majority&appName=unity";

    // Database name and collection name
    private string databaseName = "unity";
    private string collectionName = "leaderboard";

    void Start()
    {
        // Read the JSON file
        string jsonFilePath = "./leaderboard.json";
        string jsonText = File.ReadAllText(jsonFilePath);

        // Convert JSON array to BsonArray
        var jsonArray = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonArray>(jsonText);

        // Connect to MongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        var collection = database.GetCollection<BsonDocument>(collectionName);

        // Insert each document from the JSON array into MongoDB collection
        foreach (BsonDocument document in jsonArray)
        {
            collection.InsertOne(document);
        }

        Debug.Log("JSON data inserted into MongoDB.");
    }
}
