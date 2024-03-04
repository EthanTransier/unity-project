using UnityEngine;
using MongoDB.Driver;
using System.IO;

public class SendJSONToMongoDB : MonoBehaviour
{
    // MongoDB connection string
    private string connectionString = "mongodb://localhost:27017";

    // Database name and collection name
    private string databaseName = "YourDatabaseName";
    private string collectionName = "YourCollectionName";

    void Start()
    {
        // Read the JSON file
        string jsonFilePath = "path/to/your/json/file.json";
        string jsonText = File.ReadAllText(jsonFilePath);

        // Connect to MongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        var collection = database.GetCollection<BsonDocument>(collectionName);

        // Parse JSON string to BsonDocument
        var document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(jsonText);

        // Insert document into MongoDB collection
        collection.InsertOne(document);

        Debug.Log("JSON data inserted into MongoDB.");
    }
}
