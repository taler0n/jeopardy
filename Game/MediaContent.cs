using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum MediaContentType
    {
        None, Picture, Audio, Video
    }

    public class MediaContent
    {
        public MediaContentType Type { get; }
        public string Path { get; }

        public MediaContent(MediaContentType type, string path)
        {
            Type = type;
            Path = path;
        }
    }
}
