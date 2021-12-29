﻿namespace Gimma.Repositories;

public class ImageRepository
{
    private readonly List<string> _images  = new List<string>
    {
        "image (1).jpg",
        "image (2).jpg",
        "image (3).jpg",
        "image (4).jpg",
        "image (5).jpg",
        "image (6).jpg",
        "image (7).jpg",
        "image (8).jpg",
        "image (9).jpg",
        "image (10).jpg",
        "image (11).jpg",
        "image (12).jpg",
        "image (13).jpg",
        "image (14).jpg",
        "image (15).jpg",
        "image (16).jpg",
        "image (17).jpg",
        "image (18).jpg",
        "image (19).jpg",
        "image (20).jpg",
        "image (21).jpg",
        "image (22).jpg",
        "image (23).jpg",
        "image (24).jpg",
        "image (25).jpg",
        "image (26).jpg",
        "image (27).jpg",
        "image (28).jpg",
        "image (29).jpg",
        "image (30).jpg",
        "image (31).jpg",
        "image (32).jpg",
        "image (33).jpg",
        "image (34).jpg",
        "image (35).jpg",
        "image (36).jpg",
        "image (37).jpg",
        "image (38).jpg",
        "image (39).jpg",
        "image (40).jpg",
    };
    private static Random random = new Random();
    
    public List<string> GetRandomImages(int amount)
    {
        if (amount > _images.Count)
        {
            throw new Exception("Not enough unique prompts");
        }
        return _images.OrderBy(x => random.Next()).Take(amount).ToList();
    }
}