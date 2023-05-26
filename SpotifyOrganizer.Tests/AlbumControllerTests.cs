using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SpotifyOrganizer.Controllers;
using Moq;
using SpotifyOrganizer.Data;
using SpotifyOrganizer.Models;

namespace SpotifyOrganizer.Tests;

[TestFixture]
public class AlbumControllerTests
{
/* In progress
    [Test]
    public async Task GetAlbums_ReturnsAlbums()
    {
        // Arrange
        var mockAlbumService = new Mock<ApplicationDbContext>();
        var mockDbSet = new Mock<DbSet<Album>>();

        mockDbSet.Setup(db => db.Add(new Album { Id = 1, AlbumName = "Test Album" }));

        var tmpAlbums = new List<Album>();
        mockAlbumService.Setup(service => service.Albums.ToList()).Returns(tmpAlbums);

//        mockDbSet.Setup(db => db.Add(It.IsAny<Album>()));


        var controller = new AlbumsController(mockAlbumService.Object);

        //Act
        var result = await controller.Details(1);

        //Assert
        Assert.IsInstanceOf<IActionResult>(result);
        */
}