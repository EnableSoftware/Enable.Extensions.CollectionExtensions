using System;
using System.Collections.Generic;
using AutoFixture;
using Xunit;

namespace Enable.Extensions
{
    public class CollectionExtensionsBatchTests
    {
        [Fact]
        public void Batch_ThrowsIfSourceIsNull()
        {
            // Arrange
            ICollection<int> source = null;

            // Act
            var action = new Action(() =>
            {
                source.Batch(1);
            });

            // Assert
            Assert.Throws(typeof(ArgumentNullException), action);
        }

        [Fact]
        public void Batch_ThrowsIfBatchSizeIsZero()
        {
            // Arrange
            ICollection<int> source = new List<int>();

            // Act
            var action = new Action(() =>
            {
                source.Batch(0);
            });

            // Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException), action);
        }

        [Theory]
        [InlineData(0, 0, 5)]
        [InlineData(0, 0, 10)]
        [InlineData(2, 6, 5)]
        [InlineData(2, 10, 5)]
        [InlineData(1, 6, 6)]
        [InlineData(8, 400, 50)]
        [InlineData(9, 401, 50)]
        public void Batch_ReturnsBatchedList(int expectedBatchCount, int sourceCount, int batchSize)
        {
            // Arrange
            var fixture = new Fixture();

            ICollection<int> source = new List<int>();

            for (var i = 0; i < sourceCount; i++)
            {
                source.Add(fixture.Create<int>());
            }

            // Act
            var batches = source.Batch(batchSize);

            // Assert
            Assert.IsType<List<List<int>>>(batches);
            Assert.Equal(expectedBatchCount, batches.Count);
        }

        [Fact]
        public void Batch_ReturnsExpectedValues()
        {
            // Arrange
            var fixture = new Fixture();

            ICollection<int> source = new List<int>();

            var v1 = fixture.Create<int>();
            var v2 = fixture.Create<int>();
            var v3 = fixture.Create<int>();
            var v4 = fixture.Create<int>();
            var v5 = fixture.Create<int>();

            source.Add(v1);
            source.Add(v2);
            source.Add(v3);
            source.Add(v4);
            source.Add(v5);

            // Act
            var batches = source.Batch(2);

            // Assert
            Assert.IsType<List<List<int>>>(batches);
            Assert.Equal(3, batches.Count);
            Assert.Equal(2, batches[0].Count);
            Assert.Equal(2, batches[1].Count);
            Assert.Equal(1, batches[2].Count);
            Assert.Equal(v1, batches[0][0]);
            Assert.Equal(v2, batches[0][1]);
            Assert.Equal(v3, batches[1][0]);
            Assert.Equal(v4, batches[1][1]);
            Assert.Equal(v5, batches[2][0]);
        }
    }
}
