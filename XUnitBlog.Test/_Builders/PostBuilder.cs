using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Test._Builders;

internal class PostBuilder
{
    private readonly Faker _faker;
    private string _title;
    private string _content;
    private string _thumbnail;
    private long _userId;
    private bool _pinned;

    public PostBuilder()
    {
        _faker = new Faker();
        _title = _faker.Lorem.Sentences(2);
        _content = _faker.Lorem.Paragraph(1);
        _thumbnail = _faker.Image.LoremFlickrUrl();
        _userId = _faker.Random.Number(1, 100);
        _pinned = false;
    }

    public static PostBuilder New()
    {
        return new PostBuilder();
    }

    public PostBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public PostBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public PostBuilder WithThumbnail(string thumbnail)
    {
        _thumbnail = thumbnail;
        return this;
    }

    public PostBuilder WithUserId(long userId)
    {
        _userId = userId;
        return this;
    }

    public PostBuilder IsPinned(bool state)
    {
        _pinned = state;
        return this;
    }

    public Post Build()
    {
        return new Post(_title, _content, _thumbnail, _userId, _pinned);
    }
}
