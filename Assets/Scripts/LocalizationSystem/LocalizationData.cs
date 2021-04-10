[System.Serializable]
public struct LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public struct LocalizationItem
{
    public string key;
    public string value;    
}