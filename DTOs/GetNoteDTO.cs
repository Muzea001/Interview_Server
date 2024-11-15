﻿using Interview_Server.Models;

namespace Interview_Server.DTOs
{
    public class GetNoteDTO
    {
        public string title { get; set; }

        public string content { get; set; }

        public NoteStatus status { get; set; }
    }
}