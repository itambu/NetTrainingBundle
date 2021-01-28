using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFolder.Core.Models
{
    public class AudioElement : Element, IPlayable
    {
        public FileInfo FileInfo { get; private set; }

        public void Play(IPlayer player)
        {
            player.Play(new object());
        }
    }
}
