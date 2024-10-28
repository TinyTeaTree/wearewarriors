using Core;

namespace Game
{
    public class PlayerAccountRecord : BaseRecord
    {
        public string PlayerId { get; set; }

        public void Reset()  //TODO: Add a Test that Resets makes this record the same as a New() record
        {
            PlayerId = null;
        }
    }
}