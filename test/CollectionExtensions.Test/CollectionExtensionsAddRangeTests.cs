using System;
using System.Collections.Generic;
using Ploeh.AutoFixture;
using Xunit;

namespace Enable.Extensions
{
    public class CollectionExtensionsAddRangeTests
    {
        [Fact]
        public void AddRange_ThrowsIfSourceIsNull()
        {
            // Arrange
            ICollection<int> source = null;
            ICollection<int> collection = new List<int>();

            // Act
            var action = new Action(() =>
            {
                source.AddRange(collection);
            });

            // Assert
            Assert.Throws(typeof(ArgumentNullException), action);
        }

        [Fact]
        public void AddRange_ThrowsIfCollectionIsNull()
        {
            // Arrange
            ICollection<int> source = new List<int>();
            ICollection<int> collection = null;

            // Act
            var action = new Action(() =>
            {
                source.AddRange(collection);
            });

            // Assert
            Assert.Throws(typeof(ArgumentNullException), action);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 0)]
        [InlineData(0, 5)]
        [InlineData(25, 50)]
        public void AddRange_AddsCollectionToSource(int sourceCount, int collectionCount)
        {
            // Arrange
            var fixture = new Fixture();

            ICollection<int> source = new List<int>();
            ICollection<int> collection = new List<int>();

            for (var i = 0; i < sourceCount; i++)
            {
                source.Add(fixture.Create<int>());
            }

            for (var i = 0; i < collectionCount; i++)
            {
                collection.Add(fixture.Create<int>());
            }

            var preActCount = source.Count;

            // Act
            source.AddRange(collection);

            // Assert
            Assert.Equal(sourceCount, preActCount);
            Assert.Equal(sourceCount + collectionCount, source.Count);
        }
    }
}
