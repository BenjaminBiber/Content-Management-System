using Newtonsoft.Json;

namespace CMS.Services;

public class AlbumItem : IEquatable<AlbumItem>
{
    public ImageItem[] pictures { get; set; }
    public string name { get; set; }
    
    public bool Equals(AlbumItem other)
    {
        //TODO: wäre echt cool wenn es nicht nur auf den Namen geht, da es ja vlt auch Alben mit gleichen Namen gibt, daher entweder beide alben zusammenpacken, oder vermeiden
        if (other is null) return false;
        return name == other.name;
    }
}

public class SingleImageItem : IEquatable<SingleImageItem>
{
    public string url { get; set; }
    public string description { get; set; }
    
    public bool Equals(SingleImageItem other)
    {
        if (other is null) return false;
        return url == other.url && description == other.description;
    }
}

public class ImageItem : IEquatable<ImageItem>
{
    public string url { get; set; }
    public string title { get; set; }
    public bool isBest { get; set; }
    
    public bool Equals(ImageItem other)
    {
        if (other is null) return false;
        return url == other.url && title == other.title && isBest == other.isBest;
    }
}

public class TextItem : IEquatable<TextItem>
{
    public string key { get; set; }
    public string value { get; set; }
    public string description { get; set; }
    
    public bool Equals(TextItem other)
    {
        if (other is null) return false;
        return key == other.key && value == other.value && description == other.description;
    }
}

public class JsonService
{
    public static T GetItems<T> (string fileName)
    {
        var json = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    public static void AddItem<T> (string fileName, T item)
    {
        var json = File.ReadAllText(fileName);
        var items = JsonConvert.DeserializeObject<List<T>>(json);
        items.Add(item);
        var writeData = JsonConvert.SerializeObject(items, Formatting.Indented);
        File.WriteAllText(fileName, writeData);
    }
    
    public static void RemoveItem<T> (string fileName, T item)
    {
        var json = File.ReadAllText(fileName);
        var items = JsonConvert.DeserializeObject<List<T>>(json);
        items.Remove(item);
        var writeData = JsonConvert.SerializeObject(items, Formatting.Indented);
        File.WriteAllText(fileName, writeData);
    }
    
    public static void UpdateItem<T> (string fileName, T oldItem, T newItem)
    {
        var json = File.ReadAllText(fileName);
        var items = JsonConvert.DeserializeObject<List<T>>(json);
        var index = items.IndexOf(oldItem);
        if (index != -1)
        {
            items[index] = newItem;
            var writeData = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(fileName, writeData);
        }
    }
    
    public static T DeepCopy<T>(T original)
    {
        var json = JsonConvert.SerializeObject(original);
        return JsonConvert.DeserializeObject<T>(json)!;
    }
}