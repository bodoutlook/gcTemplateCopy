using ApiGraphActivator;
using ApiGraphActivator.Services;
using Markdig;
using Microsoft.Graph.Models.ExternalConnectors;

// Define a static class named ContentService
static class ContentService
{
  // Define a static list to hold ExternalItem objects
  static List<Object> content;

  // Define a static list to hold ExternalItem objects
  static List<ExternalItem> items = new();

  // Define an asynchronous static method named Extract
  async static Task Extract()
  {
    // Populate the content list with data from the External Service
    //content = await EdgarService.HydrateLookupData();
  }

  // Define a static method named Transform that takes an EdgarExternalItem as a parameter
  static void Transform(Object item)
  {
    // Create a new ExternalItem object and populate its properties
    ExternalItem exItem = new ExternalItem
    {};
      
  }

  // Define an asynchronous static method named Load
  static async Task Load()
  {
    // Iterate over each item in the items list
    foreach (var item in items)
    {
      // Output a message to the console indicating the start of the item loading process
      Console.Write(string.Format("Loading item {0}...", item.Id));
      try
      {
        // Await the asynchronous operation of putting the item into the GraphService client
        await GraphService.Client.External
          .Connections[Uri.EscapeDataString(ConnectionConfiguration.ExternalConnection.Id!)]
          .Items[item.Id]
          .PutAsync(item);
        // Output a message to the console indicating the completion of the item loading process
        Console.WriteLine("DONE");

        // Get the URL from the item's AdditionalData dictionary
        string url = (string)item.AdditionalData["Url"];

      }
      catch (Exception ex)
      {
        // Output an error message to the console if an exception occurs
        Console.WriteLine("ERROR");
        Console.WriteLine(ex.Message);
      }
    }
  }

  // Define a public asynchronous static method named LoadContent
  public static async Task LoadContent()
  {
    // Call the Extract method to populate the content list
    await Extract();

    // Iterate over each item in the content list and transform it
    foreach (var item in content)
    {
      Transform(item);
    }

    // Call the Load method to load the transformed items
    await Load();
  }
}