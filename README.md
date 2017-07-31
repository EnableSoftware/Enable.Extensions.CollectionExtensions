# Enable.Extensions.CollectionExtensions

C# extensions for modifying collections.

[![Build status](https://ci.appveyor.com/api/projects/status/176mkjf3po8l2sw3?svg=true)](https://ci.appveyor.com/project/EnableSoftware/enable-extensions-collectionextensions)

The AddRange() method takes an IEnumerable<T> collection and adds the items in the collection to the source ICollection<T>.

```c#
var source = new HashSet<int>();
var collection = new HashSet<int>();

source.AddRange(collection);
```

The RemoveRange() method takes an IEnumerable<T> collection and removes the items in the collection to the source ICollection<T>.

```c#
var source = new HashSet<int>();
var collection = new HashSet<int>();

source.RemoveRange(collection);
```

The Batch() method takes an int batchSize variable and returns a list of lists (List<List<T>>) with the specified batch size.

```c#
var source = new HashSet<int>();
var batchSize = 5;

var batches = source.Batch(batchSize);
```