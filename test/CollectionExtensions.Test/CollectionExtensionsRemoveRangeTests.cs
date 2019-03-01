using System;
using System.Collections.Generic;
using AutoFixture;
using Xunit;

namespace Enable.Extensions
{
    public class CollectionExtensionsRemoveRangeTests
    {
        [Fact]
        public void RemoveRange_ThrowsIfSourceIsNull()
        {
            // Arrange
            ICollection<int> source = null;
            ICollection<int> collection = new List<int>();

            // Act
            var action = new Action(() =>
            {
                source.RemoveRange(collection);
            });

            // Assert
            Assert.Throws(typeof(ArgumentNullException), action);
        }

        [Fact]
        public void RemoveRange_ThrowsIfCollectionIsNull()
        {
            // Arrange
            ICollection<int> source = new List<int>();
            ICollection<int> collection = null;

            // Act
            var action = new Action(() =>
            {
                source.RemoveRange(collection);
            });

            // Assert
            Assert.Throws(typeof(ArgumentNullException), action);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 0)]
        [InlineData(0, 5)]
        [InlineData(25, 50)]
        public void RemoveRange_RemovesCollectionFromSource(int sourceCount, int collectionCount)
        {
            // Arrange
            var fixture = new Fixture();

            var source = new List<int>();
            var collection = new List<int>();

            for (var i = 0; i < sourceCount + collectionCount; i++)
            {
                source.Add(fixture.Create<int>());
            }

            for (var i = 0; i < collectionCount; i++)
            {
                var item = source[i];
                collection.Add(item);
            }

            var preActCount = source.Count;

            // Act
            source.RemoveRange(collection);

            // Assert
            Assert.Equal(sourceCount + collectionCount, preActCount);
            Assert.Equal(sourceCount, source.Count);
        }
    }
}
