using MyCompany.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCompany.Data
{
    public static class MongoCollections
    {
        public const string TestCollections = nameof(TestCollections);

        public static readonly ReadOnlyDictionary<Type, string> TypeCollectionMap = new ReadOnlyDictionary<Type, string>(
            new Dictionary<Type, string>
            {
                { typeof(TestCollection), TestCollections }
            });
    }
}
