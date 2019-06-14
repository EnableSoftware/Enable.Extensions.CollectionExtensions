using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Xunit;

namespace Enable.Extensions
{
    public class CollectionExtensionsDistinctByTests
    {
        private readonly Fixture _fixture;

        public CollectionExtensionsDistinctByTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void DistinctBy_ThrowsIfSourceIsNull()
        {
            // Arrange
            IEnumerable<Person> source = null;
            Func<Person, string> property = person => person.EmailAddress;

            // Act
            var action = new Action(() =>
            {
                source.DistinctBy(property);
            });

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void DistinctBy_ThrowsIfPropertyIsNull()
        {
            // Arrange
            IEnumerable<Person> source = _fixture.CreateMany<Person>();
            Func<Person, string> property = null;

            // Act
            var action = new Action(() =>
            {
                source.DistinctBy(property);
            });

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void DistinctBy_ReturnsAnUnchangedCollectionIfNoDuplicatePropertyValuesAreFound()
        {
            // Arrange
            var source = _fixture.CreateMany<Person>();

            // Act
            var peopleWithDistinctEmailAddresses = source.DistinctBy(o => o.EmailAddress);

            // Assert
            Assert.Equal(source, peopleWithDistinctEmailAddresses);
        }

        [Fact]
        public void DistinctBy_RemovesObjectsWhichHaveDuplicatePropertyValues()
        {
            // Arrange
            var emailAddress = _fixture.Create<string>();

            var source = new List<Person>
            {
                _fixture.Create<Person>(),
                _fixture.Build<Person>().With(o => o.EmailAddress, emailAddress).Create(),
                _fixture.Build<Person>().With(o => o.EmailAddress, emailAddress).Create()
            };

            // Act
            var peopleWithDistinctEmailAddresses = source.DistinctBy(o => o.EmailAddress);

            // Assert
            foreach (var group in peopleWithDistinctEmailAddresses.GroupBy(o => o.EmailAddress))
            {
                Assert.Single(group);
            }
        }

        private class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}
