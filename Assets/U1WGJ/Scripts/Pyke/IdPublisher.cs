namespace Pyke
{
    public class IdPublisher
    {
        const int DEFAULT_MAX_PUBLISHABLE_ID = 1000;

        bool[] _publishedIds;

        public IdPublisher()
        {
            _publishedIds = new bool[DEFAULT_MAX_PUBLISHABLE_ID];
        }
        public IdPublisher(uint maxPublishableId)
        {
            _publishedIds = new bool[maxPublishableId];
        }

        public uint PublishNewId()
        {
            for (uint i = 0; i < _publishedIds.Length; i++)
            {
                if (!_publishedIds[i])
                {
                    _publishedIds[i] = true;
                    return i;
                }
            }

            throw new System.InvalidOperationException("this publisher has no publishable id.");
        }

        public void ReturnId(uint id)
        {
            _publishedIds[id] = false;
        }

    }

}


