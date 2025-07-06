using System;

namespace BlogSample.Posts;

public record PostChangedEvent(Guid BlogId);