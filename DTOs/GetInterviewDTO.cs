﻿using Interview_Server.Models;

namespace Interview_Server.DTOs
{
    public class GetInterviewDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime? time { get; set; }

        public string address { get; set; }

        public int duration { get; set; }

        public Enum status { get; set; }

        public string companyName { get; set; }

        public List<Note> notes { get; set; }

    }
}
