# About

Common classes

| BaseExceptionProperties  | |
| :--- | :--- |
| HasException | Indicate the last operation thrown an exception or not. |
| IsSuccessFul | Indicates success or failure for last method call |
|mLastException| Used to set LastException |
| LastException | Provides access to the last exception thrown |
| LastExceptionMessage | Last exception message |
| InnerExceptions | Last exception Inner exception |



| CommandProvider  |
| :--- | 
| This extension is good for viewing a SQL statement (SELECT, DELETE, UPDATE, INSERT) that use managed data provider parameters to actual see values that will be passed to the database. Has been tested with SQL-Server using @ as a parameter marker, Oracle with : as the parameter marker and MS-Access using @ as a parameter marker.|

| Extensions for CommandProvider  | |
| :--- | :--- |
| ActualCommandText | Used to show an SQL statement with actual values for debugging or logging to a file.  |

**Command provider enums**

```csharp
public enum CommandProvider
{
    SqlServer,
    Access,
    SQLite,
    Oracle
}
```
**Latitude/Longitude validating extensions**

```csharp
public static class GeoExtensions
{
    /// <summary>
    /// Validate sender is a valid latitude
    /// </summary>
    /// <param name="value">double value to validate</param>
    /// <returns>true if valid, false if not valid</returns>
    public static bool IsLatitude(this double value) 
        => value  <= 90.0 &&  value >= -90.0;

    /// <summary>
    /// Validate sender is a valid longitude
    /// </summary>
    /// <param name="value">double value to validate</param>
    /// <returns>true if valid, false if not valid</returns>
    public static bool IsLongitude(this double value) 
        => value  <= 180.0 && value >= -180.0;
}
```

**DirectoryHelper** contains methods to walk up a directory tree

```csharp
public static class DirectoryHelper
{
    public static string UpperFolder(this string folderName, int level)
    {
        var folderList = new List<string>();

        while (!string.IsNullOrWhiteSpace(folderName))
        {
            var parentFolder = Directory.GetParent(folderName);

            if (parentFolder == null) break;

            folderName = Directory.GetParent(folderName)?.FullName;
            folderList.Add(folderName);
        }

        return folderList.Count > 0 && level > 0 ? level - 1 <= folderList.Count - 1 ? 
            folderList[level - 1] : folderName : folderName;
    }

    /// <summary>
    /// Get solution folder path from a project directly beneath the
    /// current solution folder
    /// </summary>
    public static string SolutionFolder() 
        => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4);
}
```


